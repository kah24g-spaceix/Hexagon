public class PlayerSystemData
{
    public int Money { get; set; }
    public int Employees { get; set; }
    public int Resistance { get; set; }
    public double CommunityOpinionValue { get; set; }

    public PlayerSystemData(int money, int employees, int resistance, double communityOpinionValue)
    {
        Money = money;
        Employees = employees;
        Resistance = resistance;
        CommunityOpinionValue = communityOpinionValue;
    }
}
public class PlayerDayData
{
    public int Day { get; set; }
    public int LastDay { get; set; }
    public float CurrentTime { get; set; }

    public PlayerDayData (int day, int lastDay, float currentTime)
    {
        Day = day;
        LastDay = lastDay;
        CurrentTime = currentTime;
    }
}
public class PlayerMaterialData
{
    public int Alloy { get; set; }
    public int Microchip { get; set; }
    public int CarbonFiber { get; set; }
    public int ConductiveFiber { get; set; }
    public int Pump { get; set; }
    public int RubberTube { get; set; }

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
public class PlayerHyperFrameData
{
    public int Eye { get; set; }
    public int Arm { get; set; }
    public int Hand { get; set; }
    public int Lag { get; set; }
    public int Foot { get; set; }
    public int Body { get; set; }
    public int Head { get; set; }

    public PlayerHyperFrameData
    (
        int eye,
        int arm,
        int hand,
        int lag,
        int foot,
        int body,
        int head
        )
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
public class PlayerFactoryData
{
    public int[] UpgradeCosts { get; set; }
    public int[] Products { get; set; }
    public int[] Levels { get; set; }
    public bool[] IsContructions { get; set; }


    public PlayerFactoryData(
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
public class PlayerFactoryContractData
{
    public int[] Costs { get; set; }
    public int[] Products { get; set; }
    public bool[] IsContracts { get; set; }
    public PlayerFactoryContractData(int[] costs, int[] products, bool[] isContracts)
    {
        Costs = costs;
        Products = products;
        IsContracts = isContracts;
    }
}

public class PlayerTechTreeData
{
    public int TechPoint { get; set; }
    public int RevenueValue { get; set; }
    public int MaxEmployee { get; set; }
    public int[] TechLevels { get; set; }

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
    public PlayerSystemData P_SystemData { get; set; }
    public PlayerDayData P_DayData { get; set; }
    public PlayerMaterialData P_MaterialData { get; set; }
    public PlayerHyperFrameData P_HyperFrameData { get; set; }

    public PlayerFactoryData P_PlantData { get; set; }
    public PlayerFactoryContractData P_PlantContractData { get; set; }
    public PlayerTechTreeData P_TechData { get; set; }

    public PlayerData(
        PlayerSystemData p_systemData,
        PlayerDayData p_dayData,
        PlayerMaterialData p_materialData,
        PlayerHyperFrameData p_hyperFrameData,
        PlayerFactoryData p_plantData,
        PlayerFactoryContractData p_plantContractData,
        PlayerTechTreeData p_techTreeData)
    {
        P_SystemData = p_systemData;
        P_DayData = p_dayData;
        P_MaterialData = p_materialData;
        P_HyperFrameData = p_hyperFrameData;

        P_PlantData = p_plantData;
        P_PlantContractData = p_plantContractData;

        P_TechData = p_techTreeData;
    }

}