using System;
using UnityEngine;

public struct TechModel
{
    public int TechCaps { get; }
    public int TechCost { get; }
    public string TechNames { get; }
    public string TechDescriptions { get; }

    public TechModel(int pTechCaps, int pTechCost, string pTechNames, string pTechDescriptions)
    {
        TechCaps = pTechCaps;
        TechCost = pTechCost;
        TechNames = pTechNames;
        TechDescriptions = pTechDescriptions;
    }
}
