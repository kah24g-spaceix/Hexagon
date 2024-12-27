using System.Collections;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IGamePresenter
{
    private IGameModel _model;
    private PlayerSystemModel _playerSystemModel;
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
        StartCoroutine(_dayCycle.DayCycle());
        StartCoroutine(_dayCycle.SystemUpdate());
    }

    public void SystemUpdate()
    {
        MoneyPerSecond();
    }
    public string GetDay()
    {
        return $"Day {_playerSystemModel.Day}";
    }
    public string GetMoney()
    {
        return $"{_playerSystemModel.Money} H$";
    }
    public void MoneyPerSecond()
    {
        _model.Income();
        ReloadData();
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
    public void OnDaySkipButton()
    {
        _model.SetTimeScale(10);
    }
    public void OnNextDayButton()
    {
        _model.NextDay();
        DoSaveGame(true);
        ReloadData();
    }
    public void OnRestartDayButton()
    {
        DoLoadGame(true);
    }
    public void DoLoadGame(bool useDateData)
    {
        _model.LoadGame(useDateData);
        ReloadData();
    }
    public void DoSaveGame(bool useDateData)
    {
        _model.SaveGame(useDateData);
    }
    private void ReloadData()
    {
        _playerSystemModel = _model.GetPlayerSystemModel();
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