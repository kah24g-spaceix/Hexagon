using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class GameView : MonoBehaviour, IGameView
{
    private enum MenuButton
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
    [SerializeField] private TextMeshProUGUI TechPoint;
    [SerializeField] private TextMeshProUGUI R_Value;
    [SerializeField] private TextMeshProUGUI E_Value;

    [Header("In Game Button")]
    [SerializeField] private Button DayCycleButton;
    [SerializeField] private Button DaySkipButton;
    [SerializeField] private Button ResetPopupLocation;
    

    [Header("In Game Popup")]
    [SerializeField] private GameObject InGameButtonHolder;
    private List<Button> InGameButtons;
    [SerializeField] private GameObject InGamePopupHolder;
    private List<GameObject> InGamePopups;
    
    [SerializeField] private Button TechPointChangeButton;
    [SerializeField] private GameObject TechPointChange;
    private TechChange techChange;
    
    
    [Header("To Day Result UI")]
    [SerializeField] private GameObject ToDayResultUI;
    public GameObject ToDayResult { get; private set; }
    [SerializeField] private Button NextDayButton;
    [SerializeField] private Button RestartDayButton;

    [Header("Last Day Result UI")]
    [SerializeField] private GameObject LastDayResultUI;
    public GameObject LastDayResult { get; private set; }
    [SerializeField] private Button NextButton;

    private IGamePresenter gamePresenter;

    private bool isDayCycleButton;
    private bool gameIsPaused;


    private void Awake()
    {
        gamePresenter = GetComponent<IGamePresenter>();
        techChange = TechPointChange.GetComponent<TechChange>();
        InitInGameUI();
        ToDayResult = ToDayResultUI;
        LastDayResult = LastDayResultUI;
    }
    private void InitInGameUI()
    {
        InGameButtons = new List<Button>(InGameButtonHolder.GetComponentsInChildren<Button>());
        InGamePopups = new List<GameObject>();
        foreach (Transform child in InGamePopupHolder.transform)
        {
            InGamePopups.Add(child.gameObject);
        }
    }
    private void Start()
    {
        TextUIUpdate();
        HideUI(MenuPopup);
        HideUI(OptionPopup);
        HideUI(ToDayResultUI);
        HideUI(LastDayResultUI);
        HideUI(TechPointChange);
        TechChange();
        for (int i = 0; i < InGamePopups.Count; i++)
        {
            int index = i;
            HideUI(InGamePopups[i]);
            InGameButtons[i].onClick.AddListener(() => PopupTriggerButton(InGamePopups[index]));
        }

        ResumeButton.onClick.AddListener(() => ButtonType(MenuButton.Resume));
        OptionButton.onClick.AddListener(() => ButtonType(MenuButton.Option));
        SaveButton.onClick.AddListener(() => ButtonType(MenuButton.Save));
        TitleButton.onClick.AddListener(() => ButtonType(MenuButton.Title));
        ExitButton.onClick.AddListener(() => ButtonType(MenuButton.Exit));

        DayCycleButton.onClick.AddListener(PauseButton);
        DaySkipButton.onClick.AddListener(gamePresenter.OnDaySkipButton);
        ResetPopupLocation.onClick.AddListener(() =>
        {
            for (int i = 0; i < InGamePopups.Count; i++)
            {
                int index = i;
                RectTransform popupRect = InGamePopups[index].GetComponent<RectTransform>();
                popupRect.anchoredPosition = Vector2.zero;
            }
        });

        TechPointChangeButton.onClick.AddListener(() => PopupTriggerButton(TechPointChange));


        NextDayButton.onClick.AddListener(() =>
            QuestionDialogUI.Instance.ShowQuestion(
                "Do you want to move forward to the next day?", () => { gamePresenter.OnNextDayButton(); HideUI(ToDayResult); }, () => { }));
        RestartDayButton.onClick.AddListener(() =>
            QuestionDialogUI.Instance.ShowQuestion(
                "Do you want to begin the day again?", () => { gamePresenter.OnRestartDayButton(); HideUI(ToDayResult); }, () => { }));
        NextButton.onClick.AddListener(()=>SceneManager.LoadScene("EndingScene"));
    }
    public void PauseButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select); 
        PauseTrigger(); 
        isDayCycleButton = !isDayCycleButton;
    }
    private void TechChange()
    {
        
        techChange.Change.onClick.AddListener(ChangeButton);
    }
    private void ChangeButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);

        techChange.slider.value = Mathf.Clamp(techChange.slider.value, techChange.slider.minValue, techChange.slider.maxValue);
        
        gamePresenter.OnChangeTechPoint(techChange.slider.value);
        techChange.slider.value = techChange.slider.minValue;
    }
    private void ButtonType(MenuButton buttonType)
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        switch (buttonType)
        {
            case MenuButton.Resume:
                HideUI(MenuPopup);
                gamePresenter.Resume();
                break;
            case MenuButton.Option:
                ShowUI(OptionPopup);
                break;
            case MenuButton.Save:
                QuestionDialogUI.Instance.ShowQuestion(
                    "Do you want to save your progress?", () => gamePresenter.DoSaveGame(false), () => { });
                break;
            case MenuButton.Title:
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
            case MenuButton.Exit:
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
        if (!MenuPopup.activeSelf) HandleInGameInput();
        ViewUpdate();
        HandlePauseInput();
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
        PopupTrigger(InGamePopups[0], KeyCode.Alpha1);
        PopupTrigger(InGamePopups[1], KeyCode.Alpha2);
        PopupTrigger(InGamePopups[2], KeyCode.Alpha3);
        PopupTrigger(InGamePopups[3], KeyCode.Alpha4);
        PopupTrigger(InGamePopups[4], KeyCode.Alpha5);
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
        techChange.slider.maxValue = gamePresenter.MaxTechChange(techChange.slider.maxValue);
    }
    public void TextUIUpdate()
    {
        Day.SetText(gamePresenter.GetDay());
        Money.SetText(gamePresenter.GetMoney());
        TechPoint.SetText(gamePresenter.GetTechPoint());
        techChange.techPoint.SetText($"{techChange.slider.value:N0}");
        techChange.price.SetText(gamePresenter.GetTechPointPrice(techChange.slider.value));
        R_Value.SetText(gamePresenter.GetR_Value());
        E_Value.SetText(gamePresenter.GetE_Value());
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
    public void PopupTrigger(GameObject UI, KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
            UI.SetActive(!UI.activeSelf);
        }
    }
    public void PopupTriggerButton(GameObject UI)
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        UI.SetActive(!UI.activeSelf);
    }


}
