using UnityEngine;
using UnityEngine.UI;

public class OkButton : MonoBehaviour
{
    [SerializeField] Button okButton;
    private void Start()
    {
        okButton.onClick.AddListener
            (GetComponent<ResolutionOption>().OkButtonClick);
    }
}
