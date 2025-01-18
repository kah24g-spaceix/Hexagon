using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [Header("Options Popup")]
    [SerializeField] private GameObject optionPopup;

    [Header("Mode Select")]
    [SerializeField] private GameObject selectModeUI;
    [SerializeField] private Button storyButton;
    [SerializeField] private Button simulationButton;

    private GameStateManager gameStateManager;

    private void Start()
    {
        gameStateManager = FindObjectOfType<GameStateManager>();
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
        startButton.onClick.AddListener(() => SetupModeSelection(isLoad: false));
        loadButton.onClick.AddListener(() => SetupModeSelection(isLoad: true));
        optionsButton.onClick.AddListener(() => PopupTrigger(optionPopup));
        exitButton.onClick.AddListener(ExitGame);
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
        int dailyPlaytime = 120; // 예제: 이 값을 UI에서 얻을 수 있도록 수정 가능
        gameStateManager.SetGameState(isLoad, isStoryMode, dailyPlaytime);
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
