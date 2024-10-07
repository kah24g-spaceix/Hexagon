using System.Collections;
using System.Collections.Generic;

public class PlayerSaveData
{
    public int Money { get; set; } // ��
    public int Commodity { get; set; } // ����
    public int Employees { get; set; } // ����
    public int Resistance { get; set; } // ���ױ�
    public int TechPoint { get; set; } // ��ũ ����Ʈ
    public int Day { get; set; }

    public int RevenueValue { get; set; } // ���ͼ�ġ
    public double CommunityOpinionValue { get; set; } // �ν��ۼ�Ʈ
    public int MaxEmployee { get; set; } // �ִ� ���� �ο� ��
    public int[] TechLevels { get; set; }

    public PlayerSaveData
        (
        int money,
        int commodity,
        int employees,
        int resistance,
        double communityOpinionValue,
        int day,

        int techPoint,
        int revenueValue,
        int maxEmployees,
        int[] techLevels
        )
    {
        Money = money;
        Commodity = commodity;
        Employees = employees;
        Resistance = resistance;
        CommunityOpinionValue = communityOpinionValue;
        Day = day;
        TechPoint = techPoint;
        RevenueValue = revenueValue;
        MaxEmployee = maxEmployees;
        TechLevels = techLevels;
    }
}

public class PlayerData : IEnumerable<PlayerSaveData>
{
    public PlayerSaveData PlayerSaveData { get; }

    public PlayerData(PlayerSaveData playerSaveData)
    {
        PlayerSaveData = playerSaveData;
    }

    public IEnumerator<PlayerSaveData> GetEnumerator()
    {
        yield return PlayerSaveData;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}