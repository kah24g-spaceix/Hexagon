using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class PlantList : MonoBehaviour
{
    [SerializeField] private GameObject plantHolder;
    public static PlantList plantList;
    private List<Plant> plants = new List<Plant>();
    public List<Plant> Plants {get;set;}

    private PlantModel plantModel;
    public PlantModel PlantModel {get;set;}

    private void Awake()
    {
        PlantModel = plantModel;
        if(plantList == null)
            plantList = this;
    }
    public void InitalizePlantModel(PlantData plantData)
    {
        string[] names = plantData.PlantDataLines.Select(t => t.Name).ToArray();
        int[] constructionCosts = plantData.PlantDataLines.Select(t => t.ConstructionCost).ToArray();
        int[] contractCosts = plantData.PlantDataLines.Select(t => t.ContractCost).ToArray();
        int[] products = plantData.PlantDataLines.Select(t => t.Product).ToArray();
        int[] importProducts = plantData.PlantDataLines.Select(t => t.ImportProduct).ToArray();
        int[] levels = new int[plantData.PlantDataLines.Length];
        bool[] isContructions = new bool[plantData.PlantDataLines.Length];
        bool[] isContracts = new bool[plantData.PlantDataLines.Length];

        plantModel = new PlantModel(
            names,
            constructionCosts,
            contractCosts,
            products,
            importProducts,
            levels,
            isContructions,
            isContracts
        );
        PlantListStart();
    }
    public void PlantListStart()
    {
        if (plantHolder == null)
        {
            Debug.LogError("PlantHolder is not assigned.");
            return;
        }
        plants.Clear();
        foreach (Plant plant in plantHolder.GetComponentsInChildren<Plant>())
            plants.Add(plant);
        for (int i = 0; i< plants.Count; i++)
        {
            plants[i].ID = i;
        }
    }

    public void UpdateAllPlantUI(PlantModel model)
    {
        foreach (Plant plant in plants)
        {
            plant.Bind(model);
        }
    }
}