using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.VersionControl.Asset;

public class GameModel : MonoBehaviour, IGameModel
{
    [SerializeField] private TextAsset _playerDataInit;
    private PlayerSaveData _playerSaveData;
    private PlayerSaveModel _playerSaveModel;
    private PlayerTechModel _playerTechModel;

    private void Awake()
    {
        Debug.Log("init data");
        InitData();
    }
    private void InitData()
    {
        PlayerSaveData initData = JsonConvert.DeserializeObject<PlayerSaveData>(_playerDataInit.text);
        SetData(initData);

        PlayerSaveModel saveModel = new PlayerSaveModel(
        initData.Money,
        initData.Commodity,
        initData.Employees,
        initData.Resistance,
        initData.CommunityOpinionValue,
        initData.Day);
        _playerSaveModel = saveModel;
        PlayerTechModel techModel = new PlayerTechModel(
        initData.TechPoint,
        initData.RevenueValue,
        initData.MaxEmployee,
        initData.TechLevels
            );
        _playerTechModel = techModel;
    }
    public PlayerSaveModel GetPlayerSaveModel()
    {
        return _playerSaveModel;
    }

    public PlayerTechModel GetPlayerTechModel()
    {
        return _playerTechModel;
    }

    public void DoPlayerInfoResult(PlayerSaveModel model)
    {
        PlayerSaveData newData = new PlayerSaveData(
        model.Money,
        model.Commodity,
        model.Employees,
        model.Resistance,
        model.CommunityOpinionValue,
        model.Day,
        _playerTechModel.TechPoint,
        _playerTechModel.RevenueValue,
        _playerTechModel.MaxEmployee,
        _playerTechModel.TechLevels);

        _playerSaveModel = model;
        _playerSaveData = newData;
    }

    public void DoTechResult(PlayerTechModel model)
    {
        PlayerSaveData newData = new PlayerSaveData(
        _playerSaveModel.Money,
        _playerSaveModel.Commodity,
        _playerSaveModel.Employees,
        _playerSaveModel.Resistance,
        _playerSaveModel.CommunityOpinionValue,
        _playerSaveModel.Day,
        model.TechPoint,
        model.RevenueValue,
        model.MaxEmployee,
        model.TechLevels);
        Debug.Log($"point {model.TechPoint}");

        _playerTechModel = model;
        _playerSaveData = newData;
    }
    public void Income()
    {
        int money = _playerSaveModel.Money
            + _playerSaveModel.Employees
            * (UnityEngine.Random.Range(_playerSaveModel.Employees, _playerTechModel.MaxEmployee)
            * 100000);
        PlayerSaveModel model = new PlayerSaveModel(
            money, 
            _playerSaveModel.Commodity,
            _playerSaveModel.Employees,
            _playerSaveModel.Resistance,
            _playerSaveModel.CommunityOpinionValue,
            _playerSaveModel.Day);
        DoPlayerInfoResult(model);
    }
    public void BuyCommodity()
    {
        int cost = (_playerSaveModel.Employees / 10) * 1000;
        if (_playerSaveModel.Money >= cost)
        {
            int money = _playerSaveModel.Money - cost;
            int commodity = _playerSaveModel.Employees * 100;
            PlayerSaveModel model = new PlayerSaveModel(
                money,
                commodity,
                _playerSaveModel.Employees,
                _playerSaveModel.Resistance,
                _playerSaveModel.CommunityOpinionValue,
                _playerSaveModel.Day);
            DoPlayerInfoResult(model);
        }
        else
        {
            Debug.Log("lack of money");
        }
    }
    public void ExchangeTechPoint(int value)
    {
        int money = (_playerSaveModel.Employees * 1000) * value;
        int techPoint = _playerTechModel.TechPoint - value;

        PlayerSaveData newData = new PlayerSaveData
            (
            money,
            _playerSaveModel.Commodity,
            _playerSaveModel.Employees,
            _playerSaveModel.Resistance,
            _playerSaveModel.CommunityOpinionValue,
            _playerSaveModel.Day,
            techPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels
        );

        _playerSaveData = newData;
    }
    public void Motivation()
    {
        int day = _playerSaveModel.Day + 1;
        int techPoint = _playerTechModel.TechPoint + (50 + (_playerSaveModel.Day * 2));
        int employees = 0;
        int resistance = 0;

        int temp = _playerSaveModel.Employees / 10;
        double randomValue = UnityEngine.Random.Range(0f, 100f);
        if (randomValue <= (100 - _playerSaveModel.CommunityOpinionValue))
        {
            if (_playerSaveModel.Employees + temp <= _playerTechModel.MaxEmployee)
            {
                employees = _playerSaveModel.Employees + temp;
            }
        }
        else
        {
            employees = _playerSaveModel.Employees - temp;
            resistance = _playerSaveModel.Resistance + temp;
        }

        PlayerSaveData newData = new PlayerSaveData(
            _playerSaveModel.Money,
            _playerSaveModel.Commodity,
            employees,
            resistance,
            _playerSaveModel.CommunityOpinionValue,
            day,
            techPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels
            );

        _playerSaveData = newData;
    }
    public void SaveGame()
    {
        PlayerSaveData saveData = _playerSaveData;
        string json = JsonConvert.SerializeObject(saveData);
        Debug.Log(json);
        PlayerPrefs.SetString("Save", json);
    }
    public bool LoadGame()
    {
        if (!PlayerPrefs.HasKey("Save"))
            return false;
        string value = PlayerPrefs.GetString("Save");
        PlayerSaveData saveData = JsonConvert.DeserializeObject<PlayerSaveData>(value);
        SetData(saveData);
        return true;
    }
    private void SetData(PlayerSaveData data) 
    {
        int money = data.Money;
        int commodity = data.Commodity;
        int employees = data.Employees;
        int resistance = data.Resistance;
        int day = data.Day;
        int techPoint = data.TechPoint;
        int revenueValue = data.RevenueValue;
        double communityOpinionValue = data.CommunityOpinionValue;
        int maxEmployees = data.MaxEmployee;
        int[] techLevels = data.TechLevels;
        PlayerSaveData newData = new PlayerSaveData(
            money,
            commodity,
            employees,
            resistance,
            communityOpinionValue,
            day,
            techPoint,
            revenueValue,
            maxEmployees,
            techLevels);

        _playerSaveData = newData;

        Debug.Log($"Money:{money}commodity:{commodity}employees:{employees}resistance:{resistance}day:{day}tech point:{techPoint}revenueValue{revenueValue}maxEmployees{maxEmployees}techLevels{techLevels}");
    }
}