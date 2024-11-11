// 플레이어 시스템 관련 데이터
public class PlayerSystemData
{
    public int Money { get; set; } // 돈
    public int Employees { get; set; } // 직원
    public int Resistance { get; set; } // 저항군
    public double CommunityOpinionValue { get; set; } // 민심 퍼센트
    public int Day { get; set; } // 날

    public PlayerSystemData(int money, int employees, int resistance, double communityOpinionValue, int day)
    {
        Money = money;
        Employees = employees;
        Resistance = resistance;
        CommunityOpinionValue = communityOpinionValue;
        Day = day;
    }
}

// 플레이어 자원 관련 데이터
public class PlayerMaterialData
{
    public int Alloy { get; set; } // 합금
    public int Microchip { get; set; } // 마이크로 칩
    public int CarbonFiber { get; set; } // 탄소 섬유
    public int ConductiveFiber { get; set; } // 전도성 섬유
    public int Pump { get; set; } // 펌프
    public int RubberTube { get; set; } // 고무 관

    public PlayerMaterialData(int alloy, int microchip, int carbonFiber, int conductiveFiber, int pump, int tube)
    {
        Alloy = alloy;
        Microchip = microchip;
        CarbonFiber = carbonFiber;
        ConductiveFiber = conductiveFiber;
        Pump = pump;
        RubberTube = tube;
    }
}

// 플레이어 기술 트리 관련 데이터
public class PlayerTechTreeData
{
    public int TechPoint { get; set; } // 테크 포인트
    public int RevenueValue { get; set; } // 수익 수치
    public int MaxEmployee { get; set; } // 최대 수용 인원 수
    public int[] TechLevels { get; set; } // 테크 레벨

    public PlayerTechTreeData(int techPoint, int revenueValue, int maxEmployee, int[] techLevels)
    {
        TechPoint = techPoint;
        RevenueValue = revenueValue;
        MaxEmployee = maxEmployee;
        TechLevels = techLevels;
    }
}

public class PlayerData
{
    public PlayerSystemData SystemData { get; set; } // 플레이어 시스템 데이터
    public PlayerMaterialData MaterialData { get; set; } // 플레이어 자원 데이터
    public PlayerTechTreeData TechData { get; set; } // 플레이어 테크 트리 데이터

    public PlayerData(
        PlayerSystemData systemData,
        PlayerMaterialData materialData,
        PlayerTechTreeData techTreeData)
    {
        SystemData = systemData;
        MaterialData = materialData;
        TechData = techTreeData;
    }

}