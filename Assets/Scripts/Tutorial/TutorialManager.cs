using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private RectTransform[] _blocks;
    [SerializeField] private Tutorial[] tutorials;
    [SerializeField] private TextMeshProUGUI text;
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
        Tutorial();
    }
    void Update()
    {
        Time.timeScale = 0;
    }
    public void Tutorial()
    {
        if (index >= tutorials.Length)
        {
            LoadingSceneManager.LoadScene("TitleScene");
            Time.timeScale = 1;
            return;
        }
        RectTransform targetRect = tutorials[index].tutorial_Object.GetComponent<RectTransform>();
        Button targetButton = tutorials[index].tutorial_Object.GetComponent<Button>();
        text.SetText(tutorials[index].explanation);
        //BlockInputExceptRect(targetRect);
        tutorials[index].tutorial_Object.SetActive(true);
        targetButton.onClick.RemoveListener(Index);
        targetButton.onClick.AddListener(Index);
    }
    public void Index()
    {
        index++;
        Tutorial();
    }
    public void BlockInputExceptRect(RectTransform targetRectTM)
    {
        Debug.Log($"{targetRectTM.position.x} | {targetRectTM.rect.width}");
        _blocks[(int)Block.Left].sizeDelta = new Vector2(targetRectTM.position.x - targetRectTM.rect.width / 2, 0);
        _blocks[(int)Block.Right].sizeDelta = new Vector2(Screen.width - targetRectTM.position.x - targetRectTM.rect.width / 2, 0);
        _blocks[(int)Block.Top].sizeDelta = new Vector2(0, Screen.height - targetRectTM.position.y - targetRectTM.rect.height / 2);
        _blocks[(int)Block.Bottom].sizeDelta = new Vector2(0, targetRectTM.position.y - targetRectTM.rect.height / 2);
        for (int i = 0; i < _blocks.Length; i++)
            _blocks[i].gameObject.SetActive(true);
    }

    public void HideBlocks()
    {
        for (int i = 0; i < _blocks.Length; i++)
            _blocks[i].gameObject.SetActive(false);
    }
}