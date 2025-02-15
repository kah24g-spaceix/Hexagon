public class HyperFrameModel
{
    public string[] Names { get; }
    public int[] Prices { get; }
    public int[][] MaterialsCosts { get; }
    public int[] Values { get; }

    public HyperFrameModel(
        string[] names,
        int[] prices,
        int[][] materialsCosts,
        int[] values
    )
    {
        Names = names;
        Prices = prices;
        MaterialsCosts = materialsCosts;
        Values = values;
    }
}