public struct PlayerSaveModel
{
    public int Money { get; } // 돈
    public int Commodity { get; } // 물자
    public int Employees { get; } // 직원
    public int Resistance { get; } // 저항군
    public int TechPoint { get; } // 테크 포인트
    public int Day { get; }



    public PlayerSaveModel(
        int money,
        int commodity,
        int employees,
        int resistance,
        int techPoint,
        int day
        )
    {
        Money = money;
        Commodity = commodity;
        Employees = employees;
        Resistance = resistance;
        TechPoint = techPoint;
        Day = day;

    }
}
public struct PlayerTechModel
{
    public int RevenueValue { get; } // 수익수치
    public int CommunityOpinion { get; } // 민심도
    public int TransportationTimeValue { get; } // 물자 반입 시간
    public int MaxEmployee { get; } // 최대 직원 수용인원 수
    public int[] TechLevels { get; }

    public PlayerTechModel(
        int revenueValue,
        int communityOpinion,
        int transportationTimeValue,
        int maxEmployees,
        int[] techLevels)
    {
        RevenueValue = revenueValue;
        CommunityOpinion = communityOpinion;
        TransportationTimeValue = transportationTimeValue;
        MaxEmployee = maxEmployees;
        TechLevels = techLevels;
    }
    public interface IPlayerModel
    {
        PlayerSaveModel GetPlayerSaveModel();
        PlayerTechModel GetPlayerTechModel();
        void DoPlayerInfoResult();
        void DoTechResult();
        void SaveGame();
        bool LoadGame();
    }
}