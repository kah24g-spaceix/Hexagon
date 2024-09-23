public struct PlayerSaveModel
{
    public int Money { get; } // ��
    public int Commodity { get; } // ����
    public int Employees { get; } // ����
    public int Resistance { get; } // ���ױ�
    public int TechPoint { get; } // ��ũ ����Ʈ
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
    public int RevenueValue { get; } // ���ͼ�ġ
    public int CommunityOpinion { get; } // �νɵ�
    public int TransportationTimeValue { get; } // ���� ���� �ð�
    public int MaxEmployee { get; } // �ִ� ���� �����ο� ��
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