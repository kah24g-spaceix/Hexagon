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
    private void TimerStop()
    {
        StopCoroutine(_dayCycle.DayCycle());
        StopCoroutine(_dayCycle.SystemUpdate());
    }
    private void TimerStart()
    {
        StartCoroutine(_dayCycle.DayCycle());
        StartCoroutine(_dayCycle.SystemUpdate());
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
    public string GetDay()
    {
        return $"Day {_playerDayModel.Day}";
    }
    public string GetMoney()
    {
        return $"{_playerSystemModel.Money:N0} $";
    }
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
        _model.TodayResult();
        ReloadData();
    }
    public void DoLastDayResult()
    {
        ReloadData();
    }
    public void OnDaySkipButton()
    {
        
    }
    public void OnNextDayButton()
    {
        Resume();
        _model.NextDay();
        ReloadData();
        DoSaveGame(true);
        DoSaveGame(false);
        TimerStart();
    }
    public void OnRestartDayButton()
    {
        Resume();
        DoLoadGame(true);
        TimerStart();
    }
    public void DoLoadGame(bool useDateData)
    {
        if (!_model.LoadGame(useDateData))
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

    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
}