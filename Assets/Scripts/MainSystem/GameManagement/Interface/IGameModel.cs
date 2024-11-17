using System.Collections.Generic;
using Unity.VisualScripting;

public struct PlayerSystemModel
{
    public int Money { get; } // 돈
    public int Employees { get; } // 직원
    public int Resistance { get; } // 저항군
    public double CommunityOpinionValue { get; } // 민심도
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
    public int Alloy { get; } // 합금
    public int Microchip { get; } // 마이크로 칩
    public int CarbonFiber { get; } // 탄소 섬유
    public int ConductiveFiber { get; } // 전도성 섬유
    public int Pump { get; } // 펌프
    public int RubberTube { get; } // 관


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
public struct PlayerTechModel
{
    public int TechPoint { get; } // 테크 포인트
    public int RevenueValue { get; } // 수익수치
    public int MaxEmployee { get; } // 최대 직원 수용인원 수
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
    PlayerTechModel GetPlayerTechModel();
    void DoSystemResult(PlayerSystemModel model);
    void DoMaterialResult(PlayerMaterialModel model);
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