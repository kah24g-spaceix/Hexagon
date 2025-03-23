using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTextManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private TextSetting pause;
    [SerializeField] private TextSetting resume;
    [SerializeField] private TextSetting option;
    [SerializeField] private TextSetting save;
    [SerializeField] private TextSetting title;
    [SerializeField] private TextSetting exit;

    [Header("InGameUI")]
    [SerializeField] private TextSetting reStartDay;
    [SerializeField] private TextSetting nextDay;
    [SerializeField] private TextSetting next;
    private void Update()
    {
        pause.text.SetText(LocalizationManager.Instance.GetLocalizedText("pause"));
        resume.text.SetText(LocalizationManager.Instance.GetLocalizedText("resume"));
        option.text.SetText(LocalizationManager.Instance.GetLocalizedText("option"));
        save.text.SetText(LocalizationManager.Instance.GetLocalizedText("save"));
        title.text.SetText(LocalizationManager.Instance.GetLocalizedText("title"));
        exit.text.SetText(LocalizationManager.Instance.GetLocalizedText("exit"));
        
        reStartDay.text.SetText(LocalizationManager.Instance.GetLocalizedText("reStartDay"));
        nextDay.text.SetText(LocalizationManager.Instance.GetLocalizedText("nextDay"));
        next.text.SetText(LocalizationManager.Instance.GetLocalizedText("next"));
    }
}