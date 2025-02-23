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
    }
    void Start()
    {
        button.onClick.AddListener(()=> gameObject.SetActive(false));
    }
}
