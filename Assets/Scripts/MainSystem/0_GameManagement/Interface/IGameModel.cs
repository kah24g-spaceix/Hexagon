using System.Collections.Generic;
using Unity.VisualScripting;

public struct PlayerSystemModel
{
    public int Money { get; } // ��
    public int Employees { get; } // ����
    public int Resistance { get; } // ���ױ�
    public double CommunityOpinionValue { get; } // �νɵ�
    public int Day { get; }

    public PlayerSystemModel(
        int money,
        int employees,
        int resistance,
        double communityOpinionValue,
        int day
        )
    {
        Money = money;
        Employees = employees;
        Resistance = resistance;
        CommunityOpinionValue = communityOpinionValue;
        Day = day;
    }
}
public struct PlayerMaterialModel
{
    public int Alloy { get; } // �ձ�
    public int Microchip { get; } // ����ũ�� Ĩ
    public int CarbonFiber { get; } // ź�� ����
    public int ConductiveFiber { get; } // ������ ����
    public int Pump { get; } // ����
    public int RubberTube { get; } // ��


    public PlayerMaterialModel(
        int alloy,
        int microchip,
        int carbonFiber,
        int conductiveFiber,
        int pump,
        int tube
        )
    {
        Alloy = alloy;
        Microchip = microchip;
        CarbonFiber = carbonFiber;
        ConductiveFiber = conductiveFiber;
        Pump = pump;
        RubberTube = tube;
    }
}
public struct PlayerHyperFrameModel
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
public struct PlayerPlantModel
{
    public int[] UpgradeCosts { get; }
    public int[] Products { get; }
    public int[] Levels { get; }
    public bool[] IsContructions { get; }
    

    public PlayerPlantModel(
        int[] upgradeCosts,
        int[] products,
        int[] levels,
        bool[] isContructions
    )
    {
        UpgradeCosts = upgradeCosts;
        Products = products;
        Levels = levels;
        IsContructions = isContructions;
    }
}
public struct PlayerPlantContractModel
{
    public bool[] IsContracts { get; }
        public PlayerPlantContractModel(
        bool[] isContracts
    )
    {
        IsContracts = isContracts;
    }
}
public struct PlayerTechModel
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
public interface IGameModel
{
    PlayerSystemModel GetPlayerSystemModel();
    PlayerMaterialModel GetPlayerMaterialModel();
    PlayerPlantModel GetPlayerPlantModel();
    PlayerPlantContractModel GetPlayerPlantContractModel();
    PlayerTechModel GetPlayerTechModel();

    void DoSystemResult(PlayerSystemModel model);
    void DoMaterialResult(PlayerMaterialModel model);
    void DoPlantResult(PlayerPlantModel model);
    void DoPlantContractResult(PlayerPlantContractModel model);
    void DoTechResult(PlayerTechModel model);
    void Income();
    void ExchangeTechPoint(int value);
    void TodayResult();
    void NextDay();


    void Sell();

    void SaveGame();
    bool LoadGame();
}