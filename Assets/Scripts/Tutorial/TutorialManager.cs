using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Tutorial[] tutorials;
    Button targetButton;
    private int index = 0;
    private enum Block
    {
        Left,
        Right,
        Top,
        Bottom
    }
    void Start()
    {
        Time.timeScale = 0;
        Tutorial();
    }
    public void Tutorial()
    {
        if (index >= tutorials.Length)
        {
            LoadingSceneManager.LoadScene("TitleScene");
            Time.timeScale = 1;
            return;
        }
        tutorials[index].tutorial_Object.SetActive(true);
        text.SetText(LocalizationManager.Instance.GetLocalizedText($"tutorial.{index}"));

        targetButton = tutorials[index].tutorial_Object.GetComponent<Button>();

        if (targetButton != null)
        {
            targetButton.onClick.RemoveListener(Next);
            targetButton.onClick.AddListener(Next);
        }
    }

    public void Next()
    {
        targetButton.onClick.RemoveListener(Next);
        index++;
        Tutorial();
    }
}