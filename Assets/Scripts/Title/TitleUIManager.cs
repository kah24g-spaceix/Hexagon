using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
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

    [Header("Other")]
    [SerializeField] private TextMeshProUGUI TotalPlayTime;
    private int hour;
    private int min;
    private int sec;


    private PlaytimeManager playtimeManager;
    private LastDayManager lastDayManager;
    private InitialMoneyManager initialMoneyManager;

    private void Awake()
    {
        playtimeManager = GetComponent<PlaytimeManager>();
        lastDayManager = GetComponent<LastDayManager>();
        initialMoneyManager = GetComponent<InitialMoneyManager>();
    }

    private void Start()
    {
        InitializeUI();
        AddListeners();
    }

    private void Update()
    {
        int playTimeValue = playtimeManager.PlaytimeValue * lastDayManager.LastDay;
        const int timeValue = 60;
        hour = min / timeValue;
        min = playTimeValue / timeValue;
        sec = playTimeValue % timeValue;
        TotalPlayTime.SetText($"Total play time\n{min}m : {sec}s");
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
        AudioManager.Instance.PlaySFX("Select");
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
        AudioManager.Instance.PlaySFX("Select");
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
        if (isLoad)
            simulationButton.onClick.AddListener(() => OnGameModeSelected(isLoad, isStoryMode: false));
        else
            simulationButton.onClick.AddListener(() => {
                AudioManager.Instance.PlaySFX("Select");
                ShowUI(simulationPopup);
            });

        ShowUI(selectModeUI);
    }

    private void OnGameModeSelected(bool isLoad, bool isStoryMode)
    {
        AudioManager.Instance.PlaySFX("Select");
        int playtime = playtimeManager.PlaytimeValue;
        int lastDay = lastDayManager.LastDay;
        int initialMoney = initialMoneyManager.InitialMoney;
        GameStateManager.Instance.SetGameState(isLoad, isStoryMode, playtime, lastDay, initialMoney);
       
        if (playtimeManager.PlaytimeValue == 0 || lastDayManager.LastDay == 0)
        {
            WarningDialogUI.Instance.ShowWarning(
                "Warning!!\nPlaytime ot Day is 0",
                () => { }
            );
            return;
        }
        LoadingSceneManager.LoadScene("MainGameScene");
    }

    private void ShowUI(GameObject UI) => UI.SetActive(true);
    private void HideUI(GameObject UI) => UI.SetActive(false);
    private void PopupTrigger(GameObject UI) 
    {
        AudioManager.Instance.PlaySFX("Select");
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
}
