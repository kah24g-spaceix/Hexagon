using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    private GameObject targetObject;
    
    private Button closeButton;
    void Awake()
    {
        closeButton = GetComponent<Button>();
        targetObject = transform.parent.gameObject;
    }
    void Start()
    {   
        closeButton.onClick.AddListener(Close);
    }
    void Close()
    {
        targetObject.SetActive(false);
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
    }
}
