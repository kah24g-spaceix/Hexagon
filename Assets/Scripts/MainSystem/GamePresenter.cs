using System.Collections;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

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
        StartCoroutine(MoneyPerSecond());
    }
    public string GetDay()
    {
        return $"Day {_playerSaveModel.Day}";
    }
    public string GetMoney()
    {
        return $"{_playerSaveModel.Money} H$";
    }
    public IEnumerator MoneyPerSecond()
    {
        while(_dayCycle.currentTime > 0)
        {
            _model.Income();
            ReloadData();
            yield return new WaitForSeconds(1f);

            if (_dayCycle.currentTime <= 0)
            {
                yield break;
            }
        }

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
        LoadGame();
    }
    public void OnNextDayButton()
    {
        _model.NextDay();
        SaveGame();
    }
    public void OnRestartDayButton()
    {
        throw new System.NotImplementedException();
    }
    public void LoadGame()
    {
        _model.LoadGame();
        ReloadData();
    }
    public void SaveGame()
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