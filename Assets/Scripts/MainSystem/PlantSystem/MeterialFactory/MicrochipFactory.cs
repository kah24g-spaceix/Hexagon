using UnityEngine;
public class MicrochipFactory : IFactory, IResource
{
    private int purchaseCost = 150;
    private int contractCost = 30;
    private int microchipsProduced = 0;  // 생산된 마이크로칩의 양

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // 계약 시 지속적으로 생산되는 방식 (예: 매일 1개의 마이크로칩 생산)
            microchipsProduced += amount;
            Debug.Log($"{amount} Microchip produced via contract. Total: {microchipsProduced}");
        }
        else
        {
            // 구매 시 한 번만 생산 (예: 한 번의 구매 시 1개 마이크로칩 생산)
            microchipsProduced += amount;
            Debug.Log($"{amount} Microchip purchased. Total: {microchipsProduced}");
        }
    }

    public int GetProducedAmount() => microchipsProduced;
}
