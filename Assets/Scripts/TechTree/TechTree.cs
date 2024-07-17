using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    public static TechTree techTree;
    public GameObject TechHolder;
    public GameObject TechPrefab;
    public List<Tech> TechList = new List<Tech>();
    public Dictionary<int, Tech> TechDictionary; // id를 키로 사용
    private TechModel m_techModel; // private으로 변경

    public int TechPoint = 20;

    private void Awake()
    {
        techTree = this;
        Debug.Log("TechTree initialized.");
    }

    // m_techModel에 접근할 수 있는 메서드 추가
    public TechModel GetTechModel()
    {
        return m_techModel;
    }

    public void SetTechModel(TechModel techModel)
    {
        m_techModel = techModel;
    }

    public void InitializeTechModel(TechData techData)
    {
        var techLevels = new int[techData.TechDataLines.Length];
        var techCaps = techData.TechDataLines.Select(t => t.TechCap).ToArray();
        var techCosts = techData.TechDataLines.Select(t => t.TechCost).ToArray();
        var communityOpinions = techData.TechDataLines.Select(t => t.CommunityOpinion).ToArray();
        var techNames = techData.TechDataLines.Select(t => t.TechName).ToArray();
        var techDescriptions = techData.TechDataLines.Select(t => t.TechDescription).ToArray();
        var techOpens = techData.TechDataLines.Select(t => t.TechOpen).ToArray();

        m_techModel = new TechModel(
            techLevels,
            techCaps.Max(),
            techCosts.Max(),
            communityOpinions.Max(),
            techNames.First(),
            techDescriptions.First(),
            techOpens.First()
        );

        TechTreeStart();
    }

    public void TechTreeStart()
    {
        if (TechHolder == null)
        {
            Debug.LogError("TechHolder is not assigned.");
            return;
        }

        TechDictionary = new Dictionary<int, Tech>();
        TechList = new List<Tech>();

        foreach (Tech tech in TechHolder.GetComponentsInChildren<Tech>())
        {
            TechList.Add(tech);
        }
        for (int i = 0; i < TechList.Count; i++) TechList[i].id = i;
        foreach (Tech tech in TechList)
        {
            tech.ConnectedTechs = m_techModel.TechOpen;
        }
        UpdateAllTechUI(m_techModel);
    }

    public void UpdateAllTechUI(TechModel pModel)
    {
        foreach (Tech tech in TechList)
        {
            tech.Bind(pModel);
        }
    }
}
