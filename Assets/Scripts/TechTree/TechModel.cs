public struct TechModel
{
    public int[] TechLevels { get; }
    public int[] TechCaps { get; }
    public int[] TechCosts { get; }
    public int[] CommunityOpinions { get; }
    public string[] TechNames { get; }
    public string[] TechDescriptions { get; }
    public int[][] TechOpens { get; } // 2차 배열로 변경

    public TechModel(
        int[] techLevels,
        int[] techCaps,
        int[] techCosts,
        int[] communityOpinions,
        string[] techNames,
        string[] techDescriptions,
        int[][] techOpens)
    {
        TechLevels = techLevels;
        TechCaps = techCaps;
        TechCosts = techCosts;
        CommunityOpinions = communityOpinions;
        TechNames = techNames;
        TechDescriptions = techDescriptions;
        TechOpens = techOpens;
    }
}
