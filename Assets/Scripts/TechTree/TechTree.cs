using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PlayerTechModel;

public class TechTree : MonoBehaviour
{
    public static TechTree techTree;
    [SerializeField] private GameObject techHolder;
    [SerializeField] private List<Tech> techList = new List<Tech>();
    private GameManager gameManager;
    private TechModel techModel;
    private IGameModel playerModel;
    private int techPoint;
    private int communityOpinion;
    public GameObject GetTechHolder() => techHolder;
    public int TechPoint
    {
        get => techPoint;
        set => techPoint = value;
    }
    public int CommunityOpinion
    {
        get => communityOpinion;
        set => communityOpinion = value;
    }
    public TechModel TechModel => techModel;
    public List<Tech> TechList => techList;

    private void Awake()
    {
        playerModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        techPoint = gameManager.techPoint;
        communityOpinion = gameManager.communityOpinion;
        if (techTree == null)
        {
            techTree = this;
        }
        Debug.Log("TechTree initialized.");
    }


    public void InitializeTechModel(TechData techData)
    {
        int[] techLevels = new int[techData.TechDataLines.Length];
        int[] techCaps = techData.TechDataLines.Select(t => t.TechCap).ToArray();
        int[] techCosts = techData.TechDataLines.Select(t => t.TechCost).ToArray();
        int[] revenue = techData.TechDataLines.Select(t => t.Revenue).ToArray();
        int[] maxEmployee = techData.TechDataLines.Select(t=> t.MaxEmployee).ToArray();
        int[] communityOpinion = techData.TechDataLines.Select(t => t.CommunityOpinion).ToArray();
        string[] techNames = techData.TechDataLines.Select(t => t.TechName).ToArray();
        string[] techDescriptions = techData.TechDataLines.Select(t => t.TechDescription).ToArray();
        int[][] connectedTechs = techData.TechDataLines.Select(t => t.ConnectedTechs).ToArray();

        techModel = new TechModel(
            techLevels,
            techCaps,
            techCosts,
            revenue,
            maxEmployee,
            communityOpinion,
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
            techList[i].Id = i;
            Debug.Log($"Set ID for Tech: {techList[i].name} to {i}");
        }

        foreach (Tech tech in techList)
        {
            int techId = tech.Id;

            if (techId >= 0 && techId < techModel.ConnectedTechs.Length)
            {
                tech.ConnectedTechs = techModel.ConnectedTechs[techId];
                Debug.Log($"ConnectedTechs for Tech ID {techId}: {string.Join(", ", tech.ConnectedTechs)}");
            }
            else
            {
                break;
                //Debug.LogError($"Tech ID {techId} is out of range in TechOpens array.");
            }
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
