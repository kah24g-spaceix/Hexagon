using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameInformation : MonoBehaviour//, IView<HyperFrameModel>
{
    
    public Image image;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI revenueText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

}