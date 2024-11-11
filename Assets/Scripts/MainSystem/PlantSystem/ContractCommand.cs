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
            // 계약 실행: 지속적으로 차감되는 로직
            //_playerSystemModel.Money -= _contractCost; // 계약 체결 비용 차감
            Debug.Log("Contract signed");
            // 계약을 통한 자원 생산 (자동으로 이루어짐)
        }
        else
        {
            Debug.Log("Not enough money for the contract.");
        }
    }
}
