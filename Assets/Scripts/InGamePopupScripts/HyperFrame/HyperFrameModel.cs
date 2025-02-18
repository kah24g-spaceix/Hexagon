public class HyperFrameModel
{
    public string[] Names { get; }
    public string[] Description { get; }
    public int[] Prices { get; }
    public int[][] MaterialsCosts { get; }
    public int[] Values { get; }

    public HyperFrameModel(
        string[] names,
        string[] description,
        int[] prices,
        int[][] materialsCosts,
        int[] values
    )
    {
        Names = names;
        Description = description;
        Prices = prices;
        MaterialsCosts = materialsCosts;
        Values = values;
    }
}