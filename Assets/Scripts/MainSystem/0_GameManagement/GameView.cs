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
    public bool[] contracts;


    private bool option;
    private bool pause;
    private bool techtree;

    private void Awake()
    {
        gamePresenter = GetComponent<IGamePresenter>();

        option = false;
        techtree = false;
        pause = false;
    }

    private void Start()
    {
        TextUIUpdate();
        HideUI(OptionUI);
        HideUI(TechTreeUI);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            option = !option;
            ActiveTrigger(OptionUI, option);
            Pause(option);
        }
        if (Input.GetKeyDown(KeyCode.T) && !pause)
        {
            techtree = !techtree;
            ActiveTrigger(TechTreeUI, techtree);
        }
    }

    public bool[] GetContracts()
    {
        return contracts;
    }
    public void TextUIUpdate()
    {
        Day.text = gamePresenter.GetDay();
        Money.text = gamePresenter.GetMoney();
    }

    public void ClockUpdate(string currentTime)
    {
        CurrentTime.text = currentTime;
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

    private void Pause(bool active)
    {
        pause = active;
        Time.timeScale = active ? 0 : 1;
    }
}
