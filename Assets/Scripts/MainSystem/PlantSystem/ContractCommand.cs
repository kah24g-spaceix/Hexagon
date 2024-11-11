using UnityEngine;
public class ContractCommand : ITransactionCommand
{
    private readonly IFactory _factory;
    private readonly PlayerSystemModel _playerSystemModel;
    private readonly int _contractCost;

    public ContractCommand(IFactory factory, PlayerSystemModel playerSystemModel, int contractCost)
    {
        _factory = factory;
        _playerSystemModel = playerSystemModel;
        _contractCost = contractCost;
    }

    public void Execute()
    {
        if (_playerSystemModel.Money >= _contractCost)
        {
            // ��� ����: ���������� �����Ǵ� ����
            //_playerSystemModel.Money -= _contractCost; // ��� ü�� ��� ����
            Debug.Log("Contract signed");
            // ����� ���� �ڿ� ���� (�ڵ����� �̷����)
        }
        else
        {
            Debug.Log("Not enough money for the contract.");
        }
    }
}
