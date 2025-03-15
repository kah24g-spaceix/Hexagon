using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        UpdateText();
    }

    public void UpdateText()
    {
        text.SetText(LocalizationManager.Instance.GetLocalizedText(text.text));
    }
}
