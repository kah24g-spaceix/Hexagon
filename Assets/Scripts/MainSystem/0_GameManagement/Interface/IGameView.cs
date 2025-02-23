using UnityEngine;

public interface IGameView
{
    void ViewUpdate();
    void TextUIUpdate();
    public GameObject ToDayResult { get; }
    public GameObject LastDayResult { get; }
    void ClockUpdate(float hour, float minute);
    void ShowUI(GameObject gameObject);
    void HideUI(GameObject gameObject);
}