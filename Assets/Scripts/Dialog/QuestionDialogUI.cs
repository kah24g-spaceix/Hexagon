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

    private void Awake()
    {
        Instance = this;
        Hide();
    }
    public void ShowQuestion(string questionText, Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);

        textMeshPro.text = questionText;
        yesBtn.onClick.AddListener(() => {
            Hide();
            yesAction();
            AudioManager.Instance.PlaySFX("Select");
        });
        noBtn.onClick.AddListener(() => {
            Hide();
            noAction();
            AudioManager.Instance.PlaySFX("Select");
        });
    }

    private void Hide() 
    {
        gameObject.SetActive(false);
    }
}
