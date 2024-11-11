using UnityEngine;
public class CarbonFiberFactory : IFactory, IResource
{
    private int purchaseCost = 200;
    private int contractCost = 40;
    private int carbonFiberProduced = 0;  // 생산된 카본파이버의 양

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // 계약 시 지속적으로 생산되는 방식 (예: 매일 1개의 카본 파이버 생산)
            carbonFiberProduced += amount;
            Debug.Log($"{amount} Carbon Fiber produced via contract. Total: {carbonFiberProduced}");
        }
        else
        {
            // 구매 시 한 번만 생산 (예: 한 번의 구매 시 1개 카본 파이버 생산)
            carbonFiberProduced += amount;
            Debug.Log($"{amount} Carbon Fiber purchased. Total: {carbonFiberProduced}");
        }
    }

    public int GetProducedAmount() => carbonFiberProduced;
}
