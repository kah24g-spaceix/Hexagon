using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [Header("Popups")]
    [SerializeField] private GameObject optionPopup;
    [SerializeField] private GameObject simulationPopup;
    
    [Header("Mode Select")]
    [SerializeField] private GameObject selectModeUI;
    [SerializeField] private Button storyButton;
    [SerializeField] private Button simulationButton;
    [SerializeField] private Button simulationPlayButton;

    private GameStateManager gameStateManager;
    private PlaytimeManager playtimeManager;

    private void Start()
    {
        gameStateManager = GetComponent<GameStateManager>();
        playtimeManager = GetComponent<PlaytimeManager>();
        InitializeUI();
        AddListeners();
    }

    private void InitializeUI()
    {
        HideUI(selectModeUI);
        HideUI(optionPopup);
    }

    private void AddListeners()
    {
        startButton.onClick.AddListener(() => StartSelectMode());
        loadButton.onClick.AddListener(() => LoadSelectMode());
        optionsButton.onClick.AddListener(() => PopupTrigger(optionPopup));
        exitButton.onClick.AddListener(ExitGame);
    }

    private void StartSelectMode()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            QuestionDialogUI.Instance.ShowQuestion(
                "Warning!!\nPlay data remains.\nDo you want to continue?",
                () => SetupModeSelection(isLoad: false),
                () => { }
            );
        }
        else
        {
            SetupModeSelection(isLoad: false);
        }
    }

    private void LoadSelectMode()
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            WarningDialogUI.Instance.ShowWarning(
                "Warning!!\nThere is no data to load.",
                () => { }
            );
        }
        else
        {
            SetupModeSelection(isLoad: true);
        }
    }

    private void SetupModeSelection(bool isLoad)
    {
        storyButton.onClick.RemoveAllListeners();
        simulationButton.onClick.RemoveAllListeners();

        storyButton.onClick.AddListener(() => OnGameModeSelected(isLoad, isStoryMode: true));
        simulationButton.onClick.AddListener(() => OnGameModeSelected(isLoad, isStoryMode: false));

        ShowUI(selectModeUI);
    }

    private void OnGameModeSelected(bool isLoad, bool isStoryMode)
    {
        int playtime = playtimeManager.PlaytimeValue;
        int lastDay = 0;
        gameStateManager.SetGameState(isLoad, isStoryMode, playtime, lastDay);
    }

    private void ShowUI(GameObject UI) => UI.SetActive(true);
    private void HideUI(GameObject UI) => UI.SetActive(false);
    private void PopupTrigger(GameObject UI) => UI.SetActive(!UI.activeSelf);

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
