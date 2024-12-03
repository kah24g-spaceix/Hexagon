using System.Collections;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IGamePresenter
{
    private IGameModel _model;
    private IGameView _gameView;
    private PlayerSystemModel _playerSaveModel;
    private PlayerMaterialModel _playerMaterialModel;
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
        StartCoroutine(DataChangeUpdate());
    }

    public IEnumerator DataChangeUpdate()
    {
        while (_dayCycle.currentTime >= 0)
        {
            //MoneyPerSecond();
            DoUpdatePlant();
            ReloadData();
            yield return new WaitForSeconds(1f);
        }

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

    public void DoSell()
    {
        _model.Sell();
        ReloadData();
    }
    public void DoUpdatePlant()
    {
        _model.UpdatePlantData(_gameView.GetContracts());
        ReloadData();
    }
    public string[] GetPlantText()
    {
        string[] text = new string[6] 
        {
            $"{_playerMaterialModel.Alloy}",
            $"{_playerMaterialModel.Microchip}",
            $"{_playerMaterialModel.CarbonFiber}",
            $"{_playerMaterialModel.ConductiveFiber}",
            $"{_playerMaterialModel.Pump}",
            $"{_playerMaterialModel.RubberTube}"
        };

        return text;
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
        ReloadData();
    }
    public void OnNextDayButton()
    {
        _model.NextDay();
        DoSaveGame();
        ReloadData();
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
        _playerSaveModel = _model.GetPlayerSystemModel();
        _playerMaterialModel = _model.GetPlayerMaterialModel();
        _playerTechModel = _model.GetPlayerTechModel();


        _gameView.TextUIUpdate();
    }


}