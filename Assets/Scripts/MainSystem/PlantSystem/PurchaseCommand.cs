using UnityEngine;
public class PurchaseCommand : ITransactionCommand
{
    private readonly IFactory _factory;
    private readonly PlayerSystemModel _playerSystemModel;
    private readonly int _cost;

    public PurchaseCommand(IFactory factory, PlayerSystemModel playerSystemModel, int cost)
    {
        _factory = factory;
        _playerSystemModel = playerSystemModel;
        _cost = cost;
    }

    public void Execute()
    {
        if (_playerSystemModel.Money >= _cost)
        {
            // ���� ����
            //_playerSystemModel.Money -= _cost;
            _factory.Produce(0, false); // ���� �� ù ����
            Debug.Log("Purchase completed");
        }
        else
        {
            Debug.Log("Not enough money to make the purchase.");
        }
    }
}

