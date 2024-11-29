public struct PlantModel
{
    public string[] Names { get; }
    public int[] ConstructionCosts { get; }
    public int[] ContractCosts {get;}
    public int[] Product { get; }
    public int[] Levels { get; }
    public PlantModel(
        string[] names,
        string[] imageNames,
        int[] constructoinCosts,
        int[] contractCosts,
        int[] product,
        int[] levels
    )
    {
        Names = names;
        ConstructionCosts = constructoinCosts;
        ContractCosts = contractCosts;
        Product = product;
        Levels = levels;
    }
}