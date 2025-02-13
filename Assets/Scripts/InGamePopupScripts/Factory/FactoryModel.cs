public class FactoryModel
{
    public string[] Names { get; }
    public int[] ConstructionCosts { get; }
    public int[] ContractCosts { get; }
    public int[] UpgradeCosts { get; }
    public int[] Products { get; }
    public int[] ContractProducts { get; }
    public int[] LevelCaps { get; }
    public int[] Levels { get; }
    public bool[] IsContructions { get; }
    public bool[] IsContracts { get; }
    public bool[] PendingContractCancellations { get; }

    public FactoryModel(
        string[] names,
        int[] constructoinCosts,
        int[] contractCosts,
        int[] upgradeCosts,
        int[] products,
        int[] contractProducts,
        int[] levelCaps,
        int[] levels,
        bool[] isContructions,
        bool[] isContracts,
        bool[] pendingContractCancellations
    )
    {
        Names = names;
        ConstructionCosts = constructoinCosts;
        ContractCosts = contractCosts;
        UpgradeCosts = upgradeCosts;
        Products = products;
        ContractProducts = contractProducts;
        LevelCaps = levelCaps;
        Levels = levels;
        IsContructions = isContructions;
        IsContracts = isContracts;
        PendingContractCancellations = pendingContractCancellations;
    }
}