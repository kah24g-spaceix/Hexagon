using System.Collections;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IGamePresenter
{
    private IGameModel _model;
    private IGameView _gameView;
    private PlayerSystemModel _playerSaveModel;
    private PlayerTechModel _playerTechModel;

    GameManager _gameManager;
    DayCycle _dayCycle;
    private void Awake()
    {
        _model = GetComponent<IGameModel>();
        _gameView = GetComponent<IGameView>();
        _gameManager = GetComponent<GameManager>();
        _dayCycle = GetComponent<DayCycle>();
    }
    private void Start()
    {
        ReloadData();
        StartCoroutine(_dayCycle.StartDayCycle());
    }

    public IEnumerator DataChangeUpdate()
    {
        MoneyPerSecond();
        DoUpdatePlant();
    }
    public string GetDay()
    {
        return $"Day {_playerSaveModel.Day}";
    }
    public string GetMoney()
    {
        return $"{_playerSaveModel.Money} H$";
    }
    public void MoneyPerSecond()
    {
        _model.Income();
        ReloadData();
    }
    public void DoUpdatePlant()
    {

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
        DoLoadGame();
    }
    public void OnNextDayButton()
    {
        _model.NextDay();
        DoSaveGame();
    }

    public void OnRestartDayButton()
    {
        throw new System.NotImplementedException();
    }


    public void DoLoadGame()
    {
        _model.LoadGame();
        ReloadData();
    }
    public void DoSaveGame()
    {
        _model.SaveGame();
    }
    private void ReloadData()
    {
        _playerSaveModel = _model.GetPlayerSaveModel();
        _playerTechModel = _model.GetPlayerTechModel();

        _gameView.TextUIUpdate();
    }


}