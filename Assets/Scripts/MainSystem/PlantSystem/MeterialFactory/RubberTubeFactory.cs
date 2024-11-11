using UnityEngine;
public class RubberTubeFactory : IFactory, IResource
{
    private int purchaseCost = 120;
    private int contractCost = 25;
    private int rubberTubesProduced = 0;  // ����� �� Ʃ���� ��

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // ��� �� ���������� ����Ǵ� ��� (��: ���� 1���� �� Ʃ�� ����)
            rubberTubesProduced += amount;
            Debug.Log($"{amount} Rubber Tube produced via contract. Total: {rubberTubesProduced}");
        }
        else
        {
            // ���� �� �� ���� ���� (��: �� ���� ���� �� 1�� �� Ʃ�� ����)
            rubberTubesProduced += amount;
            Debug.Log($"{amount} Rubber Tube purchased. Total: {rubberTubesProduced}");
        }
    }

    public int GetProducedAmount() => rubberTubesProduced;
}
