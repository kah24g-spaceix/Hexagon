using System.Collections;
using System.Collections.Generic;

public class FactoryDataLine
{
    public string Name { get; }
    public int ConstructionCost { get; }
    public int ContractCost { get; }
    public int UpgradeCost { get; }
    public int Product { get; }
    public int ContractProduct{ get; }
    public int LevelCap { get; }

    public FactoryDataLine(
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

public class FactoryData : IEnumerable<FactoryDataLine>
{
    public FactoryDataLine[] FactoryDataLines { get; }

    public FactoryData(FactoryDataLine[] plantDataLines)
    {
        FactoryDataLines = plantDataLines;
    }

    public IEnumerator<FactoryDataLine> GetEnumerator()
    {
        foreach (var plantDataLine in FactoryDataLines)
        {
            yield return plantDataLine;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}