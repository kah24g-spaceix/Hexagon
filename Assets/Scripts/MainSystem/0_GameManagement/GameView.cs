using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameView : MonoBehaviour, IGameView
{
    [Header("Option UI")]
    [SerializeField] private GameObject OptionUI;

    [Header("UI Elements")]

    [SerializeField] private GameObject TechTreeUI;
    [SerializeField] private GameObject ToDayResultUI;
    //[SerializeField] private Button NextDayButton;
    //[SerializeField] private Button RestartDayButton;

    [Header("In Game Text")]
    [SerializeField] private TextMeshProUGUI Day;
    [SerializeField] private TextMeshProUGUI CurrentTime;
    [SerializeField] private TextMeshProUGUI Money;

    private IGamePresenter gamePresenter;

    private static bool GameIsPaused = false;
    private bool techtree;

    private void Awake()
    {
        gamePresenter = GetComponent<IGamePresenter>();
        techtree = false;
    }

    private void Start()
    {
        TextUIUpdate();
        HideUI(OptionUI);
        HideUI(TechTreeUI);


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ShowUI(OptionUI);
                Resume();
                GameIsPaused = false;
            }
            else
            {
                HideUI(OptionUI);
                Pause();
                GameIsPaused = true;
            }
        }
        if (!GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                techtree = !techtree;
                ActiveTrigger(TechTreeUI, techtree);
            }
        }
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
        CurrentTime.SetText(string.Format("{0:00} : {1:00}", hour, minute));
    }

    public void ShowToDayResult()
    {
        ToDayResultUI.gameObject.SetActive(true);
    }

    public void HideToDayResult()
    {
        ToDayResultUI.gameObject.SetActive(false);
    }

    public void ShowUI(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(true);
    }

    public void HideUI(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(false);
    }

    public void ActiveTrigger(GameObject gameObject, bool active)
    {
        gameObject.gameObject.SetActive(active);
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
