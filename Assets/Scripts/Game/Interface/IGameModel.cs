public struct PlayerSaveModel
{
    public int Money { get; } // ��
    public int Commodity { get; } // ����
    public int Employees { get; } // ����
    public int Resistance { get; } // ���ױ�
    public int Day { get; }



    public PlayerSaveModel(
        int money,
        int commodity,
        int employees,
        int resistance,
        int day
        )
    {
        Money = money;
        Commodity = commodity;
        Employees = employees;
        Resistance = resistance;
        Day = day;
    }
}
public struct PlayerTechModel
{
    public int TechPoint { get; } // ��ũ ����Ʈ
    public int RevenueValue { get; } // ���ͼ�ġ
    public double CommunityOpinionValue { get; } // �νɵ�
    public int MaxEmployee { get; } // �ִ� ���� �����ο� ��
    public int[] TechLevels { get; }

    public PlayerTechModel(
        int techPoint,
        int revenueValue,
        double communityOpinionValue,
        int maxEmployees,
        int[] techLevels)
    {
        TechPoint = techPoint;
        RevenueValue = revenueValue;
        CommunityOpinionValue = communityOpinionValue;
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