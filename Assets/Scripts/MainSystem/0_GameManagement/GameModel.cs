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
    private PlayerTechModel _playerTechModel;

    [HideInInspector] public bool isLoad;
    // 값 상수로 빼기
    // PlayerData 나누기
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
            initData.SystemData.Money,
            initData.SystemData.Employees,
            initData.SystemData.Resistance,
            initData.SystemData.CommunityOpinionValue,
            initData.SystemData.Day);
        _playerMaterialModel = new PlayerMaterialModel(
            initData.MaterialData.Alloy,
            initData.MaterialData.Microchip,
            initData.MaterialData.CarbonFiber,
            initData.MaterialData.ConductiveFiber,
            initData.MaterialData.Pump,
            initData.MaterialData.RubberTube);
        _playerTechModel = new PlayerTechModel(
            initData.TechData.TechPoint,
            initData.TechData.RevenueValue,
            initData.TechData.MaxEmployee,
            initData.TechData.TechLevels);
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
    public PlayerMaterialModel GetPlayerMaterialModel() => _playerMaterialModel;
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

    //공장

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
    public void UpdatePlantData(bool[] contracts)
    {
        var newModel = new PlayerMaterialModel(
Plant1(contracts[0]),
Plant2(contracts[1]),
Plant3(contracts[2]),
Plant4(contracts[3]),
Plant5(contracts[4]),
Plant6(contracts[5]));
        DoMaterialResult(newModel);
    }
    public int Plant1(bool contract)
    {
        if (contract)
            return _playerMaterialModel.Alloy + 1;

        return _playerMaterialModel.Alloy;
    }
    public int Plant2(bool contract)
    {
        if (contract)
            return _playerMaterialModel.Microchip + 1;

        return _playerMaterialModel.Microchip;
    }
    public int Plant3(bool contract)
    {
        if (contract)
            return _playerMaterialModel.CarbonFiber + 1;

        return _playerMaterialModel.CarbonFiber;
    }
    public int Plant4(bool contract)
    {
        if (contract)
            return _playerMaterialModel.ConductiveFiber + 1;

        return _playerMaterialModel.ConductiveFiber;
    }
    public int Plant5(bool contract)
    {
        if (contract)
            return _playerMaterialModel.Pump + 1;

        return _playerMaterialModel.Pump;
    }
    public int Plant6(bool contract)
    {
        if (contract)
            return _playerMaterialModel.RubberTube + 1;

        return _playerMaterialModel.RubberTube;
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
