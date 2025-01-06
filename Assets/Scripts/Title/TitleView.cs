using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleView : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [Header("Options Popup")]
    [SerializeField] private GameObject optionPopup;

    [Header("Mode Select")]
    [SerializeField] private GameObject SelectModeUI;
    [SerializeField] private Button StoryButton;
    [SerializeField] private Button SimulationButton;

    private void Start()
    {
        HideUI(SelectModeUI);
        HideUI(optionPopup);
        AddListeners();
    }
    private void AddListeners()
    {
        // Title buttons
        startButton.onClick.AddListener(() =>
        {
            StoryButton.onClick.RemoveAllListeners();
            SimulationButton.onClick.RemoveAllListeners();
            StoryButton.onClick.AddListener(() => StartSelectMode(true));
            SimulationButton.onClick.AddListener(() => StartSelectMode(false));
            ShowUI(SelectModeUI);
        });
        loadButton.onClick.AddListener(() =>
        {
            StoryButton.onClick.RemoveAllListeners();
            SimulationButton.onClick.RemoveAllListeners();
            StoryButton.onClick.AddListener(() => LoadSelectMode(true));
            SimulationButton.onClick.AddListener(() => LoadSelectMode(false));
            ShowUI(SelectModeUI);
        });
        optionsButton.onClick.AddListener(() => PopupTrigger(optionPopup));
        exitButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        });
    }
    private void StartSelectMode(bool isStoryMode)
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            QuestionDialogUI.Instance.ShowQuestion(
                "Warning!!\nPlay data remains.\nDo you want to continue?", () =>
                {
                    SetGameState(false, isStoryMode);
                }, () => { });
        }
        else SetGameState(false, isStoryMode);
    }
    private void LoadSelectMode(bool isStoryMode)
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            WarningDialogUI.Instance.ShowWarning("Warning!!\nThere is no data to load.", () => { });
        }
        else SetGameState(true, isStoryMode);
    }
    public void SetGameState(bool isLoad, bool isStoryMode)
    {
        // 씬 로드 완료 후 상태 전달을 위한 람다식으로 이벤트 핸들러 연결
        SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode, isLoad, isStoryMode);
        SceneManager.LoadScene("MainGameScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode, bool isLoad, bool isStoryMode)
    {
        if (scene.name == "MainGameScene") // 특정 씬인지 확인
        {
            // GameManager 확인
            GameObject gameManagerObject = GameObject.Find("GameManager");
            if (gameManagerObject == null)
            {
                Debug.LogError("GameManager GameObject를 씬에서 찾을 수 없습니다.");
                return;
            }

            GameModel gameManager = gameManagerObject.GetComponent<GameModel>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager GameObject에 GameModel 컴포넌트가 없습니다.");
                return;
            }

            // 상태 설정
            gameManager.isLoad = isLoad;
            gameManager.isStoryMode = isStoryMode;

            // 이벤트 등록 해제
            SceneManager.sceneLoaded -= (sceneArg, modeArg) => OnSceneLoaded(sceneArg, modeArg, isLoad, isStoryMode);
        }
    }

    private void ShowUI(GameObject UI)
    {
        UI.SetActive(true);
    }

    private void HideUI(GameObject UI)
    {
        UI.SetActive(false);
    }
    private void PopupTrigger(GameObject UI)
    {
        UI.SetActive(!UI.activeSelf);
    }
}
