using UnityEngine;

public interface IGameView
{
    void ViewUpdate();
    void TextUIUpdate();
    void ClockUpdate(float hour, float minute);
    void Resume();
    void Pause();
    void ShowUI(GameObject gameObject);
    void HideUI(GameObject gameObject);
}