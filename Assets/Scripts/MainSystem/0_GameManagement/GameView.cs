using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum InGameButton
{
    ResumeButton,
    OptionButton,
    SaveButton,
    TitleButton,
    ExitButton,

}
public class GameView : MonoBehaviour, IGameView
{
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

    [Header("In Game UI")]
    [SerializeField] private GameObject FactoryUI;
    private bool factory;
    [SerializeField] private GameObject TechTreeUI;
    private bool techtree;

    [Header("To Day Result UI")]
    [SerializeField] private GameObject ToDayResultUI;
    [SerializeField] private Button NextDayButton;
    [SerializeField] private Button RestartDayButton;

    private IGamePresenter gamePresenter;
    private bool gameIsPaused;


    private void Awake()
    {
        gamePresenter = GetComponent<IGamePresenter>();
    }

    private void Start()
    {
        TextUIUpdate();
        HideUI(OptionPopup);
        HideUI(TechTreeUI);
        ResumeButton.onClick.AddListener(() => ButtonType(InGameButton.ResumeButton));
        OptionButton.onClick.AddListener(() => ButtonType(InGameButton.OptionButton));
        SaveButton.onClick.AddListener(() => ButtonType(InGameButton.SaveButton));
        TitleButton.onClick.AddListener(() => ButtonType(InGameButton.TitleButton));
        ExitButton.onClick.AddListener(() => ButtonType(InGameButton.ExitButton));

        NextDayButton.onClick.AddListener(() =>
            QuestionDialogUI.Instance.ShowQuestion(
                "Do you want to move forward to the next day?", () => gamePresenter.DoSaveGame(true), () => { }));
        RestartDayButton.onClick.AddListener(() =>
            QuestionDialogUI.Instance.ShowQuestion(
                "Do you want to begin the day again?", () => gamePresenter.DoLoadGame(true), () => { }));
    }
    private void ButtonType(InGameButton buttonType)
    {
        switch (buttonType)
        {
            case InGameButton.ResumeButton:
                HideUI(MenuPopup);
                Resume();
                break;
            case InGameButton.OptionButton:
                ShowUI(MenuPopup);
                break;
            case InGameButton.SaveButton:
                QuestionDialogUI.Instance.ShowQuestion(
                    "Do you want to save your progress?", () => gamePresenter.DoSaveGame(false), () => { });
                break;
            case InGameButton.TitleButton:
                QuestionDialogUI.Instance.ShowQuestion(
                "Are you sure you want to return to the title screen?",
                () =>
                {
                    QuestionDialogUI.Instance.ShowQuestion(
                        "Are you really sure?", () =>
                        {
                            QuestionDialogUI.Instance.ShowQuestion(
                                "Do you want to save your progress?", () =>
                                {
                                    gamePresenter.DoSaveGame(false);
                                    SceneManager.LoadScene("MainGameScene");
                                }, () => { });
                        }, () => { });
                }, () => { });
                break;
            case InGameButton.ExitButton:
                QuestionDialogUI.Instance.ShowQuestion(
                "Are you sure you want to exit the game?",
                () =>
                {
                    QuestionDialogUI.Instance.ShowQuestion(
                        "Are you really sure?", () =>
                        {
                            QuestionDialogUI.Instance.ShowQuestion(
                                "Do you want to save your progress?", () =>
                                {
                                    gamePresenter.DoSaveGame(false);
                                    Application.Quit();
                                }, () => { });
                        }, () => { });
                }, () => { });
                break;
        }
    }
    private void Update()
    {
        HandlePauseInput();
        if (!gameIsPaused) HandleInGameInput();
    }
    private void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            if (!gameIsPaused)
            {
                ShowUI(MenuPopup);
                Pause();
            }
            else
            {
                HideUI(MenuPopup);
                Resume();
            }
        }
    }
    private void HandleInGameInput()
    {
        PopUpTrigger(FactoryUI, factory, KeyCode.Keypad1);
        PopUpTrigger(TechTreeUI, techtree, KeyCode.Keypad2);
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


    public void ShowUI(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void HideUI(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void PopUpTrigger(GameObject gameObject, bool active, KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            active = !active;
            gameObject.SetActive(active);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
}
