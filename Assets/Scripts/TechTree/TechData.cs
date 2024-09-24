using System.Collections;
using System.Collections.Generic;

public class TechDataLine
{
    public int TechCap { get; }
    public int TechCost { get; }
    public int Revenue { get; }
    public int MaxEmployee { get; }
    public int CommunityOpinion { get; }
    public string TechName { get; }
    public string TechDescription { get; }
    public int[] ConnectedTechs { get; }

    public TechDataLine(int techCap, int techCost, int revenue, int maxEmployee, int communityOpinion, string techName, string techDescription, int[] connectedTechs)
    {
        TechCap = techCap;
        TechCost = techCost;
        Revenue = revenue;
        MaxEmployee = maxEmployee;
        CommunityOpinion = communityOpinion;
        TechName = techName;
        TechDescription = techDescription;
        ConnectedTechs = connectedTechs;
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
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}