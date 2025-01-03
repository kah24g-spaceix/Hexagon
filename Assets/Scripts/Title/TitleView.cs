using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleView : MonoBehaviour
{
    private enum TitleButton
    {
        Option,
        Exit
    }
    

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
            StoryButton.onClick.AddListener(() => StartSelectMode(true));
            SimulationButton.onClick.AddListener(() => StartSelectMode(false));
            ShowUI(SelectModeUI);
        });
        loadButton.onClick.AddListener(() => 
        {
            StoryButton.onClick.AddListener(() => LoadSelectMode(true));
            SimulationButton.onClick.AddListener(() => LoadSelectMode(false));
            ShowUI(SelectModeUI);
        });
        optionsButton.onClick.AddListener(() => ButtonType(TitleButton.Option));
        exitButton.onClick.AddListener(() => ButtonType(TitleButton.Exit));
    }
    private void ButtonType(TitleButton buttonType)
    {
        switch (buttonType)
        {
            case TitleButton.Option: PopupTrigger(optionPopup); break;
            case TitleButton.Exit: 
                #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                #else
                        Application.Quit();
                #endif
            break;
        }
    }
    private void StartSelectMode(bool isStoryMode)
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            QuestionDialogUI.Instance.ShowQuestion(
                "Warning!!\nPlay data remains.\nDo you want to continue?", () =>
                {
                    ChangeScene();
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
    private void ChangeScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    private void SetGameState(bool isLoad, bool isStoryMode)
    {
        ChangeScene();
        GameModel gameManager = GameObject.Find("GameManager").GetComponent<GameModel>();
        if (gameManager != null)
        {
            gameManager.isLoad = isLoad;
            gameManager.isStoryMode = isStoryMode;
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
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
