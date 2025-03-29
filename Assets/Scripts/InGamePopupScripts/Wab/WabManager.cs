using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WabManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonHolder;
    private List<Button> Buttons;
    [SerializeField] private GameObject PopupHolder;
    private List<GameObject> Popups;

    void Awake()
    {
        Buttons = new List<Button>(ButtonHolder.GetComponentsInChildren<Button>());
        Popups = new List<GameObject>();
        foreach (Transform child in PopupHolder.transform)
        {
            Popups.Add(child.gameObject);
        }
    }
    void Start()
    {
        for (int i = 0; i < Popups.Count; i++)
        {
            int index = i;
            HideUI(Popups[i]);
            Buttons[i].onClick.AddListener(() => PopupTriggerButton(Popups[index]));
        }

    }
    public void ShowUI(GameObject UI)
    {
        UI.SetActive(true);
    }
    public void HideUI(GameObject UI)
    {
        UI.SetActive(false);
    }
    public void PopupTriggerButton(GameObject UI)
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        if (!UI.activeSelf)
        {
            for (int i = 0; i < Popups.Count; i++)
            {
                HideUI(Popups[i]);
                if (Popups[i] == UI) ShowUI(UI);
            }
        }
        else UI.SetActive(false);
    }
}
