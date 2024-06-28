using System.Collections;
using System.Collections.Generic;

public class TechDataLine
{

    public int TechCap { get; }
    public int TechCost { get; }
    public int CommunityOpinion { get; }
    public int[] TechList { get; }
    public string TechName { get; }
    public string TechDescription { get; }


    public TechDataLine(int pTechCaps, int pTechCost, int pCommunityOpinion, int[] pTechList)
    {
        TechCap = pTechCaps;
        TechCost = pTechCost;
        CommunityOpinion = pCommunityOpinion;
        TechList = pTechList;
    }

    public TechDataLine(string pTechNames, string pTechDescriptions)
    {
        TechName = pTechNames;
        TechDescription = pTechDescriptions;
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