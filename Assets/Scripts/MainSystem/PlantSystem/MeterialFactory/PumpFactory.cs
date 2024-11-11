using UnityEngine;
public class PumpFactory : IFactory, IResource
{
    private int purchaseCost = 300;
    private int contractCost = 60;
    private int pumpsProduced = 0;  // 생산된 펌프의 양

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // 계약 시 지속적으로 생산되는 방식 (예: 매일 1개의 펌프 생산)
            pumpsProduced += amount;
            Debug.Log($"{amount} Pump produced via contract. Total: {pumpsProduced}");
        }
        else
        {
            // 구매 시 한 번만 생산 (예: 한 번의 구매 시 1개 펌프 생산)
            pumpsProduced += amount;
            Debug.Log($"{amount} Pump purchased. Total: {pumpsProduced}");
        }
    }

    public int GetProducedAmount() => pumpsProduced;
}
