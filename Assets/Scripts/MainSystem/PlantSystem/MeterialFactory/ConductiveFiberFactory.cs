using UnityEngine;
public class ConductiveFiberFactory : IFactory, IResource
{
    private int purchaseCost = 250;
    private int contractCost = 50;
    private int conductiveFiberProduced = 0;  // 생산된 컨덕티브 파이버의 양

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // 계약 시 지속적으로 생산되는 방식 (예: 매일 1개의 컨덕티브 파이버 생산)
            conductiveFiberProduced += amount;
            Debug.Log($"{amount} Conductive Fiber produced via contract. Total: {conductiveFiberProduced}");
        }
        else
        {
            // 구매 시 한 번만 생산 (예: 한 번의 구매 시 1개 컨덕티브 파이버 생산)
            conductiveFiberProduced += amount;
            Debug.Log($"{amount} Conductive Fiber purchased. Total: {conductiveFiberProduced}");
        }
    }

    public int GetProducedAmount() => conductiveFiberProduced;
}
