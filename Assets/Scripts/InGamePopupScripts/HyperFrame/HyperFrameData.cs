using System.Collections;
using System.Collections.Generic;

public class HyperFrameDataLine
{
    public string Name { get; }
    public string Description { get; }
    public int Price { get; }
    public int[] MaterialsCost { get; }

    public HyperFrameDataLine(
        string name,
        string description,
        int price,
        int[] materialsCost
    )
    {
        Name = name;
        Description = description;
        Price = price;
        MaterialsCost = materialsCost;
    }
}

public class HyperFrameData : IEnumerable<HyperFrameDataLine>
{
    public HyperFrameDataLine[] HyperFrameDataLines { get; }
    public HyperFrameData(HyperFrameDataLine[] hyperFrameDataLines)
    {
        HyperFrameDataLines = hyperFrameDataLines;
    }
    public IEnumerator<HyperFrameDataLine> GetEnumerator()
    {
        foreach (var hyperFrameDataLine in HyperFrameDataLines)
        {
            yield return hyperFrameDataLine;
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}