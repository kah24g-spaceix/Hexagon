using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlantGroup : MonoBehaviour
{
    private static PlantGroup _instance;

    public static PlantGroup Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlantGroup>();
                if (_instance == null)
                {
                    Debug.LogError("PlantGroup instance is not found in the scene.");
                }
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject plantHolder;
    public List<Plant> PlantList { get; private set; }
    public PlantModel PlantModel { get; private set; }

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

    public void InitializePlantModel(PlantData plantData)
    {
        if (plantData == null)
        {
            Debug.LogError("PlantData is not assigned.");
            return;
        }
        if (plantHolder == null)
        {
            Debug.LogError("PlantHolder is not assigned in PlantGroup.");
            return;
        }
        PlantModel = new PlantModel(
            plantData.PlantDataLines.Select(t => t.Name).ToArray(),
            plantData.PlantDataLines.Select(t => t.ConstructionCost).ToArray(),
            plantData.PlantDataLines.Select(t => t.ContractCost).ToArray(),
            plantData.PlantDataLines.Select(t => t.UpgradeCost).ToArray(),
            plantData.PlantDataLines.Select(t => t.Product).ToArray(),
            plantData.PlantDataLines.Select(t => t.ContractProduct).ToArray(),
            plantData.PlantDataLines.Select(t => t.LevelCap).ToArray(),
            new int[plantData.PlantDataLines.Length],
            new bool[plantData.PlantDataLines.Length],
            new bool[plantData.PlantDataLines.Length],
            new bool[plantData.PlantDataLines.Length]
        );

        PlantList = new List<Plant>(plantHolder.GetComponentsInChildren<Plant>());
        for (int i = 0; i < PlantList.Count; i++)
        {
            PlantList[i].ID = i;
        }

        UpdateAllPlantUI(PlantModel);
    }

    public void UpdateAllPlantUI(PlantModel model)
    {
        foreach (var plant in PlantList)
        {
            plant.Bind(model);
        }
    }
}
