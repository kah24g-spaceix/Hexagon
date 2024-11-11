using UnityEngine;
public class MicrochipFactory : IFactory, IResource
{
    private int purchaseCost = 150;
    private int contractCost = 30;
    private int microchipsProduced = 0;  // ����� ����ũ��Ĩ�� ��

    public int GetPurchaseCost() => purchaseCost;
    public int GetContractCost() => contractCost;

    public void Produce(int amount, bool isContract)
    {
        if (isContract)
        {
            // ��� �� ���������� ����Ǵ� ��� (��: ���� 1���� ����ũ��Ĩ ����)
            microchipsProduced += amount;
            Debug.Log($"{amount} Microchip produced via contract. Total: {microchipsProduced}");
        }
        else
        {
            // ���� �� �� ���� ���� (��: �� ���� ���� �� 1�� ����ũ��Ĩ ����)
            microchipsProduced += amount;
            Debug.Log($"{amount} Microchip purchased. Total: {microchipsProduced}");
        }
    }

    public int GetProducedAmount() => microchipsProduced;
}
