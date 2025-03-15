using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionTextManager : MonoBehaviour
{
    [SerializeField] private TextSetting fullScreen;
    [SerializeField] private TextSetting resolution;
    [SerializeField] private TextSetting language;
    [SerializeField] private TextSetting apply;
    private void Update()
    {
        fullScreen.text.SetText(LocalizationManager.Instance.GetLocalizedText("option.fullscreen"));
        resolution.text.SetText(LocalizationManager.Instance.GetLocalizedText("option.resolution"));
        language.text.SetText(LocalizationManager.Instance.GetLocalizedText("option.language"));
        apply.text.SetText(LocalizationManager.Instance.GetLocalizedText("option.apply"));
    }
}