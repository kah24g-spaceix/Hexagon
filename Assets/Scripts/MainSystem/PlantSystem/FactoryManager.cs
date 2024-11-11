using System.Collections.Generic;

public class FactoryManager
{
    private Dictionary<string, IFactory> _factories;

    public FactoryManager()
    {
        _factories = new Dictionary<string, IFactory>
        {
            { "Alloy", new AlloyFactory() },
            { "Microchip", new MicrochipFactory() },
            { "CarbonFiber", new CarbonFiberFactory() },
            { "ConductiveFiber", new ConductiveFiberFactory() },
            { "Pump", new PumpFactory() },
            { "RubberTube", new RubberTubeFactory() }
        };
    }

    public IFactory GetFactory(string factoryType)
    {
        return _factories.ContainsKey(factoryType) ? _factories[factoryType] : null;
    }

    public void ExecuteTransaction(string factoryType, bool isContract, PlayerSystemModel playerSystemModel)
    {
        IFactory factory = GetFactory(factoryType);

        if (factory is IResource resource)
        {
            int cost = isContract ? resource.GetContractCost() : resource.GetPurchaseCost();

            if (isContract)
            {
                var contractCommand = new ContractCommand(factory, playerSystemModel, cost);
                contractCommand.Execute();
            }
            else
            {
                var purchaseCommand = new PurchaseCommand(factory, playerSystemModel, cost);
                purchaseCommand.Execute();
            }
        }
    }
}
