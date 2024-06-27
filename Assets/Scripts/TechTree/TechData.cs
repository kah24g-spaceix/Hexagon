using System.Collections;
using System.Collections.Generic;

public class TechDataLine
{
    public int TechCaps { get; }
    public int TechCost { get; }
    public string TechNames { get; }
    public string TechDescriptions { get; }

    public TechDataLine(int pTechCaps, int pTechCost, string pTechNames, string pTechDescriptions)
    {
        TechCaps = pTechCaps;
        TechCost = pTechCost;
        TechNames = pTechNames;
        TechDescriptions = pTechDescriptions;
    }
}
public class TechData : IEnumerable<TechDataLine>
{
    public TechDataLine[] TechDataLines { get; }

    public TechData(TechDataLine[] techDataLines)
    {

        TechDataLines = techDataLines;
    }
    public IEnumerator<TechDataLine> GetEnumerator()
    {
        foreach (var techDataLine in TechDataLines)
        {
            yield return techDataLine;
        }
        yield break;
    }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}