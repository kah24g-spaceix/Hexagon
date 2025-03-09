using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;
    private Text uiText;
    private TMP_Text tmpText;

    void Start()
    {
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TMP_Text>();

        UpdateText();
    }

    public void UpdateText()
    {
        string localizedValue = LocalizationManager.Instance.GetLocalizedText(key);

        if (uiText != null)
        {
            uiText.text = localizedValue;
        }
        else if (tmpText != null)
        {
            tmpText.text = localizedValue;
        }
    }
}
