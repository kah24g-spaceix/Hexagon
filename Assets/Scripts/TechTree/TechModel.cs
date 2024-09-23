public struct TechModel
{
    public int[] TechLevels { get; }
    public int[] TechCaps { get; }
    public int[] TechCosts { get; }
    public int[] Revenue { get; }
    public int[] TransportationTime { get; }
    public int[] MaxEmployee { get; }
    public int[] CommunityOpinion { get; }
    public string[] TechNames { get; }
    public string[] TechDescriptions { get; }
    public int[][] ConnectedTechs { get; }

    public TechModel(
        int[] techLevels,
        int[] techCaps,
        int[] techCosts,
        int[] revenue,
        int[] transportationTime,
        int[] maxEmployee,
        int[] communityOpinion,
        string[] techNames,
        string[] techDescriptions,
        int[][] connectedTechs)
    {
        TechLevels = techLevels;
        TechCaps = techCaps;
        TechCosts = techCosts;
        Revenue = revenue;
        TransportationTime = transportationTime;
        MaxEmployee = maxEmployee;
        CommunityOpinion = communityOpinion;
        TechNames = techNames;
        TechDescriptions = techDescriptions;
        ConnectedTechs = connectedTechs;
    }
}
