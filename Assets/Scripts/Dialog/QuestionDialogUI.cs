using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class QuestionDialogUI : MonoBehaviour
{
    public static QuestionDialogUI Instance {get; private set;}
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI yesText;
    [SerializeField] private TextMeshProUGUI noText;

    private void Awake()
    {
        Instance = this;
        Hide();
    }
    public void ShowQuestion(string questionText, Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);

        textMeshPro.text = questionText;
        yesText.text = LocalizationManager.Instance.GetLocalizedText("question.yes");
        noText.text = LocalizationManager.Instance.GetLocalizedText("question.no");
        yesBtn.onClick.AddListener(() => {
            Hide();
            yesAction();
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        });
        noBtn.onClick.AddListener(() => {
            Hide();
            noAction();
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        });
    }

    private void Hide() 
    {
        gameObject.SetActive(false);
    }
}
