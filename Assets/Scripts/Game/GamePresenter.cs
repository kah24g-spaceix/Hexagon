using System.Collections;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GamePresenter : MonoBehaviour, IGamePresenter
{
    private IGameModel _model;
    private IGameView _gameView;
    private PlayerSaveModel _playerSaveModel;
    private PlayerTechModel _playerTechModel;

    GameManager _gameManager;
    Clock _clock;
    private void Awake()
    {
        _model = GetComponent<IGameModel>();
        _gameView = GetComponent<IGameView>();
        _gameManager = GetComponent<GameManager>();
        _clock = GetComponent<Clock>();
    }
    private void Start()
    {
        ReloadData();
        StartCoroutine(_clock.StartTimer());
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
        while (true)
        {
            _model.Income();
            ReloadData();
            yield return new WaitForSeconds(1f);
        }
    }
    public void OnBuyCommodityButton()
    {
        _model.BuyCommodity();
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

    public void NextDay()
    {
        StopCoroutine(MoneyPerSecond());
        _model.Motivation();
        SaveGame();
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