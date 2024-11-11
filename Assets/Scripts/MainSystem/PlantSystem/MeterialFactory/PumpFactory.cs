using UnityEngine;
public class PumpFactory : IFactory, IResource
{
    private int purchaseCost = 300;
    private int contractCost = 60;
    private int pumpsProduced = 0;  // ����� ������ ��

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // ��� �� ���������� ����Ǵ� ��� (��: ���� 1���� ���� ����)
            pumpsProduced += amount;
            Debug.Log($"{amount} Pump produced via contract. Total: {pumpsProduced}");
        }
        else
        {
            // ���� �� �� ���� ���� (��: �� ���� ���� �� 1�� ���� ����)
            pumpsProduced += amount;
            Debug.Log($"{amount} Pump purchased. Total: {pumpsProduced}");
        }
    }

    public int GetProducedAmount() => pumpsProduced;
}
