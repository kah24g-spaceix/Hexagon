using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameView : MonoBehaviour, IGameView
{
    [Header("Option UI")]
    [SerializeField] private GameObject OptionUI;
    [Header("InGame UI")]
    [SerializeField] private GameObject TechTreeUI;
    [Header("InGame Value UI")]
    [SerializeField] private TextMeshProUGUI Day;
    [SerializeField] private TextMeshProUGUI CurrentTime;
    [SerializeField] private TextMeshProUGUI Money;
    [SerializeField] private TextMeshProUGUI Commodity;

    IGamePresenter gamePresenter;
    private bool option;
    private bool techtree;
    private bool pause;
    // Start is called before the first frame update
    private void Awake()
    {
        gamePresenter = GetComponent<IGamePresenter>();
        TextUIUpdate();
        HideUI(OptionUI);
        HideUI(TechTreeUI);
        option = false;
        techtree = false;
        pause = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            option = !option;
            ActiveTrigger(OptionUI, option);
            Pause(option);
        }
        if (Input.GetKeyDown(KeyCode.T) && !pause)
        {
            techtree = !techtree;
            ActiveTrigger(TechTreeUI, techtree);
        }
    }


    public void TextUIUpdate()
    {
        Day.text = gamePresenter.GetDay();
        Money.text = gamePresenter.GetMoney();
    }
    public void ClockUpdate(string currentTime)
    {
        CurrentTime.text = currentTime;
    }
    public void HideUI(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(false);
    }
    public void ShowUI(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(true);
    }
    public void ActiveTrigger(GameObject gameObject, bool active)
    {
        gameObject.gameObject.SetActive(active);
    }
    private void Pause(bool active)
    {
        pause = active;
        if (active)
        {
            Time.timeScale = 0; // 게임 정지
        }
        else
        {
            Time.timeScale = 1; // 게임 재개
        }
    }
}