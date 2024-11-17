using UnityEngine;

public interface IGameView
{
    void TextUIUpdate();
    void ClockUpdate(string currentTime);

    void ShowToDayResult();
    void HideToDayResult();
    void ShowUI(GameObject gameObject);
    void HideUI(GameObject gameObject);
    void ActiveTrigger(GameObject gameObject, bool active);

    public bool[] GetContracts();
}