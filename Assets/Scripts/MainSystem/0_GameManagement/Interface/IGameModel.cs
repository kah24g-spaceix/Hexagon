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
public struct PlayerPlantModel
{

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
    PlayerSystemModel GetPlayerSaveModel();
    PlayerMaterialModel GetPlayerMaterialModel();
    PlayerPlantModel GetPlayerPlantModel();
    PlayerTechModel GetPlayerTechModel();
    void DoSystemResult(PlayerSystemModel model);
    void DoMaterialResult(PlayerMaterialModel model);
    void DoPlantResult(PlayerPlantModel model);
    void DoTechResult(PlayerTechModel model);
    void Income();
    void ExchangeTechPoint(int value);
    void TodayResult();
    void NextDay();


    void Sell();
    void UpdatePlantData(bool[] contracts);
    int Plant1(bool contract);
    int Plant2(bool contract);
    int Plant3(bool contract);
    int Plant4(bool contract);
    int Plant5(bool contract);
    int Plant6(bool contract);



    void SaveGame();
    bool LoadGame();
}