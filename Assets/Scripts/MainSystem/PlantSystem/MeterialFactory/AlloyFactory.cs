using UnityEngine;
public class AlloyFactory : IFactory, IResource
{
    private int purchaseCost = 100;  // 구매 비용
    private int contractCost = 20;   // 계약 비용
    private int alloyProduced = 0;   // 생산된 알로이의 양

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // 계약 시 지속적으로 생산되는 방식 (예: 매일 1개의 알로이 생산)
            alloyProduced += amount;
            Debug.Log($"{amount} Alloy produced via contract. Total: {alloyProduced}");
        }
        else
        {
            // 구매 시 한 번만 생산 (예: 한 번의 구매 시 1개 알로이 생산)
            alloyProduced += amount;
            Debug.Log($"{amount} Alloy purchased. Total: {alloyProduced}");
        }
    }

    public int GetProducedAmount() => alloyProduced;
}
