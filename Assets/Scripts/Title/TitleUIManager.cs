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
    private LastDayManager lastDayManager;
    private InitialMoneyManager initialMoneyManager;


    private void Awake()
    {
        gameStateManager = GetComponent<GameStateManager>();
        playtimeManager = GetComponent<PlaytimeManager>();
        lastDayManager = GetComponent<LastDayManager>();
        initialMoneyManager = GetComponent<InitialMoneyManager>();
    }
    private void Start()
    {

        InitializeUI();
        AddListeners();
    }

    private void InitializeUI()
    {
        HideUI(selectModeUI);
        HideUI(optionPopup);
        HideUI(simulationPopup);
    }

    private void AddListeners()
    {
        startButton.onClick.AddListener(() => StartSelectMode());
        loadButton.onClick.AddListener(() => LoadSelectMode());
        optionsButton.onClick.AddListener(() => PopupTrigger(optionPopup));
        exitButton.onClick.AddListener(ExitGame);

        simulationPlayButton.onClick.AddListener(() => OnGameModeSelected(false, isStoryMode: false));
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
        if (isLoad) simulationButton.onClick.AddListener(() => OnGameModeSelected(isLoad, isStoryMode: false));
        else simulationButton.onClick.AddListener(() => ShowUI(simulationPopup));
        ShowUI(selectModeUI);
    }

    private void OnGameModeSelected(bool isLoad, bool isStoryMode)
    {
        int playtime = playtimeManager.PlaytimeValue;
        int lastDay = lastDayManager.LastDay;
        int initialMoney = initialMoneyManager.InitialMoney;
        gameStateManager.SetGameState(isLoad, isStoryMode, playtime, lastDay, initialMoney);
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
