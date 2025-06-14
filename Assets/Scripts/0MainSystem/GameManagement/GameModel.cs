using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GameModel : MonoBehaviour, IGameModel
{
    [SerializeField] private TextAsset _playerDataInit;
    private PlayerData _playerData;

    private PlayerSystemModel _playerSystemModel;
    private PlayerDayModel _playerDayModel;
    private PlayerMaterialModel _playerMaterialModel;
    private PlayerHyperFrameModel _playerHyperFrameModel;

    private PlayerFactoryModel _playerFactoryModel;
    private PlayerFactoryContractModel _playerFactoryContractModel;

    private PlayerTechModel _playerTechModel;
    private PlayerCurrentInfoModel _playerCurrentInfoModel;
    private GameSettings _gameSettings;
    private readonly int defaultMoney = 1;
    private readonly float revenueMultiplier = 0.1f;

    private void Awake()
    {
        InitGame();
    }
    public void InitGame()
    {
        _gameSettings = new GameSettings();
        var simulationManager = GameStateManager.Instance;
        ApplySimulationManagerSettings(simulationManager);
        if (_gameSettings.IsLoad)
        {
            if (!LoadGame(false))
            {
                Debug.LogError("Failed to load the game data. Initializing data instead.");
                return;
            }
        }
        else
        {
            Debug.Log("GameModel Awake - Initializing Data...");
            InitData();
            ResetTime();
        }
    }
    private void ApplySimulationManagerSettings(GameStateManager simulationManager)
    {
        if (simulationManager == null)
        {
            Debug.LogError("simulationManager is null!");
            _gameSettings.IsLoad = false;
            _gameSettings.IsStoryMode = false;
            _gameSettings.DailyPlaytime = 2400;
            _gameSettings.LastDay = 10;
            _gameSettings.InitialMoney = 0;
            return;
        }
        _gameSettings.IsLoad = simulationManager.IsLoad;
        _gameSettings.IsStoryMode = simulationManager.IsStoryMode;
        _gameSettings.DailyPlaytime = simulationManager.Playtime;
        _gameSettings.LastDay = simulationManager.LastDay;
        _gameSettings.InitialMoney = simulationManager.InitialMoney;
        Debug.Log(
            $"Simulation settings applied: IsStoryMode: {_gameSettings.IsStoryMode}, " +
            $"DailyPlaytime: {_gameSettings.DailyPlaytime}, " +
            $"LastDay: {_gameSettings.LastDay}, " +
            $"InitialMoney: {_gameSettings.InitialMoney}");
    }
    #region GameProgress
    public void InitData()
    {
        PlayerData initData = JsonConvert.DeserializeObject<PlayerData>(_playerDataInit.text);
        if (initData == null)
        {
            Debug.LogError("PlayerData deserialization failed. Check _playerDataInit.");
            return;
        }

        if (initData.P_SystemData == null || initData.P_ProductData == null)
        {
            Debug.LogError("One or more fields in PlayerData are null. Check JSON structure.");
            return;
        }
        SetData(initData);
        IsSimulationMode();
    }

    private void IsSimulationMode()
    {
        Debug.Log($"{_gameSettings.IsLoad} {_gameSettings.IsStoryMode} {_gameSettings.DailyPlaytime} {_gameSettings.LastDay} {_gameSettings.InitialMoney}");
        if (_gameSettings.IsStoryMode)
        {
            Debug.Log("Story Mode");
            return;
        }
        if (_gameSettings.DailyPlaytime == 0 && _gameSettings.LastDay == 0 && _gameSettings.InitialMoney == 0)
        {
            Debug.LogWarning("Simulation Initial Value is 0");
            return;
        }
        _playerSystemModel = new PlayerSystemModel(
            _gameSettings.InitialMoney,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue
        );
        _playerDayModel = new PlayerDayModel(
            _playerDayModel.Day,
            _playerDayModel.CurrentTime,
            _gameSettings.LastDay,
            _gameSettings.DailyPlaytime

        );

        UpdatePlayerSaveData();
    }
    private void UpdatePlayerSaveData()
    {
        if (_playerSystemModel == null || _playerDayModel == null || _playerMaterialModel == null ||
            _playerHyperFrameModel == null || _playerFactoryModel == null || _playerFactoryContractModel == null ||
            _playerTechModel == null)
        {
            Debug.LogError("Cannot update save data. Some models are not initialized.");
            return;
        }

        _playerData.P_SystemData = new PlayerSystemData(
            _playerSystemModel.Money,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue
        );
        _playerData.P_DayData = new PlayerDayData(
            _playerDayModel.Day,
            _playerDayModel.CurrentTime,
            _playerDayModel.LastDay,
            _playerDayModel.DayLength
        );
        _playerData.P_ProductData = new PlayerProductData(
            _playerMaterialModel.Alloy,
            _playerMaterialModel.Microchip,
            _playerMaterialModel.CarbonFiber,
            _playerMaterialModel.ConductiveFiber,
            _playerMaterialModel.Pump,
            _playerMaterialModel.RubberTube
        );
        _playerData.P_HyperFrameData = new PlayerHyperFrameData(
            _playerHyperFrameModel.Eye,
            _playerHyperFrameModel.Arm,
            _playerHyperFrameModel.Head,
            _playerHyperFrameModel.Lag,
            _playerHyperFrameModel.Foot,
            _playerHyperFrameModel.Body,
            _playerHyperFrameModel.Head
        );

        _playerData.P_FactoryData = new PlayerFactoryData(
            _playerFactoryModel.UpgradeCosts,
            _playerFactoryModel.Products,
            _playerFactoryModel.Levels,
            _playerFactoryModel.IsConstructions
        );
        _playerData.P_FactoryContractData = new PlayerFactoryContractData(
            _playerFactoryContractModel.Costs,
            _playerFactoryContractModel.Products,
            _playerFactoryContractModel.IsContracts
        );

        _playerData.P_TechData = new PlayerTechTreeData(
            _playerTechModel.TechPoint,
            _playerTechModel.RevenueValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels
        );
        _playerData.P_CurrentInfo = new PlayerCurrentInfo(
            _playerCurrentInfoModel.SalesRevenue,
            _playerCurrentInfoModel.OtherRevenue,
            _playerCurrentInfoModel.OtherIncome,
            _playerCurrentInfoModel.Salary,
            _playerCurrentInfoModel.CostSpent
        );
        _playerData.P_Setting = new PlayerSetting(
            _gameSettings.DailyPlaytime,
            _gameSettings.LastDay,
            _gameSettings.InitialMoney
        );
    }
    private void SetData(PlayerData data)
    {
        if (data == null)
        {
            Debug.LogError("SetData received null data. Aborting.");
            return;
        }
        _playerSystemModel = new PlayerSystemModel(
            data.P_SystemData.Money,
            data.P_SystemData.Employees,
            data.P_SystemData.Resistance,
            data.P_SystemData.CommunityOpinionValue
        );
        _playerDayModel = new PlayerDayModel(
            data.P_DayData.Day,
            data.P_DayData.CurrentTime,
            data.P_DayData.LastDay,
            data.P_DayData.DayLength
        );
        _playerMaterialModel = new PlayerMaterialModel(
            data.P_ProductData.Alloy,
            data.P_ProductData.Microchip,
            data.P_ProductData.CarbonFiber,
            data.P_ProductData.ConductiveFiber,
            data.P_ProductData.Pump,
            data.P_ProductData.RubberTube
        );
        _playerHyperFrameModel = new PlayerHyperFrameModel(
            data.P_HyperFrameData.Eye,
            data.P_HyperFrameData.Arm,
            data.P_HyperFrameData.Hand,
            data.P_HyperFrameData.Lag,
            data.P_HyperFrameData.Foot,
            data.P_HyperFrameData.Body,
            data.P_HyperFrameData.Head
        );
        _playerFactoryModel = new PlayerFactoryModel(
            data.P_FactoryData.UpgradeCosts,
            data.P_FactoryData.Products,
            data.P_FactoryData.Levels,
            data.P_FactoryData.IsContructions
        );
        _playerFactoryContractModel = new PlayerFactoryContractModel(
            data.P_FactoryContractData.Costs,
            data.P_FactoryContractData.Products,
            data.P_FactoryContractData.IsContracts
        );
        _playerTechModel = new PlayerTechModel(
            data.P_TechData.TechPoint,
            data.P_TechData.RevenueValue,
            data.P_TechData.MaxEmployee,
            data.P_TechData.TechLevels
        );
        _playerCurrentInfoModel = new PlayerCurrentInfoModel(
            data.P_CurrentInfo.SalesRevenue,
            data.P_CurrentInfo.OtherRevenue,
            data.P_CurrentInfo.OtherIncome,
            data.P_CurrentInfo.Salary,
            data.P_CurrentInfo.CostSpent
        );

        if (_gameSettings.IsLoad)
        {
            _gameSettings.DailyPlaytime = data.P_Setting.DailyPlaytime;
            _gameSettings.LastDay = data.P_Setting.LastDay;
            _gameSettings.InitialMoney = data.P_Setting.InitialMoney;
        }


        _playerData = data;
    }
    public void SaveGame(bool isDayData)
    {
        string key;
        if (_gameSettings.IsStoryMode) key = isDayData ? "StoryDaySave" : "StorySave";
        else key = isDayData ? "DaySave" : "Save";

        string json = JsonConvert.SerializeObject(_playerData);
        Debug.Log($"Saving game data for key: {key}");
        PlayerPrefs.SetString(key, json);
    }

    public bool LoadGame(bool isDayData)
    {
        string key;
        if (_gameSettings.IsStoryMode) key = isDayData ? "StoryDaySave" : "StorySave";
        else key = isDayData ? "DaySave" : "Save";

        if (!PlayerPrefs.HasKey(key))
        {
            Debug.LogWarning($"No save data found for key: {key}");
            return false;
        }

        try
        {
            string json = PlayerPrefs.GetString(key);
            SetData(JsonConvert.DeserializeObject<PlayerData>(json));
            Debug.Log($"Loaded game data for key: {key}");
        }
        catch (JsonException ex)
        {
            Debug.LogError($"Failed to deserialize save data for key {key}: {ex.Message}");
            return false;
        }
        return true;
    }
    public void ResetData()
    {
        if (_gameSettings.IsStoryMode)
        {
            PlayerPrefs.DeleteKey("StoryDaySave");
            PlayerPrefs.DeleteKey("StorySave");
        }
        else
        {
            PlayerPrefs.DeleteKey("DaySave");
            PlayerPrefs.DeleteKey("Save");
        }
    }
    public void TodayResult()
    {
        int techPoint = _playerTechModel.TechPoint + 50 + (_playerDayModel.Day * 2);
        int temp = _playerSystemModel.Employees / 10;

        double randomValue = UnityEngine.Random.Range(0f, defaultMoney);
        int employees = randomValue <= (defaultMoney - _playerSystemModel.CommunityOpinionValue)
            ? Mathf.Min(_playerSystemModel.Employees + temp, _playerTechModel.MaxEmployee)
            : _playerSystemModel.Employees - temp;

        int resistance = randomValue > (defaultMoney - _playerSystemModel.CommunityOpinionValue) ? _playerSystemModel.Resistance + temp : _playerSystemModel.Resistance;

        _playerSystemModel = new PlayerSystemModel(_playerSystemModel.Money, employees, resistance, _playerSystemModel.CommunityOpinionValue);
        _playerTechModel = new PlayerTechModel(techPoint, _playerTechModel.RevenueValue, _playerTechModel.MaxEmployee, _playerTechModel.TechLevels);
        UpdatePlayerSaveData();
    }
    public void LastDayResult()
    {
        UpdatePlayerSaveData();
    }
    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    public void NextDay()
    {
        _playerDayModel = new PlayerDayModel(
            _playerDayModel.Day + 1,
            _playerDayModel.CurrentTime,
            _playerDayModel.LastDay,
            _playerDayModel.DayLength
        );

        ProcessContractCancellations();
        UpdatePlayerSaveData();
    }
    public void ResetTime()
    {
        _playerDayModel = new PlayerDayModel(
            _playerDayModel.Day,
            _playerDayModel.DayLength,
            _playerDayModel.LastDay,
            _playerDayModel.DayLength
        );
        UpdatePlayerSaveData();
    }
    #endregion

    #region PlayerSystemModel
    public PlayerSystemModel GetPlayerSystemModel() => _playerSystemModel;
    public void DoSystemResult(PlayerSystemModel model)
    {
        _playerSystemModel = model;
        UpdatePlayerSaveData();
    }
    public PlayerDayModel GetPlayerDayModel() => _playerDayModel;
    public void DoDayResult(PlayerDayModel model)
    {
        _playerDayModel = model;
        UpdatePlayerSaveData();
    }
    public void Income(float skipTime)
    {
        int currentEmployees = _playerSystemModel.Employees;
        float revenue = 1 + (_playerTechModel.RevenueValue * revenueMultiplier);
        long money = _playerSystemModel.Money + Mathf.FloorToInt(currentEmployees * defaultMoney * revenue * skipTime);

        _playerSystemModel = new PlayerSystemModel(money, _playerSystemModel.Employees, _playerSystemModel.Resistance, _playerSystemModel.CommunityOpinionValue);
        UpdatePlayerSaveData();
    }

    readonly private int pointPrice = 1000;
    public void ChangeTechPoint(float value)
    {
        long money = _playerSystemModel.Money - GetTechPointPrice() * (long)value;
        if (money <= 0) 
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.Error);
            return;
        }
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Buy);
        _playerSystemModel = new PlayerSystemModel(
            money,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue
        );

        _playerTechModel = new PlayerTechModel(
            _playerTechModel.TechPoint + (int)value,
            _playerTechModel.RevenueValue,
            _playerTechModel.MaxEmployee,
            _playerTechModel.TechLevels
        );
        UpdatePlayerSaveData();
    }
    public long GetTechPointPrice()
    {
        if (_playerSystemModel.Employees == 0)
            return pointPrice;
        else
            return _playerSystemModel.Employees * pointPrice;
    }
    #endregion

    #region PlayerMaterialModel
    public PlayerMaterialModel GetPlayerMaterialModel() => _playerMaterialModel;
    public void DoMaterialResult(PlayerMaterialModel model)
    {
        _playerMaterialModel = model;
        UpdatePlayerSaveData();
    }
    #endregion

    #region PlayerHyperFrameModel

    public PlayerHyperFrameModel GetPlayerHyperFrameModel() => _playerHyperFrameModel;
    public void DoHyperFrameResult(PlayerHyperFrameModel model)
    {
        _playerHyperFrameModel = model;
        UpdatePlayerSaveData();
    }
    #endregion

    #region PlayerFactoryModel
    public PlayerFactoryModel GetPlayerFactoryModel() => _playerFactoryModel;
    public void DoFactoryResult(PlayerFactoryModel model)
    {
        _playerFactoryModel = model;
        UpdatePlayerSaveData();
    }
    public PlayerFactoryContractModel GetPlayerFactoryContractModel() => _playerFactoryContractModel;
    public void DoFactoryContractResult(PlayerFactoryContractModel model)
    {
        _playerFactoryContractModel = model;
        UpdatePlayerSaveData();
    }
    private void ProcessContractCancellations()
    {
        FactoryModel currentFactoryModel = FactoryGroup.Instance.Model;

        for (int i = 0; i < currentFactoryModel.PendingContractCancellations.Length; i++)
        {
            if (currentFactoryModel.PendingContractCancellations[i])
            {
                _playerSystemModel = new PlayerSystemModel
                (
                    _playerSystemModel.Money - currentFactoryModel.ContractCosts[i],
                    _playerSystemModel.Employees,
                    _playerSystemModel.Resistance,
                    _playerSystemModel.CommunityOpinionValue
                );

                currentFactoryModel.IsContracts[i] = false;
                currentFactoryModel.PendingContractCancellations[i] = false;
                Debug.Log($"Contract for Plant {i} has been cancelled.");
            }
        }

        FactoryGroup.Instance.UpdateAllPlantUI(currentFactoryModel);
    }

    private void UpdateMaterialModel(Func<ProductName, int> productGetter)
    {
        _playerMaterialModel = new
        (
            _playerMaterialModel.Alloy + productGetter(ProductName.Alloy),
            _playerMaterialModel.Microchip + productGetter(ProductName.Microchip),
            _playerMaterialModel.CarbonFiber + productGetter(ProductName.CarbonFiber),
            _playerMaterialModel.ConductiveFiber + productGetter(ProductName.ConductiveFiber),
            _playerMaterialModel.Pump + productGetter(ProductName.Pump),
            _playerMaterialModel.RubberTube + productGetter(ProductName.RubberTube)
        );
        UpdatePlayerSaveData();
    }
    public List<int> GetProductList()
    {
        return new List<int> {
            _playerMaterialModel.Alloy,
            _playerMaterialModel.Microchip,
            _playerMaterialModel.CarbonFiber,
            _playerMaterialModel.ConductiveFiber,
            _playerMaterialModel.Pump,
            _playerMaterialModel.RubberTube};

    }

    public void AddProduct()
    {
        UpdateMaterialModel(GetProduct);
    }

    public void AddContractProduct()
    {
        UpdateMaterialModel(GetContractProduct);
    }


    private int GetProduct(ProductName productName)
    {
        return TryGetProduct(_playerFactoryModel.Products, productName);
    }

    private int GetContractProduct(ProductName productName)
    {
        if (_playerFactoryContractModel.IsContracts[(int)productName])
        {
            return TryGetProduct(_playerFactoryContractModel.Products, productName);
        }
        return 0;
    }
    private int TryGetProduct(int[] products, ProductName productName)
    {
        if (!Enum.IsDefined(typeof(ProductName), productName))
        {
            Debug.LogError($"Invalid product name: {productName}");
            return -1;
        }

        int index = (int)productName;
        if (index < 0 || index >= products.Length)
        {
            Debug.LogError($"Product index out of range: {index} | products Length: {products.Length}");
            return -1;
        }

        return products[index];
    }
    #endregion

    #region PlayerTechModel
    public PlayerTechModel GetPlayerTechModel() => _playerTechModel;
    public void DoTechResult(PlayerTechModel model)
    {
        _playerTechModel = model;
        UpdatePlayerSaveData();
    }
    #endregion
    
    #region PlayerCurrentInfoModel
    public PlayerCurrentInfoModel GetPlayerCurrentInfoModel() => _playerCurrentInfoModel;
    public void DoCurrentInfoResult(PlayerCurrentInfoModel model)
    {
        _playerCurrentInfoModel = model;
        UpdatePlayerSaveData();
    }
    #endregion

}
