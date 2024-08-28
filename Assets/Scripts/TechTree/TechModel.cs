public struct TechModel
{
    public int[] TechLevels { get; }
    public int[] TechCaps { get; }
    public int[] TechCosts { get; }
    public int[] CommunityOpinions { get; }
    public string[] TechNames { get; }
    public string[] TechDescriptions { get; }
    public int[][] ConnectedTechs { get; }

    public TechModel(
        int[] techLevels,
        int[] techCaps,
        int[] techCosts,
        int[] communityOpinions,
        string[] techNames,
        string[] techDescriptions,
        int[][] connectedTechs)
    {
        TechLevels = techLevels;
        TechCaps = techCaps;
        TechCosts = techCosts;
        CommunityOpinions = communityOpinions;
        TechNames = techNames;
        TechDescriptions = techDescriptions;
        ConnectedTechs = connectedTechs;
    }
}
