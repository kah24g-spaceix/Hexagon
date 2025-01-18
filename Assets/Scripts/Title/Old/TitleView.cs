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
        InitializeUI();
        AddListeners();
    }

    #region Initialization

    private void InitializeUI()
    {
        HideUI(SelectModeUI);
        HideUI(optionPopup);
    }

    private void AddListeners()
    {
        // Title buttons
        startButton.onClick.AddListener(() => SetupModeSelection(StartSelectMode));
        loadButton.onClick.AddListener(() => SetupModeSelection(LoadSelectMode));
        optionsButton.onClick.AddListener(() => PopupTrigger(optionPopup));
        exitButton.onClick.AddListener(ExitGame);
    }

    private void SetupModeSelection(Action<bool> modeAction)
    {
        StoryButton.onClick.RemoveAllListeners();
        SimulationButton.onClick.RemoveAllListeners();
        StoryButton.onClick.AddListener(() => modeAction(true));
        SimulationButton.onClick.AddListener(() => modeAction(false));
        ShowUI(SelectModeUI);
    }

    #endregion

    #region Game Mode Management

    private void StartSelectMode(bool isStoryMode)
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            QuestionDialogUI.Instance.ShowQuestion(
                "Warning!!\nPlay data remains.\nDo you want to continue?", 
                () => SetGameState(false, isStoryMode), 
                () => { });
        }
        else
        {
            SetGameState(false, isStoryMode);
        }
    }

    private void LoadSelectMode(bool isStoryMode)
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            WarningDialogUI.Instance.ShowWarning("Warning!!\nThere is no data to load.", () => { });
        }
        else
        {
            SetGameState(true, isStoryMode);
        }
    }

    public void SetGameState(bool isLoad, bool isStoryMode)
    {
        SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode, isLoad, isStoryMode);
        SceneManager.LoadScene("MainGameScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode, bool isLoad, bool isStoryMode)
    {
        if (scene.name == "MainGameScene")
        {
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

            gameManager.isLoad = isLoad;
            gameManager.isStoryMode = isStoryMode;

            SceneManager.sceneLoaded -= (sceneArg, modeArg) => OnSceneLoaded(sceneArg, modeArg, isLoad, isStoryMode);
        }
    }

    #endregion

    #region UI Management

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

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion
}
