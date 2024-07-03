using System;
using System.Collections.Generic;
using UnityEngine;

public struct TechModel
{
    public int[] TechLevels { get; }
    public int TechCaps { get; }
    public int TechCost { get; }
    public int CommunityOpinion { get; }
    public int[] TechOpen { get; }
    public string TechName { get; }
    public string TechDescription { get; }

    public TechModel(int[] pTechLevels, int pTechCaps, int pTechCost, int pCommunityOpinion, int[] pTechOpen, string pTechName, string pTechDescription)
    {
        TechLevels = pTechLevels;
        TechCaps = pTechCaps;
        TechCost = pTechCost;
        TechOpen = pTechOpen;
        CommunityOpinion = pCommunityOpinion;
        TechName = pTechName;
        TechDescription = pTechDescription;
    }
}
