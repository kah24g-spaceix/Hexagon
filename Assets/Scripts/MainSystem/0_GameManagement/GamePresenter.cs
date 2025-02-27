using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IGamePresenter
{
    private IGameModel _model;
    private PlayerSystemModel _playerSystemModel;
    private PlayerDayModel _playerDayModel;
    private PlayerMaterialModel _playerMaterialModel;
    private PlayerTechModel _playerTechModel;
    GameDateManager _dayCycle;

    private bool isDayCycleRunning = false;
    private void Awake()
    {
        _model = GetComponent<IGameModel>();
        _dayCycle = GetComponent<GameDateManager>();
    }
    private void Start()
    {
        ReloadData();
        TimerStart();
    }
    private void Update()
    {
        ReloadData();
    }
    private Coroutine dayCycleCoroutine;
    private Coroutine systemUpdateCoroutine;

    private void TimerStart()
    {
        
        if (!isDayCycleRunning)
        {
            dayCycleCoroutine = StartCoroutine(_dayCycle.DayCycle());
            systemUpdateCoroutine = StartCoroutine(_dayCycle.SystemUpdate());
            isDayCycleRunning = true;
        }
    }

    private void TimerStop()
    {
        if (isDayCycleRunning)
        {
            if(dayCycleCoroutine != null)
                StopCoroutine(dayCycleCoroutine);
            if(systemUpdateCoroutine != null)
                StopCoroutine(systemUpdateCoroutine);
            isDayCycleRunning = false;
        }
    }

    public void SystemUpdate()
    {
        SystemSkipUpdate(1);
    }
    public void SystemSkipUpdate(float skipTime)
    {
        _model.Income(skipTime);
        ReloadData();
    }
    public string GetDay() => $"Day {_playerDayModel.Day}";
    public string GetMoney() => $"{_playerSystemModel.Money:N0} $";
    public void OnExchangeTechPointButton(int value)
    {
        if (_playerTechModel.TechPoint == 0)
        {
            _model.ExchangeTechPoint(value);
        }
        else
        {
            Debug.Log("Tech points are 0 and cannot be exchanged");
        }
        ReloadData();
    }

    public void DoTodayResult()
    {
        TimerStop();
        _model.TodayResult();
        _model.NextDay();
        ReloadData();
        Pause();
    }
    public void DoLastDayResult()
    {
        ReloadData();
        _model.ResetData();
    }
    public void OnDaySkipButton()
    {
        
    }
    public void OnNextDayButton()
    {
        DoSaveGame(true);
        DoSaveGame(false);
        TimerStart();
        ReloadData();
        Resume();
    }
    public void OnRestartDayButton()
    {
        Resume();
        DoLoadGame(true);
        TimerStart();
        ReloadData();
    }
    public void DoLoadGame(bool isDayData)
    {
        if (!_model.LoadGame(isDayData))
            _model.InitData();
        ReloadData();
    }
    public void DoSaveGame(bool useDateData)
    {
        _model.SaveGame(useDateData);
    }
    private void ReloadData()
    {
        _playerSystemModel = _model.GetPlayerSystemModel();
        _playerDayModel = _model.GetPlayerDayModel();
        _playerMaterialModel = _model.GetPlayerMaterialModel();
        _playerTechModel = _model.GetPlayerTechModel();
    }
    public void Resume() => Time.timeScale = 1;

    public void Pause() => Time.timeScale = 0;
}