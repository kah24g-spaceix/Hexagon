using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class FactoryGroup : MonoBehaviour
{
    private static FactoryGroup _instance;

    public static FactoryGroup Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FactoryGroup>();
                if (_instance == null)
                {
                    Debug.LogError("FactoryGroup instance is not found in the scene.");
                }
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject factoryHolder;
    [SerializeField] private GameObject productHolder;
    public List<Factory> List { get; private set; }
    public List<ProductValue> ProductList {get; private set;}
    public FactoryModel Model { get; private set; }

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

    public void InitializePlantModel(FactoryData factoryData)
    {
        if (factoryData == null)
        {
            Debug.LogError("FactoryData is not assigned.");
            return;
        }
        if (factoryHolder == null)
        {
            Debug.LogError("Factory Holder is not assigned in FactoryGroup.");
            return;
        }
        if (productHolder == null)
        {
            Debug.LogError("Material Holder is not assigned in FactoryGroup.");
            return;
        }
        Model = new FactoryModel(
            factoryData.FactoryDataLines.Select(t => t.Name).ToArray(),
            factoryData.FactoryDataLines.Select(t => t.ConstructionCost).ToArray(),
            factoryData.FactoryDataLines.Select(t => t.ContractCost).ToArray(),
            factoryData.FactoryDataLines.Select(t => t.UpgradeCost).ToArray(),
            factoryData.FactoryDataLines.Select(t => t.Product).ToArray(),
            factoryData.FactoryDataLines.Select(t => t.ContractProduct).ToArray(),
            factoryData.FactoryDataLines.Select(t => t.LevelCap).ToArray(),
            new int[factoryData.FactoryDataLines.Length],
            new bool[factoryData.FactoryDataLines.Length],
            new bool[factoryData.FactoryDataLines.Length],
            new bool[factoryData.FactoryDataLines.Length]
        );

        List = new List<Factory>(factoryHolder.GetComponentsInChildren<Factory>());
        ProductList = new List<ProductValue>(productHolder.GetComponentsInChildren<ProductValue>());
        for (int i = 0; i < List.Count; i++)
        {
            List[i].ID = i;
        }

        UpdateAllPlantUI(Model);
    }

    public void UpdateAllPlantUI(FactoryModel model)
    {
        foreach (var plant in List)
        {
            plant.Bind(model);
        }
    }
}
