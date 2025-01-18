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
            "Do you want to reset the game data?\nThis action cannot be undone.",
            ()=>ResetData(),
            ()=>{}
        ));
    }
    private void ResetData()
    {
        PlayerPrefs.DeleteKey("StoryDaySave");
        PlayerPrefs.DeleteKey("StorySave");
        PlayerPrefs.DeleteKey("DaySave");
        PlayerPrefs.DeleteKey("Save");
        PlayerPrefs.Save();
    }
}
