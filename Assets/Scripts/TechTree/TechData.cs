using System.Collections;
using System.Collections.Generic;

public class TechDataLine
{

    public int TechCap { get; }
    public int TechCost { get; }
    public int CommunityOpinion { get; }

    public string TechName { get; }
    public string TechDescription { get; }
    public int[] TechOpen { get; }

    public TechDataLine(int pTechCaps, int pTechCost, int pCommunityOpinion)
    {
        TechCap = pTechCaps;
        TechCost = pTechCost;
        CommunityOpinion = pCommunityOpinion;
        
    }

    public TechDataLine(string pTechNames, string pTechDescriptions, int[] pTechOpen)
    {
        TechName = pTechNames;
        TechDescription = pTechDescriptions;
        TechOpen = pTechOpen;
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