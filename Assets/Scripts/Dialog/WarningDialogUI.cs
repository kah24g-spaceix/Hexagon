using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WarningDialogUI : MonoBehaviour
{
    public static WarningDialogUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button okBtn;

    private void Awake()
    {
        Instance = this;
        Hide();
    }
    public void ShowWarning(string warningText, Action okAction)
    {
        gameObject.SetActive(true);

        textMeshPro.text = warningText;
        okBtn.onClick.AddListener(() =>
        {
            Hide();
            okAction();
            AudioManager.Instance.PlaySFX("Select");
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
