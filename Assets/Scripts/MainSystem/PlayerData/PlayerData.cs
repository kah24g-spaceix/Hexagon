// �÷��̾� �ý��� ���� ������
public class PlayerSystemData
{
    public int Money { get; set; } // ��
    public int Employees { get; set; } // ����
    public int Resistance { get; set; } // ���ױ�
    public double CommunityOpinionValue { get; set; } // �ν� �ۼ�Ʈ
    public int Day { get; set; } // ��

    public PlayerSystemData(int money, int employees, int resistance, double communityOpinionValue, int day)
    {
        Money = money;
        Employees = employees;
        Resistance = resistance;
        CommunityOpinionValue = communityOpinionValue;
        Day = day;
    }
}

// �÷��̾� �ڿ� ���� ������
public class PlayerMaterialData
{
    public int Alloy { get; set; } // �ձ�
    public int Microchip { get; set; } // ����ũ�� Ĩ
    public int CarbonFiber { get; set; } // ź�� ����
    public int ConductiveFiber { get; set; } // ������ ����
    public int Pump { get; set; } // ����
    public int RubberTube { get; set; } // ���� ��

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
public class PlayerPlantData
{
    public int Product { get; set;}
    public int Level { get; set; }
}
// �÷��̾� ��� Ʈ�� ���� ������
public class PlayerTechTreeData
{
    public int TechPoint { get; set; } // ��ũ ����Ʈ
    public int RevenueValue { get; set; } // ���� ��ġ
    public int MaxEmployee { get; set; } // �ִ� ���� �ο� ��
    public int[] TechLevels { get; set; } // ��ũ ����

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
    public PlayerSystemData SystemData { get; set; } // �÷��̾� �ý��� ������
    public PlayerMaterialData MaterialData { get; set; } // �÷��̾� �ڿ� ������
    public PlayerTechTreeData TechData { get; set; } // �÷��̾� ��ũ Ʈ�� ������

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