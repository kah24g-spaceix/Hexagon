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
        min = playTimeValue / timeValue;
        sec = playTimeValue % timeValue;
        TotalPlayTime.SetText($"{LocalizationManager.Instance.GetLocalizedText("playTime.total")}\n{min}m : {sec}s");
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

        simulationPlayButton.onClick.AddListener(() => PlaySimulationMode(false, isStoryMode: false));
    }

    private void StartSelectMode()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        SetupModeSelection(isLoad: false);

    }

    private void LoadSelectMode()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        SetupModeSelection(isLoad: true);
    }

    private void SetupModeSelection(bool isLoad)
    {
        storyButton.onClick.RemoveAllListeners();
        simulationButton.onClick.RemoveAllListeners();

        storyButton.onClick.AddListener(() => OnStoryButton(isLoad));
        if (isLoad)
            simulationButton.onClick.AddListener(() => OnSimulationButton(isLoad));
        else
            simulationButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
                ShowUI(simulationPopup);
            });

        ShowUI(selectModeUI);
    }
    private void OnStoryButton(bool isLoad)
    {
        if (!isLoad && PlayerPrefs.HasKey("StorySave"))
        {
            QuestionDialogUI.Instance.ShowQuestion(
                LocalizationManager.Instance.GetLocalizedText("question.story"),
                () => OnGameModeSelected(isLoad, isStoryMode: true),
                () => { }
            );
            return;
        }
        if (isLoad && !PlayerPrefs.HasKey("StorySave"))
        {
            WarningDialogUI.Instance.ShowWarning(
                LocalizationManager.Instance.GetLocalizedText("warning.noData"),
                () => { });
            return;
        }
        OnGameModeSelected(isLoad, isStoryMode: true);
    }
    private void OnSimulationButton(bool isLoad)
    {
        if (!isLoad && PlayerPrefs.HasKey("Save"))
        {
            QuestionDialogUI.Instance.ShowQuestion(
                LocalizationManager.Instance.GetLocalizedText("question.simulation"),
                () => OnGameModeSelected(isLoad, isStoryMode: false),
                () => { }
            );
            return;
        }
        if (isLoad && !PlayerPrefs.HasKey("Save"))
        {
            WarningDialogUI.Instance.ShowWarning(
                LocalizationManager.Instance.GetLocalizedText("warning.noData"), 
                () => { });
            return;
        }
        OnGameModeSelected(isLoad, isStoryMode: false);
    }
    private void OnGameModeSelected(bool isLoad, bool isStoryMode)
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        int playtime = playtimeManager.PlaytimeValue;
        int lastDay = lastDayManager.LastDay;
        int initialMoney = initialMoneyManager.InitialMoney;
        GameStateManager.Instance.SetGameState(isLoad, isStoryMode, playtime, lastDay, initialMoney);
        LoadingSceneManager.LoadScene("MainGameScene");
    }
    private void PlaySimulationMode(bool isLoad, bool isStoryMode)
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        int playtime = playtimeManager.PlaytimeValue;
        int lastDay = lastDayManager.LastDay;
        int initialMoney = initialMoneyManager.InitialMoney;
        GameStateManager.Instance.SetGameState(isLoad, isStoryMode, playtime, lastDay, initialMoney);

        if (!isStoryMode || !isLoad)
        {
            if (playtimeManager.PlaytimeValue == 0 || lastDayManager.LastDay == 0)
            {
                WarningDialogUI.Instance.ShowWarning(
                    LocalizationManager.Instance.GetLocalizedText("warning.zero"),
                    () => { });
                return;
            }

        }
        LoadingSceneManager.LoadScene("MainGameScene");
    }
    private void ShowUI(GameObject UI) => UI.SetActive(true);
    private void HideUI(GameObject UI) => UI.SetActive(false);
    private void PopupTrigger(GameObject UI)
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
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
