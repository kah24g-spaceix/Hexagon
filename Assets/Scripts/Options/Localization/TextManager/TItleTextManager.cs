using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleTextManager : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private TextSetting start;
    [SerializeField] private TextSetting load;
    [SerializeField] private TextSetting option;
    [SerializeField] private TextSetting exit;
    [SerializeField] private TextSetting tutorial;
    [Header("Select mode")]
    [SerializeField] private TextSetting story;
    [SerializeField] private TextSetting simulation;

    [Header("Simulation")]
    [SerializeField] private TextSetting daily;
    [SerializeField] private TextSetting lastDay;
    [SerializeField] private TextSetting initMoney;
    [SerializeField] private TextSetting play;
    private void Update()
    {
        start.text.SetText(LocalizationManager.Instance.GetLocalizedText("start"));
        load.text.SetText(LocalizationManager.Instance.GetLocalizedText("load"));
        option.text.SetText(LocalizationManager.Instance.GetLocalizedText("option"));
        exit.text.SetText(LocalizationManager.Instance.GetLocalizedText("exit"));
        tutorial.text.SetText(LocalizationManager.Instance.GetLocalizedText("tutorial"));

        story.text.SetText(LocalizationManager.Instance.GetLocalizedText("story"));
        simulation.text.SetText(LocalizationManager.Instance.GetLocalizedText("simulation"));

        daily.text.SetText(LocalizationManager.Instance.GetLocalizedText("playTime.daily"));
        lastDay.text.SetText(LocalizationManager.Instance.GetLocalizedText("playTime.lastDay"));
        initMoney.text.SetText(LocalizationManager.Instance.GetLocalizedText("playTime.initMoney"));
        play.text.SetText(LocalizationManager.Instance.GetLocalizedText("playTime.play"));
    }
}