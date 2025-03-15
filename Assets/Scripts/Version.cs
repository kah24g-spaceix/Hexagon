using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Version : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().SetText($"v{Application.version}");
    }
}
