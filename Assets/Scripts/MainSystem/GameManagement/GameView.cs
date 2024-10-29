using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameView : MonoBehaviour, IGameView
{
    [Header("Option UI")]
    [SerializeField] private GameObject OptionUI;
    
    [Header("InGame UI")]
    [SerializeField] private GameObject TechTreeUI;
    [SerializeField] private GameObject ToDayResultUI;
    
    [SerializeField] private Button NextDayButton;
    [SerializeField] private Button RestartDayButton;
    
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
        //NextDayButton.onClick.AddListener(gamePresenter.OnNextDayButton);
        //RestartDayButton.onClick.AddListener(gamePresenter.OnRestartDayButton);

        option = false;
        techtree = false;
        pause = false;
    }
    private void Start()
    {
        TextUIUpdate();
        HideUI(OptionUI);
        HideUI(TechTreeUI);
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

    public void ShowToDayResult()
    {
        ToDayResultUI.gameObject.SetActive(true);
    }
    public void HideToDayResult()
    {
        ToDayResultUI.gameObject.SetActive(false);
    }
    public void ShowUI(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(true);
    }
    public void HideUI(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(false);
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