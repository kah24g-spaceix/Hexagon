using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [Header("Options Popup")]
    [SerializeField] private GameObject optionsPopup;

    [Header("Check Data Popup")]
    [SerializeField] private GameObject checkDataPopup;
    [SerializeField] private Button checkDataYesButton;
    [SerializeField] private Button checkDataNoButton;

    [Header("Load Data Popup")]
    [SerializeField] private GameObject loadFailedPopup;
    [SerializeField] private Button loadFailedPopupOkButton;

    private void Awake()
    {
        InitializePopup();
        AddListeners();
    }

    private void InitializePopup()
    {
        HidePopup(checkDataPopup);
        HidePopup(loadFailedPopup);
    }

    private void AddListeners()
    {
        // Title buttons
        startButton.onClick.AddListener(OnStartButtonClicked);
        loadButton.onClick.AddListener(OnLoadButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        // Check data popup buttons
        checkDataYesButton.onClick.AddListener(OnCheckDataYes);
        checkDataNoButton.onClick.AddListener(() => HidePopup(checkDataPopup));

        // Load failed popup button
        loadFailedPopupOkButton.onClick.AddListener(() => HidePopup(loadFailedPopup));
    }

    // Button Actions
    private void OnStartButtonClicked()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            ShowPopup(checkDataPopup);
        }
        else
        {
            OnCheckDataYes();
        }
    }

    private void OnLoadButtonClicked()
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            ShowPopup(loadFailedPopup);
        }
        else
        {
            ChangeScene();
            SetLoadState(true);
        }
    }

    private void OnOptionsButtonClicked()
    {
        ShowPopup(optionsPopup);
    }

    private void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnCheckDataYes()
    {
        ChangeScene();
        SetLoadState(false);
    }

    // Utility Methods
    private void ChangeScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    private void SetLoadState(bool isLoad)
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        if (gameManager != null)
        {
            gameManager.GetComponent<GameModel>().isLoad = isLoad;
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void ShowPopup(GameObject popup)
    {
        popup?.SetActive(true);
    }

    private void HidePopup(GameObject popup)
    {
        popup?.SetActive(false);
    }
}
