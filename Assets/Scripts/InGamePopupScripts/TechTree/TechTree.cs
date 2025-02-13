using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TechTree : MonoBehaviour
{
    private static TechTree _instance;

    public static TechTree Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TechTree>();
                if (_instance == null)
                {
                    Debug.LogError("TechTree instance is not found in the scene.");
                }
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject techHolder;
    public List<Tech> TechList { get; private set; }
    public TechModel TechModel { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

    }

    public void InitializeTechModel(TechData techData)
    {
        if (techData == null || techHolder == null)
        {
            Debug.LogError("TechData or TechHolder is not assigned.");
            return;
        }

        TechModel = new TechModel(
            new int[techData.TechDataLines.Length],
            techData.TechDataLines.Select(t => t.TechCap).ToArray(),
            techData.TechDataLines.Select(t => t.TechCost).ToArray(),
            techData.TechDataLines.Select(t => t.Revenue).ToArray(),
            techData.TechDataLines.Select(t => t.MaxEmployee).ToArray(),
            techData.TechDataLines.Select(t => t.TechName).ToArray(),
            techData.TechDataLines.Select(t => t.TechDescription).ToArray(),
            techData.TechDataLines.Select(t => t.ConnectedTechs ?? new int[0]).ToArray()
        );

        TechList = new List<Tech>(techHolder.GetComponentsInChildren<Tech>());
        for (int i = 0; i < TechList.Count; i++)
        {
            TechList[i].ID = i;
            TechList[i].ConnectedTechs = TechModel.ConnectedTechs[i];
        }

        UpdateAllTechUI(TechModel);
    }

    public void UpdateAllTechUI(TechModel model)
    {
        foreach (var tech in TechList)
        {
            tech.Bind(model);
        }
    }
}
