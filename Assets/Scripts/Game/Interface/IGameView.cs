using UnityEngine;

public interface IGameView
{
    void TextUIUpdate();
    void ClockUpdate(string currentTime);
    void HideUI(GameObject gameObject);
    void ShowUI(GameObject gameObject);
    void ActiveTrigger(GameObject gameObject, bool active);
}