using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameView : MonoBehaviour, IGameView
{
    private enum InGameButton 
    {
        Resume,
        Option,
        Save,
        Title,
        Exit,
    }
    
    [Header("Menu Popup")]
    [SerializeField] private GameObject MenuPopup;
    [SerializeField] private GameObject OptionPopup;


    [Header("Menu Buttons")]
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button OptionButton;
    [SerializeField] private Button SaveButton;
    [SerializeField] private Button TitleButton;
    [SerializeField] private Button ExitButton;

    [Header("In Game Text")]
    [SerializeField] private TextMeshProUGUI Day;
    [SerializeField] private TextMeshProUGUI CurrentTime;
    [SerializeField] private TextMeshProUGUI Money;

    [Header("In Game Button")]
    [SerializeField] private Button DayCycleButton;
    [SerializeField] private Button DaySkipButton;

    [Header("In Game Popup")]
    [SerializeField] private GameObject FactoryUI;
    [SerializeField] private GameObject HyperFrameUI;
    [SerializeField] private GameObject TechTreeUI;
    [SerializeField] private GameObject HeadCountUI;
    [SerializeField] private GameObject StoreUI;

    [Header("To Day Result UI")]
    [SerializeField] private GameObject ToDayResultUI;
    public GameObject ToDayResult {get; private set;}
    [SerializeField] private Button NextDayButton;
    [SerializeField] private Button RestartDayButton;

    private IGamePresenter gamePresenter;

    private bool isDayCycleButton;
    private bool gameIsPaused;


    private void Awake()
    {
        gamePresenter = GetComponent<IGamePresenter>();
        ToDayResult = ToDayResultUI;
    }

    private void Start()
    {
        TextUIUpdate();
        HideUI(MenuPopup);
        HideUI(OptionPopup);
        HideUI(ToDayResultUI);

        HideUI(FactoryUI);
        HideUI(HyperFrameUI);
        HideUI(TechTreeUI);
        HideUI(HeadCountUI);
        HideUI(StoreUI);

        ResumeButton.onClick.AddListener(() => ButtonType(InGameButton.Resume));
        OptionButton.onClick.AddListener(() => ButtonType(InGameButton.Option));
        SaveButton.onClick.AddListener(() => ButtonType(InGameButton.Save));
        TitleButton.onClick.AddListener(() => ButtonType(InGameButton.Title));
        ExitButton.onClick.AddListener(() => ButtonType(InGameButton.Exit));

        DayCycleButton.onClick.AddListener(() => {PauseTrigger(); isDayCycleButton = !isDayCycleButton;});
        DaySkipButton.onClick.AddListener(gamePresenter.OnDaySkipButton);
        
        NextDayButton.onClick.AddListener(() =>
            QuestionDialogUI.Instance.ShowQuestion(
                "Do you want to move forward to the next day?", () => {gamePresenter.OnNextDayButton(); HideUI(ToDayResult); }, () => { }));
        RestartDayButton.onClick.AddListener(() =>
            QuestionDialogUI.Instance.ShowQuestion(
                "Do you want to begin the day again?", () => { gamePresenter.OnRestartDayButton(); HideUI(ToDayResult); }, () => { }));
    }
    private void ButtonType(InGameButton buttonType)
    {
        switch (buttonType)
        {
            case InGameButton.Resume:
                HideUI(MenuPopup);
                gamePresenter.Resume();
                break;
            case InGameButton.Option:
                ShowUI(OptionPopup);
                break;
            case InGameButton.Save:
                QuestionDialogUI.Instance.ShowQuestion(
                    "Do you want to save your progress?", () => gamePresenter.DoSaveGame(false), () => { });
                break;
            case InGameButton.Title:
                QuestionDialogUI.Instance.ShowQuestion(
                "Are you sure you want to return to the title screen?",
                () =>
                {
                    QuestionDialogUI.Instance.ShowQuestion(
                        "Do you want to save your progress?", () =>
                        {
                            gamePresenter.DoSaveGame(false);
                            SceneManager.LoadScene("TitleScene");
                            gamePresenter.Resume();
                        }, () => { SceneManager.LoadScene("TitleScene"); gamePresenter.Resume(); });
                }, () => { });
                break;
            case InGameButton.Exit:
                QuestionDialogUI.Instance.ShowQuestion(
                "Are you sure you want to exit the game?",
                () =>
                {
                    QuestionDialogUI.Instance.ShowQuestion(
                        "Do you want to save your progress?", () =>
                        {
                            gamePresenter.DoSaveGame(false);
                            Application.Quit();
                        }, () => { });
                }, () => { });
                break;
        }
    }
    private void Update()
    {
        HandlePauseInput();
        if (!MenuPopup.activeSelf) HandleInGameInput();
    }
    private void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionPopup.activeSelf)
                OptionPopup.SetActive(!OptionPopup.activeSelf);
            else
            {
                if (!isDayCycleButton) PauseTrigger();
                MenuPopup.SetActive(!MenuPopup.activeSelf);
            }
        }
    }
    private void HandleInGameInput()
    {
        PopUpTrigger(FactoryUI, KeyCode.Alpha1);
        PopUpTrigger(HyperFrameUI, KeyCode.Alpha2);
        PopUpTrigger(TechTreeUI, KeyCode.Alpha3);
        PopUpTrigger(HeadCountUI, KeyCode.Alpha4);
        PopUpTrigger(StoreUI, KeyCode.Alpha5);
    }
    private void PauseTrigger()
    {
        if (!gameIsPaused)
            gamePresenter.Pause();
            
        else
            gamePresenter.Resume();
        gameIsPaused = !gameIsPaused;
    }
    public void ViewUpdate()
    {
        TextUIUpdate();
    }
    public void TextUIUpdate()
    {
        Day.SetText(gamePresenter.GetDay());
        Money.SetText(gamePresenter.GetMoney());
    }
    public void ClockUpdate(float hour, float minute)
    {
        CurrentTime.SetText($"{hour:00} : {minute:00}");
    }
    public void ShowUI(GameObject UI)
    {
        UI.SetActive(true);
    }
    public void HideUI(GameObject UI)
    {
        UI.SetActive(false);
    }
    public void PopUpTrigger(GameObject UI, KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            UI.SetActive(!UI.activeSelf);
        }
    }



}
