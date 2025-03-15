using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataReset : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => QuestionDialogUI.Instance.ShowQuestion
        (
            LocalizationManager.Instance.GetLocalizedText("question.reset"),
            ()=>ResetData(),
            ()=>{}
        ));
    }
    private void ResetData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        LoadingSceneManager.LoadScene("TitleScene");
    }
}
