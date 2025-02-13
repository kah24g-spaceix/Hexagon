public struct TechModel
{
    public int[] TechLevels { get; }
    public int[] TechCaps { get; }
    public int[] TechCosts { get; }
    public int[] Revenue { get; }
    public int[] MaxEmployee { get; }
    public string[] TechNames { get; }
    public string[] TechDescriptions { get; }
    public int[][] ConnectedTechs { get; }

    public TechModel(
        int[] techLevels,
        int[] techCaps,
        int[] techCosts,
        int[] revenue,
        int[] maxEmployee,
        string[] techNames,
        string[] techDescriptions,
        int[][] connectedTechs)
    {
        TechLevels = techLevels;
        TechCaps = techCaps;
        TechCosts = techCosts;
        Revenue = revenue;
        MaxEmployee = maxEmployee;
        TechNames = techNames;
        TechDescriptions = techDescriptions;
        ConnectedTechs = connectedTechs;
    }
}
