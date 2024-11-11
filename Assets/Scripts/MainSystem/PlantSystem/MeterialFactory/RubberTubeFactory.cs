using UnityEngine;
public class RubberTubeFactory : IFactory, IResource
{
    private int purchaseCost = 120;
    private int contractCost = 25;
    private int rubberTubesProduced = 0;  // 생산된 고무 튜브의 양

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // 계약 시 지속적으로 생산되는 방식 (예: 매일 1개의 고무 튜브 생산)
            rubberTubesProduced += amount;
            Debug.Log($"{amount} Rubber Tube produced via contract. Total: {rubberTubesProduced}");
        }
        else
        {
            // 구매 시 한 번만 생산 (예: 한 번의 구매 시 1개 고무 튜브 생산)
            rubberTubesProduced += amount;
            Debug.Log($"{amount} Rubber Tube purchased. Total: {rubberTubesProduced}");
        }
    }

    public int GetProducedAmount() => rubberTubesProduced;
}
