public struct PlantModel
{
    public string[] Names { get; }
    public int[] ConstructionCosts { get; }
    public int[] ContractCosts { get; }
    public int[] UpgradeCosts { get; }
    public int[] Products { get; }
    public int[] ImportProducts { get; }
    public int[] Levels { get; }
    public bool[] IsContructions { get; }
    public bool[] IsContracts { get; }

    public PlantModel(
        string[] names,
        int[] constructoinCosts,
        int[] contractCosts,
        int[] upgradeCosts,
        int[] products,
        int[] importProducts,
        int[] levels,
        bool[] isContructions,
        bool[] isContracts
    )
    {
        Names = names;
        ConstructionCosts = constructoinCosts;
        ContractCosts = contractCosts;
        UpgradeCosts = upgradeCosts;
        Products = products;
        ImportProducts = importProducts;
        Levels = levels;
        IsContructions = isContructions;
        IsContracts = isContracts;
    }
}