using UnityEngine;
public class CarbonFiberFactory : IFactory, IResource
{
    private int purchaseCost = 200;
    private int contractCost = 40;
    private int carbonFiberProduced = 0;  // ����� ī�����̹��� ��

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // ��� �� ���������� ����Ǵ� ��� (��: ���� 1���� ī�� ���̹� ����)
            carbonFiberProduced += amount;
            Debug.Log($"{amount} Carbon Fiber produced via contract. Total: {carbonFiberProduced}");
        }
        else
        {
            // ���� �� �� ���� ���� (��: �� ���� ���� �� 1�� ī�� ���̹� ����)
            carbonFiberProduced += amount;
            Debug.Log($"{amount} Carbon Fiber purchased. Total: {carbonFiberProduced}");
        }
    }

    public int GetProducedAmount() => carbonFiberProduced;
}
