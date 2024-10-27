using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameModel : MonoBehaviour, IGameModel
{
    [SerializeField] private TextAsset _playerDataInit;
    private PlayerData _playerData;

    private PlayerSystemModel _playerSystemModel;
    private PlayerMaterialModel _playerMaterialModel;
    private PlayerTechModel _playerTechModel;
    // �� ����� ����
    // PlayerData ������
    private void Awake()
    {
        Debug.Log("init data");
        InitData();
    }

    private void InitData()
    {
        PlayerData initData = JsonConvert.DeserializeObject<PlayerData>(_playerDataInit.text);
        SetData(initData);

        _playerSystemModel = new PlayerSystemModel(initData.SystemData.Money, initData.SystemData.Employees, initData.SystemData.Resistance, initData.SystemData.CommunityOpinionValue, initData.SystemData.Day);
        _playerMaterialModel = new PlayerMaterialModel(initData.MaterialData.Alloy, initData.MaterialData.Microchip, initData.MaterialData.CarbonFiber, initData.MaterialData.ConductiveFiber, initData.MaterialData.Pump, initData.MaterialData.RubberTube);
        _playerTechModel = new PlayerTechModel(initData.TechData.TechPoint, initData.TechData.RevenueValue, initData.TechData.MaxEmployee, initData.TechData.TechLevels);
    }
    private void UpdatePlayerSaveData()
    {
        _playerData.SystemData = new PlayerSystemData(
            _playerSystemModel.Money,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue,
            _playerSystemModel.Day
            );
        _playerData.MaterialData = new PlayerMaterialData(
            _playerMaterialModel.Alloy,
            _playerMaterialModel.Microchip,
            _playerMaterialModel.CarbonFiber,
            _playerMaterialModel.ConductiveFiber,
            _playerMaterialModel.Pump,
            _playerMaterialModel.RubberTube
            );
        _playerData.TechData = new PlayerTechTreeData(
            _playerTechModel.TechPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels
            );
    }

    
    public PlayerSystemModel GetPlayerSaveModel() => _playerSystemModel;
    public PlayerMaterialModel GetPlayerCommodityModel() => _playerMaterialModel;
    public PlayerTechModel GetPlayerTechModel() => _playerTechModel;


    public void DoSystemResult(PlayerSystemModel model)
    {
        _playerSystemModel = model;
        UpdatePlayerSaveData();
    }

    public void DoMaterialResult(PlayerMaterialModel model)
    {
        _playerMaterialModel = model;
        UpdatePlayerSaveData();
    }

    public void DoTechResult(PlayerTechModel model)
    {
        _playerTechModel = model;
        UpdatePlayerSaveData();
        Debug.Log($"point {model.TechPoint}");
    }

    public void Income()
    {
        int currentEmployees = _playerSystemModel.Employees;
        int maxIncome = Mathf.FloorToInt(_playerTechModel.MaxEmployee - (_playerSystemModel.Employees / 10));
        int revenue = 1 + (_playerTechModel.RevenueValue / 100);

        int money = currentEmployees <= maxIncome
            ? _playerSystemModel.Money + (UnityEngine.Random.Range(currentEmployees, maxIncome) * 100 * revenue)
            : _playerSystemModel.Money + (currentEmployees * 100);

        _playerSystemModel = new PlayerSystemModel(money, _playerSystemModel.Employees, _playerSystemModel.Resistance, _playerSystemModel.CommunityOpinionValue, _playerSystemModel.Day);
        UpdatePlayerSaveData();
    }

    public void ExchangeTechPoint(int value)
    {
        _playerSystemModel = new PlayerSystemModel(
            _playerSystemModel.Money + (_playerSystemModel.Employees * 1000) * value,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue,
            _playerSystemModel.Day
        );

        _playerTechModel = new PlayerTechModel(
            _playerTechModel.TechPoint - value,
            _playerTechModel.RevenueValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels
        );
        UpdatePlayerSaveData();
    }

    public void TodayResult()
    {
        int techPoint = _playerTechModel.TechPoint + (50 + (_playerSystemModel.Day * 2));
        int temp = _playerSystemModel.Employees / 10;

        double randomValue = UnityEngine.Random.Range(0f, 100f);
        int employees = randomValue <= (100 - _playerSystemModel.CommunityOpinionValue)
            ? Mathf.Min(_playerSystemModel.Employees + temp, _playerTechModel.MaxEmployee)
            : _playerSystemModel.Employees - temp;

        int resistance = randomValue > (100 - _playerSystemModel.CommunityOpinionValue) ? _playerSystemModel.Resistance + temp : _playerSystemModel.Resistance;

        _playerSystemModel = new PlayerSystemModel(_playerSystemModel.Money, employees, resistance, _playerSystemModel.CommunityOpinionValue, _playerSystemModel.Day);
        _playerTechModel = new PlayerTechModel(techPoint, _playerTechModel.RevenueValue, _playerTechModel.MaxEmployee, _playerTechModel.TechLevels);
        UpdatePlayerSaveData();
    }

    public void NextDay()
    {
        _playerSystemModel = new PlayerSystemModel(
            _playerSystemModel.Money,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue,
            _playerSystemModel.Day + 1
        );
        UpdatePlayerSaveData();
    }

    //����
    public void AlloyFactory()
    {

    }
    public void MicrochipFactory()
    {

    }
    public void CarbonFiberFactory()
    {

    }
    public void ConductiveFiber()
    {

    }
    public void PumpFactory()
    {

    }
    public void RubberTubeFactory()
    {

    }

    public void SaveGame()
    {
        string json = JsonConvert.SerializeObject(_playerData);
        Debug.Log(json);
        PlayerPrefs.SetString("Save", json);
    }

    public bool LoadGame()
    {
        if (!PlayerPrefs.HasKey("Save"))
            return false;

        SetData(JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString("Save")));
        return true;
    }

    private void SetData(PlayerData data)
    {
        _playerSystemModel = new PlayerSystemModel(
            data.SystemData.Money,
            data.SystemData.Employees,
            data.SystemData.Resistance,
            data.SystemData.CommunityOpinionValue,
            data.SystemData.Day
            );
        _playerMaterialModel = new PlayerMaterialModel(
            data.MaterialData.Alloy,
            data.MaterialData.Microchip,
            data.MaterialData.CarbonFiber,
            data.MaterialData.ConductiveFiber,
            data.MaterialData.Pump, 
            data.MaterialData.RubberTube
            );
        _playerTechModel = new PlayerTechModel(
            data.TechData.TechPoint,
            data.TechData.RevenueValue,
            data.TechData.MaxEmployee,
            data.TechData.TechLevels
            );

        _playerData = data;
    }
}
