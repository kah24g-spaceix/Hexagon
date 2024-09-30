using Newtonsoft.Json;
using UnityEngine;

public class GameModel : MonoBehaviour, IGameModel
{
    private PlayerSaveData _playerSaveData;
    private PlayerSaveModel _playerSaveModel;
    private PlayerTechModel _playerTechModel;

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
        model.Day,
        _playerTechModel.TechPoint,
        _playerTechModel.RevenueValue,
        _playerTechModel.CommunityOpinionValue,
        _playerTechModel.MaxEmployee,
        _playerTechModel.TechLevels);

        _playerSaveData = newData;
    }

    public void DoTechResult(PlayerTechModel model)
    {
        PlayerSaveData newData = new PlayerSaveData(
        _playerSaveModel.Money,
        _playerSaveModel.Commodity,
        _playerSaveModel.Employees,
        _playerSaveModel.Resistance,
        _playerSaveModel.Day,
        model.TechPoint,
        model.RevenueValue,
        model.CommunityOpinionValue,
        model.MaxEmployee,
        model.TechLevels);

        _playerSaveData = newData;
    }
    public void Income()
    {
        int money = _playerSaveModel.Money
            + _playerSaveModel.Employees
            * (UnityEngine.Random.Range(_playerSaveModel.Employees, _playerTechModel.MaxEmployee)
            * 100000);
        PlayerSaveData newData = new PlayerSaveData(
            money,
            _playerSaveModel.Commodity,
            _playerSaveModel.Employees,
            _playerSaveModel.Resistance,
            _playerSaveModel.Day,
            _playerTechModel.TechPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.CommunityOpinionValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels);

        _playerSaveData = newData;
    }
    public void BuyCommodity()
    {
        int cost = (_playerSaveModel.Employees / 10) * 1000;
        if (_playerSaveModel.Money >= cost)
        {
            int money = _playerSaveModel.Money - cost;
            int commodity = _playerSaveModel.Employees * 100;
            PlayerSaveData newData = new PlayerSaveData(
            money,
            commodity,
            _playerSaveModel.Employees,
            _playerSaveModel.Resistance,
            _playerSaveModel.Day,
            _playerTechModel.TechPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.CommunityOpinionValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels);

            _playerSaveData = newData;
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
            _playerSaveModel.Day,
            techPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.CommunityOpinionValue,
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
        if (randomValue <= (100 - _playerTechModel.CommunityOpinionValue))
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
            day,
            techPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.CommunityOpinionValue,
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
        int money = saveData.Money;
        int commodity = saveData.Commodity;
        int employees = saveData.Employees;
        int resistance = saveData.Resistance;
        int day = saveData.Day;
        int techPoint = saveData.TechPoint;
        int revenueValue = saveData.RevenueValue;
        double communityOpinionValue = saveData.CommunityOpinionValue;
        int maxEmployees = saveData.MaxEmployee;
        int[] techLevels = saveData.TechLevels;
        _playerSaveData = new PlayerSaveData(
            money,
            commodity,
            employees,
            resistance,
            day,
            techPoint,
            revenueValue,
            communityOpinionValue,
            maxEmployees,
            techLevels);
        return true;
    }
}