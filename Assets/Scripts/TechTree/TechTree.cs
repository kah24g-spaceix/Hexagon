using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    public static TechTree techTree;
    public GameObject TechHolder;
    public GameObject TechPrefab;
    public List<Tech> TechList = new List<Tech>();
    public Dictionary<int, Tech> TechDictionary = new Dictionary<int, Tech>(); // id를 키로 사용
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
        int[] techLevels = new int[techData.TechDataLines.Length];
        int[] techCaps = techData.TechDataLines.Select(t => t.TechCap).ToArray();
        int[] techCosts = techData.TechDataLines.Select(t => t.TechCost).ToArray();
        int[] communityOpinions = techData.TechDataLines.Select(t => t.CommunityOpinion).ToArray();
        string[] techNames = techData.TechDataLines.Select(t => t.TechName).ToArray();
        string[] techDescriptions = techData.TechDataLines.Select(t => t.TechDescription).ToArray();
        int[][] techOpens = techData.TechDataLines.Select(t => t.TechOpen).ToArray(); // 2차원 배열로 변환

        m_techModel = new TechModel(
            techLevels,
            techCaps,
            techCosts,
            communityOpinions,
            techNames,
            techDescriptions,
            techOpens
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
        foreach (Tech tech in TechHolder.GetComponentsInChildren<Tech>())
        {
            TechList.Add(tech);
            Debug.Log($"AddList");
        }

        UpdateAllTechUI(m_techModel);
        Debug.Log($"Update");
    }

    public void UpdateAllTechUI(TechModel pModel)
    {
        foreach (Tech tech in TechList)
        {
            tech.Bind(pModel);
        }
    }
}
