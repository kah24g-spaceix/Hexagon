using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBoxObject;
    private DialogueBox dialogueBox;
    [SerializeField] private Dialogue[] boxes;
    private int index = 0;

    void Awake()
    {
        dialogueBox = dialogueBoxObject.GetComponent<DialogueBox>();
    }
    void Start()
    {
        ShowDialogue();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Next();
        }
    }
    public void ShowDialogue()
    {
        // if (index >= tutorials.Length)
        // {
        //     LoadingSceneManager.LoadScene("TitleScene");
        //     Time.timeScale = 1;
        //     return;
        // }
        dialogueBoxObject.SetActive(true);
        dialogueBox.text.SetText(LocalizationManager.Instance.GetLocalizedText($"tutorial.{index}"));
    }

    public void Next()
    {
        index++;
        ShowDialogue();
    }
}
