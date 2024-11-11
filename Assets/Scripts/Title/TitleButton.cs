using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Answer
{
    Yes,
    No
};
public class TitleButton : MonoBehaviour
{
    [Header("Title Button")]
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button gameLoadButton;
    [SerializeField] private Button gameQuitButton;


    [Header("Warning UI")]
    [SerializeField] private GameObject checkData;
    private Button[] checkDataButtons;

    [SerializeField] private GameObject loadFailedPopup;
    private Button loadFailedPopupOkButton;

    private TextMeshProUGUI gameStartButtonName;
    private TextMeshProUGUI gameLoadButtonName;
    private TextMeshProUGUI gameQuitButtonName;

    private void Awake()
    {
        gameStartButtonName = gameStartButton.GetComponentInChildren<TextMeshProUGUI>();
        gameLoadButtonName = gameLoadButton.GetComponentInChildren<TextMeshProUGUI>();
        gameQuitButtonName = gameQuitButton.GetComponentInChildren<TextMeshProUGUI>();

        checkDataButtons = checkData.GetComponentsInChildren<Button>();
        loadFailedPopupOkButton = loadFailedPopup.GetComponentInChildren<Button>();

        HideCheckDataPopup();
        HideLoadFailedPopup();

        gameStartButtonName.SetText("Start");
        gameLoadButtonName.SetText("Load");
        gameQuitButtonName.SetText("Quit");

        gameStartButton.onClick.AddListener(OnStartButton);
        checkDataButtons[(int)Answer.Yes].onClick.AddListener(CheckDataAnswerYes);
        checkDataButtons[(int)Answer.No].onClick.AddListener(HideCheckDataPopup);

        gameLoadButton.onClick.AddListener(OnDoDataLoad);
        loadFailedPopupOkButton.onClick.AddListener(HideLoadFailedPopup);

        gameQuitButton.onClick.AddListener(OnQuitButton);


    }

    private void OnStartButton()
    {
        if (PlayerPrefs.HasKey("Save"))
            checkData.SetActive(true);
        else
        {
            CheckDataAnswerYes();
        }

    }
    private void CheckDataAnswerYes()
    {
        ChangeScene();
        IsLoad(false);
    }
    private void HideCheckDataPopup()
    {
        checkData.SetActive(false);
    }


    private void OnDoDataLoad()
    {
        if (!PlayerPrefs.HasKey("Save"))
            loadFailedPopup.SetActive(true);
        else
        {
            ChangeScene();
            IsLoad(true);
        }
    }
    private void HideLoadFailedPopup()
    {
        loadFailedPopup.SetActive(false);
    }


    private void OnQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void IsLoad(bool isDataLoad)
    {
        GameObject otherSceneObject = GameObject.FindWithTag("GameMamager");
        otherSceneObject.GetComponent<GameModel>().isLoad = isDataLoad;
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
