using System.Collections;
using System.Collections.Generic;

public class PlantDataLine
{
    public string Name { get; }
    public int ConstructionCost { get; }
    public int ContractCost { get; }
    public int UpgradeCost { get; }
    public int Product { get; }
    public int ContractProduct{ get; }
    public int LevelCap { get; }

    public PlantDataLine(
        string name, 
        int constructionCost,
        int contractCost, 
        int upgradeCost,
        int product, 
        int contractProduct,
        int levelCap
        )
    {
        Name = name;
        ConstructionCost = constructionCost;
        ContractCost = contractCost;
        UpgradeCost = upgradeCost;
        Product = product;
        LevelCap = levelCap;
        ContractProduct = contractProduct;
    }
}

public class PlantData : IEnumerable<PlantDataLine>
{
    public PlantDataLine[] PlantDataLines { get; }

    public PlantData(PlantDataLine[] plantDataLines)
    {
        PlantDataLines = plantDataLines;
    }

    public IEnumerator<PlantDataLine> GetEnumerator()
    {
        foreach (var plantDataLine in PlantDataLines)
        {
            yield return plantDataLine;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}