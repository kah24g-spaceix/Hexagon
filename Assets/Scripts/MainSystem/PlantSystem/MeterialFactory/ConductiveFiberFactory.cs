using UnityEngine;
public class ConductiveFiberFactory : IFactory, IResource
{
    private int purchaseCost = 250;
    private int contractCost = 50;
    private int conductiveFiberProduced = 0;  // ����� ����Ƽ�� ���̹��� ��

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // ��� �� ���������� ����Ǵ� ��� (��: ���� 1���� ����Ƽ�� ���̹� ����)
            conductiveFiberProduced += amount;
            Debug.Log($"{amount} Conductive Fiber produced via contract. Total: {conductiveFiberProduced}");
        }
        else
        {
            // ���� �� �� ���� ���� (��: �� ���� ���� �� 1�� ����Ƽ�� ���̹� ����)
            conductiveFiberProduced += amount;
            Debug.Log($"{amount} Conductive Fiber purchased. Total: {conductiveFiberProduced}");
        }
    }

    public int GetProducedAmount() => conductiveFiberProduced;
}
