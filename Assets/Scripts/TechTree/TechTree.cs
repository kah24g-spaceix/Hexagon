using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    [SerializeField] private GameObject techHolder;
    public static TechTree techTree;
    
    private List<Tech> techList = new List<Tech>();
    public List<Tech> TechList { get; set; }

    private TechModel techModel;
    public TechModel TechModel { get; set; }

    private void Awake()
    {
        TechModel = techModel;
        if (techTree == null)
        {
            techTree = this;
        }
    }
    public void InitializeTechModel(TechData techData)
    {
        int[] techLevels = new int[techData.TechDataLines.Length];
        int[] techCaps = techData.TechDataLines.Select(t => t.TechCap).ToArray();
        int[] techCosts = techData.TechDataLines.Select(t => t.TechCost).ToArray();
        int[] revenue = techData.TechDataLines.Select(t => t.Revenue).ToArray();
        int[] maxEmployee = techData.TechDataLines.Select(t=> t.MaxEmployee).ToArray();
        string[] techNames = techData.TechDataLines.Select(t => t.TechName).ToArray();
        string[] techDescriptions = techData.TechDataLines.Select(t => t.TechDescription).ToArray();
        int[][] connectedTechs = techData.TechDataLines.Select(t => t.ConnectedTechs).ToArray();

        techModel = new TechModel(
            techLevels,
            techCaps,
            techCosts,
            revenue,
            maxEmployee,
            techNames,
            techDescriptions,
            connectedTechs
        );
        TechTreeStart();
    }
    public void TechTreeStart()
    {
        if (techHolder == null)
        {
            Debug.LogError("TechHolder is not assigned.");
            return;
        }
        techList.Clear();
        foreach (Tech tech in techHolder.GetComponentsInChildren<Tech>())
        {
            techList.Add(tech);
        }

        for (int i = 0; i < techList.Count; i++)
        {
            techList[i].ID = i;
        }

        for (int i = 0; i < techList.Count; i++)
        {
            int techId = techList[i].ID;
            techList[i].ConnectedTechs = techModel.ConnectedTechs[techId];
        }

        UpdateAllTechUI(techModel);
        Debug.Log("Tech UI updated.");
    }

    public void UpdateAllTechUI(TechModel model)
    {
        foreach (Tech tech in techList)
        {
            tech.Bind(model);
        }
    }
}
