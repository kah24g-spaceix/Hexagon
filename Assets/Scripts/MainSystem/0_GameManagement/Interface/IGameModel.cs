using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerSystemModel
{
    public int Money { get; } // ��
    public int Employees { get; } // ����
    public int Resistance { get; } // ���ױ�
    public double CommunityOpinionValue { get; } // �νɵ�

    public PlayerSystemModel(
        int money,
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
    public int LastDay { get; }
    public float DayLength { get; }
    public PlayerDayModel (int day,int lastDay, float dayLength)
    {
        Day = day;
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
    public int Eye { get; }
    public int Arm { get; }
    public int Hand { get; }
    public int Lag { get; }
    public int Foot { get; }
    public int Body { get; }
    public int Head { get; }

    public PlayerHyperFrameModel
    (
        int eye, 
        int arm, 
        int hand, 
        int lag, 
        int foot, 
        int body,
        int head)
    {
        Eye = eye;
        Arm = arm;
        Hand = hand;
        Lag = lag;
        Foot = foot;
        Body = body;
        Head = head;
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
    public int TechPoint { get; } // ��ũ ����Ʈ
    public int RevenueValue { get; } // ���ͼ�ġ
    public int MaxEmployee { get; } // �ִ� ���� �����ο� ��
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

public interface IPlayerSystemModelHandler
{
    PlayerSystemModel GetPlayerSystemModel();
    void DoSystemResult(PlayerSystemModel model);
    PlayerDayModel GetPlayerDayModel();
    void DoDayResult(PlayerDayModel model);
    void Income(float skipTime);
    void ExchangeTechPoint(int value);
}
public interface IPlayerMaterialModelHandler
{
    PlayerMaterialModel GetPlayerMaterialModel();
    void DoMaterialResult(PlayerMaterialModel model);
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

public interface IGameProgressHandler
{
    void InitData();
    void SaveGame(bool useDateData);
    bool LoadGame(bool useDateData);
    void TodayResult();
    void SetTimeScale(float scale);
    void NextDay();
}
public interface IGameModel :
    IPlayerSystemModelHandler,
    IPlayerMaterialModelHandler,
    IPlayerFactoryModelHandler,
    IPlayerHyperFrameModelHandler,
    IPlayerTechModelHandler,
    IGameProgressHandler
{
}