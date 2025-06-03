using UnityEngine;
using UnityEngine.UI;

public class SkipButton : MonoBehaviour
{
    private Button button;
    void Awake()
    {
        button = GetComponent<Button>();
    }
    void Start()
    {
        button.onClick.AddListener(()=> LoadingSceneManager.LoadScene("TitleScene"));
    }
}
