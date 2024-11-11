using UnityEngine;
public class AlloyFactory : IFactory, IResource
{
    private int purchaseCost = 100;  // ���� ���
    private int contractCost = 20;   // ��� ���
    private int alloyProduced = 0;   // ����� �˷����� ��

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // ��� �� ���������� ����Ǵ� ��� (��: ���� 1���� �˷��� ����)
            alloyProduced += amount;
            Debug.Log($"{amount} Alloy produced via contract. Total: {alloyProduced}");
        }
        else
        {
            // ���� �� �� ���� ���� (��: �� ���� ���� �� 1�� �˷��� ����)
            alloyProduced += amount;
            Debug.Log($"{amount} Alloy purchased. Total: {alloyProduced}");
        }
    }

    public int GetProducedAmount() => alloyProduced;
}
