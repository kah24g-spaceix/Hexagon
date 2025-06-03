using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject SpaceBar;
    [SerializeField] private GameObject dialogueBoxObject;
    private DialogueBox dialogueBox;
    [SerializeField] private Dialogue[] boxes;

    private int index = 0;
    private bool isTyping = false;
    private string fullText;
    private Coroutine typingCoroutine;

    void Awake()
    {
        dialogueBox = dialogueBoxObject.GetComponent<DialogueBox>();
    }

    void Start()
    {
        dialogueBoxObject.SetActive(false);
        SpaceBar.SetActive(true);
        ShowDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceBar.SetActive(false);
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueBox.text.SetText(fullText);
                isTyping = false;
            }
            else
            {
                Next();
            }
        }
    }

    public void ShowDialogue()
    {
        if (index >= boxes.Length)
        {
            LoadingSceneManager.LoadScene("TitleScene");
            return;
        }
        dialogueBox.target = boxes[index].targetObject;
        dialogueBoxObject.SetActive(true);

        fullText = LocalizationManager.Instance.GetLocalizedText($"intro.{index}");

        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueBox.text.SetText("");
        
        foreach (char letter in text)
        {
            dialogueBox.text.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        
        isTyping = false;
    }

    public void Next()
    {
        index++;
        dialogueBoxObject.SetActive(false);
        ShowDialogue();
    }
}
