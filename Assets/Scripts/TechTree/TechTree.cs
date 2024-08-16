using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    public static TechTree techTree;

    [SerializeField] private GameObject techHolder;
    [SerializeField] private List<Tech> techList = new List<Tech>();
    private TechModel techModel;
    public int techPoint = 20;

    public GameObject GetTechHolder() => techHolder;
    public int TechPoint => techPoint;
    public TechModel TechModel => techModel;
    public List<Tech> TechList => techList;

    private void Awake()
    {
        if (techTree == null)
        {
            techTree = this;
        }
        else if (techTree != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("TechTree initialized.");
    }


    public void InitializeTechModel(TechData techData)
    {
        int[] techLevels = new int[techData.TechDataLines.Length];
        int[] techCaps = techData.TechDataLines.Select(t => t.TechCap).ToArray();
        int[] techCosts = techData.TechDataLines.Select(t => t.TechCost).ToArray();
        int[] communityOpinions = techData.TechDataLines.Select(t => t.CommunityOpinion).ToArray();
        string[] techNames = techData.TechDataLines.Select(t => t.TechName).ToArray();
        string[] techDescriptions = techData.TechDataLines.Select(t => t.TechDescription).ToArray();
        int[][] techOpens = techData.TechDataLines.Select(t => t.TechOpen).ToArray();

        techModel = new TechModel(
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
        if (techHolder == null)
        {
            Debug.LogError("TechHolder is not assigned.");
            return;
        }
        techList.Clear();
        int index = 0;
        foreach (Tech tech in techHolder.GetComponentsInChildren<Tech>())
        {
            if (index < techHolder.GetComponentsInChildren<Tech>().Length - 1)
            {
                techList.Add(tech);
                index++;
            }
            else break;
        }

        for (int i = 0; i < techList.Count; i++)
        {
            techList[i].Id = i;
            Debug.Log($"Set ID for Tech: {techList[i].name} to {i}");
        }

        foreach (Tech tech in techList)
        {
            int techId = tech.Id;

            if (techId >= 0 && techId < techModel.TechOpens.Length)
            {
                tech.ConnectedTechs = techModel.TechOpens[techId];
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
