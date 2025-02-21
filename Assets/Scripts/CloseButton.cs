using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    private GameObject closeObject;
    private Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        closeObject = transform.parent.gameObject;
    }
    void Start()
    {   
        button.onClick.AddListener(Close);
    }
    void Close()
    {
        closeObject.SetActive(false);
        AudioManager.Instance.PlaySFX("Select");
    }
}
