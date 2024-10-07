public struct PlayerSaveModel
{
    public int Money { get; } // 돈
    public int Commodity { get; } // 물자
    public int Employees { get; } // 직원
    public int Resistance { get; } // 저항군
    public double CommunityOpinionValue { get; } // 민심도
    public int Day { get; }



    public PlayerSaveModel(
        int money,
        int commodity,
        int employees,
        int resistance,
        double communityOpinionValue,
        int day
        )
    {
        Money = money;
        Commodity = commodity;
        Employees = employees;
        Resistance = resistance;
        CommunityOpinionValue = communityOpinionValue;
        Day = day;
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
    PlayerSaveModel GetPlayerSaveModel();
    PlayerTechModel GetPlayerTechModel();
    void Income();
    void BuyCommodity();
    void ExchangeTechPoint(int value);
    void Motivation();
    void DoPlayerInfoResult(PlayerSaveModel model);
    void DoTechResult(PlayerTechModel model);
    void SaveGame();
    bool LoadGame();
}