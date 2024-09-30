using UnityEngine;

public interface IGameView
{
    void HideUI(GameObject gameObject);
    void ShowUI(GameObject gameObject);
    void ActiveTrigger(GameObject gameObject, bool active);
}