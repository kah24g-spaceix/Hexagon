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

    [HideInInspector] public bool isLoad;
    private void Awake()
    {
        if (isLoad)
            LoadGame(false);
        else
        {
            Debug.Log("init data");
            InitData();
        }

    }
    public void InitData()
    {
        PlayerData initData = JsonConvert.DeserializeObject<PlayerData>(_playerDataInit.text);

        if (initData == null)
        {
            Debug.LogError("PlayerData deserialization failed. Check _playerDataInit.");
            return;
        }

        if (initData.P_SystemData == null || initData.P_MaterialData == null)
        {
            Debug.LogError("One or more fields in PlayerData are null. Check JSON structure.");
            return;
        }
        SetData(initData);
    }
    private void UpdatePlayerSaveData()
    {
        _playerData.P_SystemData = new PlayerSystemData(
            _playerSystemModel.Money,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue
            );
        _playerData.P_DayData = new PlayerDayData(
            _playerDayModel.Day,
            _playerDayModel.LastDay,
            _playerDayModel.CurrentTime
            );
        _playerData.P_MaterialData = new PlayerMaterialData(
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

        _playerData.P_PlantData = new PlayerFactoryData(
            _playerFactoryModel.UpgradeCosts,
            _playerFactoryModel.Products,
            _playerFactoryModel.Levels,
            _playerFactoryModel.IsConstructions
        );
        _playerData.P_PlantContractData = new PlayerFactoryContractData(
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
    }
    private void SetData(PlayerData data)
    {
        _playerSystemModel = new PlayerSystemModel
        (
            data.P_SystemData.Money,
            data.P_SystemData.Employees,
            data.P_SystemData.Resistance,
            data.P_SystemData.CommunityOpinionValue
        );
        _playerDayModel = new PlayerDayModel
        (
            data.P_DayData.Day,
            data.P_DayData.LastDay,
            data.P_DayData.CurrentTime
        );
        _playerMaterialModel = new PlayerMaterialModel
        (
            data.P_MaterialData.Alloy,
            data.P_MaterialData.Microchip,
            data.P_MaterialData.CarbonFiber,
            data.P_MaterialData.ConductiveFiber,
            data.P_MaterialData.Pump,
            data.P_MaterialData.RubberTube
        );
        _playerHyperFrameModel = new PlayerHyperFrameModel
        (
            data.P_HyperFrameData.Eye,
            data.P_HyperFrameData.Arm,
            data.P_HyperFrameData.Hand,
            data.P_HyperFrameData.Lag,
            data.P_HyperFrameData.Foot,
            data.P_HyperFrameData.Body,
            data.P_HyperFrameData.Head
        );
        _playerFactoryModel = new PlayerFactoryModel
        (
            data.P_PlantData.UpgradeCosts,
            data.P_PlantData.Products,
            data.P_PlantData.Levels,
            data.P_PlantData.IsContructions
        );
        _playerFactoryContractModel = new PlayerFactoryContractModel
        (
            data.P_PlantContractData.Costs,
            data.P_PlantContractData.Products,
            data.P_PlantContractData.IsContracts
        );
        _playerTechModel = new PlayerTechModel
        (
            data.P_TechData.TechPoint,
            data.P_TechData.RevenueValue,
            data.P_TechData.MaxEmployee,
            data.P_TechData.TechLevels
        );

        _playerData = data;
    }
    public void SaveGame(bool useDateData)
    {
        if (!useDateData)
        {
            string json = JsonConvert.SerializeObject(_playerData);
            Debug.Log(json);
            PlayerPrefs.SetString("Save", json);
        }
        else
        {
            string json = JsonConvert.SerializeObject(_playerData);
            Debug.Log(json);
            PlayerPrefs.SetString("DaySave", json);
        }
    }
    public bool LoadGame(bool useDateData)
    {
        if (!useDateData){
            if (!PlayerPrefs.HasKey("Save"))
                return false;
            SetData(JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString("Save")));
        }
        else{
            if (!PlayerPrefs.HasKey("DaySave"))
                return false;
            SetData(JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString("DaySave")));
        }
        return true;
    }
    public PlayerSystemModel GetPlayerSystemModel() => _playerSystemModel;
    public PlayerDayModel GetPlayerDayModel() => _playerDayModel;
    public PlayerMaterialModel GetPlayerMaterialModel() => _playerMaterialModel;
    public PlayerHyperFrameModel GetPlayerHyperFrameModel() => _playerHyperFrameModel;
    public PlayerFactoryModel GetPlayerFactoryModel() => _playerFactoryModel;
    public PlayerFactoryContractModel GetPlayerFactoryContractModel() => _playerFactoryContractModel;
    public PlayerTechModel GetPlayerTechModel() => _playerTechModel;

    public void DoSystemResult(PlayerSystemModel model)
    {
        _playerSystemModel = model;
        UpdatePlayerSaveData();
    }
    public void DoDayResult(PlayerDayModel model)
    {
        _playerDayModel = model;
        UpdatePlayerSaveData();
    }
    public void DoMaterialResult(PlayerMaterialModel model)
    {
        _playerMaterialModel = model;
        UpdatePlayerSaveData();
    }
    public void DoHyperFrameResult(PlayerHyperFrameModel model)
    {
        _playerHyperFrameModel = model;
        UpdatePlayerSaveData();
    }
    public void DoFactoryResult(PlayerFactoryModel model)
    {
        _playerFactoryModel = model;
        UpdatePlayerSaveData();
    }
    public void DoFactoryContractResult(PlayerFactoryContractModel model)
    {
        _playerFactoryContractModel = model;
        UpdatePlayerSaveData();
    }
    public void DoTechResult(PlayerTechModel model)
    {
        _playerTechModel = model;
        UpdatePlayerSaveData();
    }

    private readonly int defaultMoney = 1;
    private readonly int balanceValue = 20;
    private readonly float revenueMultiplier = 0.1f; // 수익 증가 비율 조정

    public void Income()
    {
        int currentEmployees = _playerSystemModel.Employees;
        int maxIncome = Mathf.FloorToInt(_playerTechModel.MaxEmployee - (_playerSystemModel.Employees / balanceValue));

        // 수익 증가율 조정
        float revenue = 1 + (_playerTechModel.RevenueValue * revenueMultiplier);

        // 무작위 범위 최소값을 조정해 과도한 증가 방지
        int randomIncome = Mathf.Clamp(UnityEngine.Random.Range(currentEmployees / 2, maxIncome), 0, maxIncome);

        // 기본 증가 방식 조정
        int money = currentEmployees <= maxIncome
            ? _playerSystemModel.Money + Mathf.FloorToInt(randomIncome * defaultMoney * revenue)
            : _playerSystemModel.Money + Mathf.FloorToInt(currentEmployees * defaultMoney * revenue);

        // 새로운 PlayerSystemModel 생성 및 저장
        _playerSystemModel = new PlayerSystemModel(money, _playerSystemModel.Employees, _playerSystemModel.Resistance, _playerSystemModel.CommunityOpinionValue);
        UpdatePlayerSaveData();
    }

    public void ExchangeTechPoint(int value)
    {
        _playerSystemModel = new PlayerSystemModel(
            _playerSystemModel.Money + _playerSystemModel.Employees * 100 * value,
            _playerSystemModel.Employees,
            _playerSystemModel.Resistance,
            _playerSystemModel.CommunityOpinionValue
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
    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    public void NextDay()
    {
        _playerDayModel = new PlayerDayModel(
            _playerDayModel.Day + 1,
            _playerDayModel.LastDay,
            _playerDayModel.CurrentTime
        );

        ProcessContractCancellations();
        UpdatePlayerSaveData();
    }
    private void ProcessContractCancellations()
    {
        FactoryModel currentPlantModel = FactoryGroup.Instance.Model;

        for (int i = 0; i < currentPlantModel.PendingContractCancellations.Length; i++)
        {
            if (currentPlantModel.PendingContractCancellations[i])
            {
                currentPlantModel.IsContracts[i] = false;
                currentPlantModel.PendingContractCancellations[i] = false; // 처리 후 초기화
                Debug.Log($"Contract for Plant {i} has been cancelled.");
            }
        }

        FactoryGroup.Instance.UpdateAllPlantUI(currentPlantModel);
    }
    public void AddProduct()
    {
        _playerMaterialModel = new
        (
            _playerMaterialModel.Alloy + GetProduct(ProductName.Alloy),
            _playerMaterialModel.Microchip + GetProduct(ProductName.Microchip),
            _playerMaterialModel.CarbonFiber + GetProduct(ProductName.CarbonFiber),
            _playerMaterialModel.ConductiveFiber + GetProduct(ProductName.ConductiveFiber),
            _playerMaterialModel.Pump + GetProduct(ProductName.Pump),
            _playerMaterialModel.RubberTube + GetProduct(ProductName.RubberTube)
        );
        UpdatePlayerSaveData();
    }
    private int GetProduct(ProductName productName)
    {
        switch (productName)
        {
            case ProductName.Alloy:
                return _playerFactoryModel.Products[0];

            case ProductName.Microchip:
                return _playerFactoryModel.Products[1];

            case ProductName.CarbonFiber:
                return _playerFactoryModel.Products[2];

            case ProductName.ConductiveFiber:
                return _playerFactoryModel.Products[3];

            case ProductName.Pump:
                return _playerFactoryModel.Products[4];

            case ProductName.RubberTube:
                return _playerFactoryModel.Products[5];

            default:
                break;
        }
        Debug.LogError("does not match enum value");
        return -1;
    }
    public void AddContractProduct()
    {
        _playerMaterialModel = new
        (
            _playerMaterialModel.Alloy + GetContractProduct(ProductName.Alloy),
            _playerMaterialModel.Microchip + GetContractProduct(ProductName.Microchip),
            _playerMaterialModel.CarbonFiber + GetContractProduct(ProductName.CarbonFiber),
            _playerMaterialModel.ConductiveFiber + GetContractProduct(ProductName.ConductiveFiber),
            _playerMaterialModel.Pump + GetContractProduct(ProductName.Pump),
            _playerMaterialModel.RubberTube + GetContractProduct(ProductName.RubberTube)
        );
        UpdatePlayerSaveData();
    }
    private int GetContractProduct(ProductName productName)
    {
        switch (productName)
        {
            case ProductName.Alloy:
                if (_playerFactoryContractModel.IsContracts[(int)ProductName.Alloy])
                    return _playerFactoryContractModel.Products[(int)ProductName.Alloy];
                return 0;

            case ProductName.Microchip:
                if (_playerFactoryContractModel.IsContracts[(int)ProductName.Microchip])
                    return _playerFactoryContractModel.Products[(int)ProductName.Microchip];
                return 0;

            case ProductName.CarbonFiber:
                if (_playerFactoryContractModel.IsContracts[(int)ProductName.CarbonFiber])
                    return _playerFactoryContractModel.Products[(int)ProductName.CarbonFiber];
                return 0;

            case ProductName.ConductiveFiber:
                if (_playerFactoryContractModel.IsContracts[(int)ProductName.ConductiveFiber])
                    return _playerFactoryContractModel.Products[(int)ProductName.ConductiveFiber];
                return 0;

            case ProductName.Pump:
                if (_playerFactoryContractModel.IsContracts[(int)ProductName.Pump])
                    return _playerFactoryContractModel.Products[(int)ProductName.Pump];
                return 0;

            case ProductName.RubberTube:
                if (_playerFactoryContractModel.IsContracts[(int)ProductName.RubberTube])
                    return _playerFactoryContractModel.Products[(int)ProductName.RubberTube];
                return 0;

            default:
                break;
        }
        Debug.LogError("does not match enum value");
        return -1;
    }
}
