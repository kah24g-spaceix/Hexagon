using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class GameModel : MonoBehaviour, IGameModel
{
    [SerializeField] private TextAsset _playerDataInit;
    private PlayerData _playerData;

    private PlayerSystemModel _playerSystemModel;
    private PlayerMaterialModel _playerMaterialModel;
    private PlayerPlantModel _playerPlantModel;
    private PlayerTechModel _playerTechModel;

    [HideInInspector] public bool isLoad;
    // �� ����� ����
    // PlayerData ������
    private void Awake()
    {
        if (isLoad)
            LoadGame();
        else
        {
            Debug.Log("init data");
            InitData();
        }

    }

    private void InitData()
    {
        PlayerData initData = JsonConvert.DeserializeObject<PlayerData>(_playerDataInit.text);
        SetData(initData);

        _playerSystemModel = new PlayerSystemModel(
            initData.P_SystemData.Money,
            initData.P_SystemData.Employees,
            initData.P_SystemData.Resistance,
            initData.P_SystemData.CommunityOpinionValue,
            initData.P_SystemData.Day);
        _playerMaterialModel = new PlayerMaterialModel(
            initData.P_MaterialData.Alloy,
            initData.P_MaterialData.Microchip,
            initData.P_MaterialData.CarbonFiber,
            initData.P_MaterialData.ConductiveFiber,
            initData.P_MaterialData.Pump,
            initData.P_MaterialData.RubberTube);
        _playerPlantModel = new PlayerPlantModel(
            
            );
        _playerTechModel = new PlayerTechModel(
            initData.P_TechData.TechPoint,
            initData.P_TechData.RevenueValue,
            initData.P_TechData.MaxEmployee,
            initData.P_TechData.TechLevels);
    }
    private void UpdatePlayerSaveData()
    {
        _playerData.P_SystemData = new PlayerSystemData(
            _playerSystemModel.Money,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue,
            _playerSystemModel.Day
            );
        _playerData.P_MaterialData = new PlayerMaterialData(
            _playerMaterialModel.Alloy,
            _playerMaterialModel.Microchip,
            _playerMaterialModel.CarbonFiber,
            _playerMaterialModel.ConductiveFiber,
            _playerMaterialModel.Pump,
            _playerMaterialModel.RubberTube
            );
        _playerData.P_TechData = new PlayerTechTreeData(
            _playerTechModel.TechPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels
            );
    }

    public PlayerSystemModel GetPlayerSystemModel() => _playerSystemModel;
    public PlayerMaterialModel GetPlayerMaterialModel() => _playerMaterialModel;
    public PlayerPlantModel GetPlayerPlantModel() => _playerPlantModel;
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
    public void DoPlantResult(PlayerPlantModel model)
    {
        _playerPlantModel = model;
        UpdatePlayerSaveData();
    }
    public void DoTechResult(PlayerTechModel model)
    {
        _playerTechModel = model;
        UpdatePlayerSaveData();
    }

    private readonly int defaultMoney = 100;
    private readonly int balanceValue = 10;
    public void Income()
    {
        int currentEmployees = _playerSystemModel.Employees;
        int maxIncome = Mathf.FloorToInt(_playerTechModel.MaxEmployee - (_playerSystemModel.Employees / balanceValue));
        int revenue = 1 + (_playerTechModel.RevenueValue / defaultMoney);

        int money = currentEmployees <= maxIncome
            ? _playerSystemModel.Money + (UnityEngine.Random.Range(currentEmployees, maxIncome) * defaultMoney * revenue)
            : _playerSystemModel.Money + (currentEmployees * defaultMoney);

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

        double randomValue = UnityEngine.Random.Range(0f, defaultMoney);
        int employees = randomValue <= (defaultMoney - _playerSystemModel.CommunityOpinionValue)
            ? Mathf.Min(_playerSystemModel.Employees + temp, _playerTechModel.MaxEmployee)
            : _playerSystemModel.Employees - temp;

        int resistance = randomValue > (defaultMoney - _playerSystemModel.CommunityOpinionValue) ? _playerSystemModel.Resistance + temp : _playerSystemModel.Resistance;

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

    public void Sell()
    {
        _playerSystemModel = new PlayerSystemModel(
            _playerSystemModel.Money + 10000,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue,
            _playerSystemModel.Day);



        _playerMaterialModel = new PlayerMaterialModel(
_playerMaterialModel.Alloy - 1,
_playerMaterialModel.Microchip - 1,
_playerMaterialModel.CarbonFiber - 1,
_playerMaterialModel.ConductiveFiber - 1,
_playerMaterialModel.Pump - 1,
_playerMaterialModel.RubberTube - 1);
        UpdatePlayerSaveData();
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
            data.P_SystemData.Money,
            data.P_SystemData.Employees,
            data.P_SystemData.Resistance,
            data.P_SystemData.CommunityOpinionValue,
            data.P_SystemData.Day
            );
        _playerMaterialModel = new PlayerMaterialModel(
            data.P_MaterialData.Alloy,
            data.P_MaterialData.Microchip,
            data.P_MaterialData.CarbonFiber,
            data.P_MaterialData.ConductiveFiber,
            data.P_MaterialData.Pump,
            data.P_MaterialData.RubberTube
            );
        _playerTechModel = new PlayerTechModel(
            data.P_TechData.TechPoint,
            data.P_TechData.RevenueValue,
            data.P_TechData.MaxEmployee,
            data.P_TechData.TechLevels
            );

        _playerData = data;
    }

}
