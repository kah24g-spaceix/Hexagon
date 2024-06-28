using System;
using System.Collections.Generic;
using UnityEngine;

public struct TechModel
{
    public int[] TechLevels { get; }
    public int TechCaps { get; }
    public int TechCost { get; }
    public int CommunityOpinion { get; }
    public int[] TechList { get; }
    public string TechName { get; }
    public string TechDescription { get; }

    public TechModel(int[] pTechLevels, int pTechCaps, int pTechCost, int pCommunityOpinion, int[] pTechList, string pTechName, string pTechDescription)
    {
        TechLevels = pTechLevels;
        TechCaps = pTechCaps;
        TechCost = pTechCost;
        TechList = pTechList;
        CommunityOpinion = pCommunityOpinion;
        TechName = pTechName;
        TechDescription = pTechDescription;
    }
}
