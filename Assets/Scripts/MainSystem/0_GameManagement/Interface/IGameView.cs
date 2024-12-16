using UnityEngine;

public interface IGameView
{
    void ViewUpdate();
    void TextUIUpdate();
    void ClockUpdate(float hour, float minute);

    void ShowToDayResult();
    void HideToDayResult();
    void Resume();
    void Pause();
    void ShowUI(GameObject gameObject);
    void HideUI(GameObject gameObject);
    void ActiveTrigger(GameObject gameObject, bool active);
}