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
                _instance = Object.FindAnyObjectByType<FactoryGroup>();
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
    public List<Factory> FactoryList { get; private set; }
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

    public void InitializeModel(FactoryData data)
    {
        if (data == null)
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
            data.FactoryDataLines.Select(t => t.Name).ToArray(),
            data.FactoryDataLines.Select(t => t.ConstructionCost).ToArray(),
            data.FactoryDataLines.Select(t => t.ContractCost).ToArray(),
            data.FactoryDataLines.Select(t => t.UpgradeCost).ToArray(),
            data.FactoryDataLines.Select(t => t.Product).ToArray(),
            data.FactoryDataLines.Select(t => t.ContractProduct).ToArray(),
            data.FactoryDataLines.Select(t => t.LevelCap).ToArray(),
            new int[data.FactoryDataLines.Length],
            new bool[data.FactoryDataLines.Length],
            new bool[data.FactoryDataLines.Length],
            new bool[data.FactoryDataLines.Length]
        );

        FactoryList = new List<Factory>(factoryHolder.GetComponentsInChildren<Factory>());
        ProductList = new List<ProductValue>(productHolder.GetComponentsInChildren<ProductValue>());
        for (int i = 0; i < FactoryList.Count; i++)
        {
            FactoryList[i].ID = i;
        }

        UpdateAllPlantUI(Model);
    }

    public void UpdateAllPlantUI(FactoryModel model)
    {
        foreach (var plant in FactoryList)
        {
            plant.Bind(model);
        }
    }
}
