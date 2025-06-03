using System.Collections.Generic;

public class PlayerSystemModel
{
    public long Money { get; }
    public int Employees { get; }
    public int Resistance { get; }
    public double CommunityOpinionValue { get; }

    public PlayerSystemModel(
        long money,
        int employees,
        int resistance,
        double communityOpinionValue
        )
    {
        Money = money;
        Employees = employees;
        Resistance = resistance;
        CommunityOpinionValue = communityOpinionValue;
    }
}
public class PlayerDayModel
{
    public int Day { get; }
    public float CurrentTime { get; }
    public int LastDay { get; }
    public float DayLength { get; }
    public PlayerDayModel(int day, float currentTime, int lastDay, float dayLength)
    {
        Day = day;
        CurrentTime = currentTime;
        LastDay = lastDay;
        DayLength = dayLength;
    }
}
public enum ProductName
{
    Alloy,
    Microchip,
    CarbonFiber,
    ConductiveFiber,
    Pump,
    RubberTube
}
public class PlayerMaterialModel
{
    public int Alloy { get; }
    public int Microchip { get; }
    public int CarbonFiber { get; }
    public int ConductiveFiber { get; }
    public int Pump { get; }
    public int RubberTube { get; }


    public PlayerMaterialModel(
        int alloy,
        int microchip,
        int carbonFiber,
        int conductiveFiber,
        int pump,
        int rubberTube
        )
    {
        Alloy = alloy;
        Microchip = microchip;
        CarbonFiber = carbonFiber;
        ConductiveFiber = conductiveFiber;
        Pump = pump;
        RubberTube = rubberTube;
    }
}
public class PlayerHyperFrameModel
{
    public int Head { get; }
    public int Eye { get; }
    public int Arm { get; }
    public int Hand { get; }
    public int Body { get; }
    public int Lag { get; }
    public int Foot { get; }

    public PlayerHyperFrameModel
    (
        int head,
        int eye,
        int arm,
        int hand,
        int body,
        int lag,
        int foot)
    {
        Head = head;
        Eye = eye;
        Arm = arm;
        Hand = hand;
        Body = body;
        Lag = lag;
        Foot = foot;
    }
}
public class PlayerFactoryModel
{
    public int[] UpgradeCosts { get; }
    public int[] Products { get; }
    public int[] Levels { get; }
    public bool[] IsConstructions { get; }


    public PlayerFactoryModel(
        int[] upgradeCosts,
        int[] products,
        int[] levels,
        bool[] isConstructions
    )
    {
        UpgradeCosts = upgradeCosts;
        Products = products;
        Levels = levels;
        IsConstructions = isConstructions;
    }
}
public class PlayerFactoryContractModel
{
    public int[] Costs { get; }
    public int[] Products { get; }
    public bool[] IsContracts { get; }
    public PlayerFactoryContractModel(int[] costs, int[] products, bool[] isContracts)
    {
        Costs = costs;
        Products = products;
        IsContracts = isContracts;
    }
}
public class PlayerTechModel
{
    public int TechPoint { get; }
    public int RevenueValue { get; }
    public int MaxEmployee { get; }
    public int[] TechLevels { get; }

    public PlayerTechModel(
        int techPoint,
        int revenueValue,
        int maxEmployees,
        int[] techLevels)
    {
        TechPoint = techPoint;
        RevenueValue = revenueValue;
        MaxEmployee = maxEmployees;
        TechLevels = techLevels;
    }
}
public class PlayerCurrentInfoModel
{
    public int SalesRevenue { get; }
    public int OtherRevenue { get; }
    public int OtherIncome { get; }
    public int Salary { get; }
    public int CostSpent { get; }

    public PlayerCurrentInfoModel(int salesRevenue, int otherRevenue, int otherIncome, int salary, int costSpent)
    {
        SalesRevenue = salesRevenue;
        OtherRevenue = otherRevenue;
        OtherIncome = otherIncome;
        Salary = salary;
        CostSpent = costSpent;
    }
}
public class GameSettings
{
    public bool IsLoad { get; set; }
    public bool IsStoryMode { get; set; }
    public int DailyPlaytime { get; set; }
    public int LastDay { get; set; }
    public int InitialMoney { get; set; }
}
public interface IGameProgressHandler
{
    void InitGame();
    void InitData();
    void SaveGame(bool isDayData);
    bool LoadGame(bool isDayData);
    void ResetData();
    void TodayResult();
    void LastDayResult();
    void SetTimeScale(float scale);
    void NextDay();
    void ResetTime();
}
public interface IPlayerSystemModelHandler
{
    PlayerSystemModel GetPlayerSystemModel();
    void DoSystemResult(PlayerSystemModel model);
    PlayerDayModel GetPlayerDayModel();
    void DoDayResult(PlayerDayModel model);
    void Income(float skipTime);
    void ChangeTechPoint(float value);
    long GetTechPointPrice();
}
public interface IPlayerMaterialModelHandler
{
    PlayerMaterialModel GetPlayerMaterialModel();
    void DoMaterialResult(PlayerMaterialModel model);
    List<int> GetProductList();
}
public interface IPlayerHyperFrameModelHandler
{
    PlayerHyperFrameModel GetPlayerHyperFrameModel();
    void DoHyperFrameResult(PlayerHyperFrameModel model);
}
public interface IPlayerFactoryModelHandler
{
    PlayerFactoryModel GetPlayerFactoryModel();
    void DoFactoryResult(PlayerFactoryModel model);
    void AddProduct();
    PlayerFactoryContractModel GetPlayerFactoryContractModel();
    void DoFactoryContractResult(PlayerFactoryContractModel model);
    void AddContractProduct();
}

public interface IPlayerTechModelHandler
{
    PlayerTechModel GetPlayerTechModel();
    void DoTechResult(PlayerTechModel model);
}

public interface IPlayerCurrentInfoModelHandler
{
    PlayerCurrentInfoModel GetPlayerCurrentInfoModel();
    void DoCurrentInfoResult(PlayerCurrentInfoModel model);
}
public interface IGameModel :
    IGameProgressHandler,
    IPlayerSystemModelHandler,
    IPlayerMaterialModelHandler,
    IPlayerFactoryModelHandler,
    IPlayerHyperFrameModelHandler,
    IPlayerCurrentInfoModelHandler,
    IPlayerTechModelHandler
{ }