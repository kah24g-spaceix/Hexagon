using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleView : MonoBehaviour
{
    private enum TitleButton
    {
        Start,
        Load,
        Option,
        Exit
    }
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [Header("Options Popup")]
    [SerializeField] private GameObject optionPopup;

    private void Start()
    {
        AddListeners();
    }
    private void AddListeners()
    {
        // Title buttons
        startButton.onClick.AddListener(() => ButtonType(TitleButton.Start));
        loadButton.onClick.AddListener(() => ButtonType(TitleButton.Load));
        optionsButton.onClick.AddListener(() => ButtonType(TitleButton.Option));
        exitButton.onClick.AddListener(() => ButtonType(TitleButton.Exit));
    }
    private void ButtonType(TitleButton buttonType)
    {
        switch (buttonType)
        {
            case TitleButton.Start:
                if (PlayerPrefs.HasKey("Save"))
                {
                    QuestionDialogUI.Instance.ShowQuestion(
                        "Warning!!\nPlay data remains.\nDo you want to continue?", () =>
                        {
                            ChangeScene();
                            SetLoadState(false);
                        }, () => { });
                }
                else
                {
                    ChangeScene();
                    SetLoadState(false);
                }
                break;
            case TitleButton.Load:
                if (!PlayerPrefs.HasKey("Save"))
                {
                    WarningDialogUI.Instance.ShowWarning(
                        "Warning!!\nThere is no data to load.", () =>
                        {

                        });
                }
                else
                {
                    ChangeScene();
                    SetLoadState(true);
                }

                break;
            case TitleButton.Option: PopupTrigger(optionPopup); break;
            case TitleButton.Exit: break;
        }
    }
    // Button Actions
    private void OnOptionsButtonClicked()
    {
        ShowUI(optionPopup);
    }

    private void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Utility Methods
    private void ChangeScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    private void SetLoadState(bool isLoad)
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        if (gameManager != null)
        {
            gameManager.GetComponent<GameModel>().isLoad = isLoad;
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private void ShowUI(GameObject UI)
    {
        UI.SetActive(true);
    }

    private void HideUI(GameObject UI)
    {
        UI.SetActive(false);
    }
    private void PopupTrigger(GameObject UI)
    {
        UI.SetActive(!UI.activeSelf);
    }
}
