public class HyperFrameModel
{
    public string[] Names { get; }
    public string[] Descriptions { get; }
    public int[] Prices { get; }
    public int[][] MaterialsCosts { get; }
    public int[] Counts { get; }

    public HyperFrameModel(
        string[] names,
        string[] descriptions,
        int[] prices,
        int[][] materialsCosts,
        int[] counts
    )
    {
        Names = names;
        Descriptions = descriptions;
        Prices = prices;
        MaterialsCosts = materialsCosts;
        Counts = counts;
    }
}