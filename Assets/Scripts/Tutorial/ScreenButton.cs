using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(DisableButton);
    }
    
    public void DisableButton()
    {
        gameObject.SetActive(false);
    }
}
