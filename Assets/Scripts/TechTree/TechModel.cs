public struct TechModel
{
    public int[] TechLevels { get; }
    public int TechCaps { get; }
    public int TechCost { get; }
    public int CommunityOpinion { get; }
    public string TechName { get; }
    public string TechDescription { get; }
    public int[] TechOpen { get; }

    public TechModel(int[] pTechLevels, int pTechCaps, int pTechCost, int pCommunityOpinion, string pTechName, string pTechDescription, int[] pTechOpen)
    {
        TechLevels = pTechLevels ?? new int[0];
        TechCaps = pTechCaps;
        TechCost = pTechCost;
        CommunityOpinion = pCommunityOpinion;
        TechName = pTechName ?? string.Empty;
        TechDescription = pTechDescription ?? string.Empty;
        TechOpen = pTechOpen ?? new int[0];
    }
}
