using UnityEngine;

public interface IGameView
{
    void ViewUpdate();
    void TextUIUpdate();
    void ClockUpdate(float hour, float minute);
    void ShowUI(GameObject gameObject);
    void HideUI(GameObject gameObject);
}