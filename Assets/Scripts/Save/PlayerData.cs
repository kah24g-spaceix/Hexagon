using System.Collections;
using System.Collections.Generic;

public class PlayerSaveData
{
    public int Money { get; set; } // 돈
    public int Commodity { get; set; } // 물자
    public int Employees { get; set; } // 직원
    public int Resistance { get; set; } // 저항군
    public int TechPoint { get; set; } // 테크 포인트
    public int Day { get; set; }

    public int RevenueValue { get; set; } // 수익수치
    public int CommunityOpinion { get; set; } // 민심도
    public int TransportationTimeValue { get; set; } // 물자 반입 시간
    public int MaxEmployee { get; set; } // 최대 직원 수용인원 수
    public int[] TechLevels { get; set; }

    public PlayerSaveData
        (
        int money,
        int commodity,
        int employees,
        int resistance,
        int techPoint,
        int day,

        int revenueValue,
        int communityOpinion,
        int transportationTimeValue,
        int maxEmployees,
        int[] techLevels
        )
    {
        Money = money;
        Commodity = commodity;
        Employees = employees;
        Resistance = resistance;
        TechPoint = techPoint;
        Day = day;
        RevenueValue = revenueValue;
        CommunityOpinion = communityOpinion;
        TransportationTimeValue = transportationTimeValue;
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