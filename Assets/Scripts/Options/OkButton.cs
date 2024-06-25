using UnityEngine;
using UnityEngine.UI;

public class OkButton : MonoBehaviour
{
    [SerializeField] Button okButton;
    ResolutionOption resolutionOption;
    private void Awake()
    {
        resolutionOption = GetComponent<ResolutionOption>();
    }
    private void Start()
    {
        okButton.onClick.AddListener(resolutionOption.OkButtonClick);
    }
}
