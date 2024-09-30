using System.Collections;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IGamePresenter
{
    private IGameModel _model;
    private IGameView _gameView;
    private PlayerSaveModel _playerSaveModel;
    private PlayerTechModel _playerTechModel;

    GameManager _gameManager;
    private void Awake()
    {
        _model = GetComponent<IGameModel>();
    }
    private void Start()
    {
        ReloadData();
        StartCoroutine(MoneyPerSecond());
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
    private void ReloadData()
    {
        _playerSaveModel = _model.GetPlayerSaveModel();
        _playerTechModel = _model.GetPlayerTechModel();
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
}