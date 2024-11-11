using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameView : MonoBehaviour, IGameView
{
    [Header("UI Elements")]
    [SerializeField] private GameObject OptionUI;
    [SerializeField] private GameObject TechTreeUI;
    [SerializeField] private GameObject ToDayResultUI;
    [SerializeField] private Button NextDayButton;
    [SerializeField] private Button RestartDayButton;
    [SerializeField] private TextMeshProUGUI Day;
    [SerializeField] private TextMeshProUGUI CurrentTime;
    [SerializeField] private TextMeshProUGUI Money;
    [SerializeField] private TextMeshProUGUI Commodity;

    [Header("Plant Buy Buttons")]
    [SerializeField] private Button[] plantButtons;  // ���� ��ư �迭
    [SerializeField] private string[] plantTypes;    // �� ��ư�� �����Ǵ� ���� ���� (����)

    [Header("Plant Contract Buttons")]
    [SerializeField] private Button[] contractButtons; // ��� ��ư �迭
    [SerializeField] private string[] contractPlantTypes;  // �� ��ư�� �����Ǵ� ���� ���� (���)

    private IGamePresenter gamePresenter;
    private bool option;
    private bool techtree;
    private bool pause;

    private void Awake()
    {
        gamePresenter = GetComponent<IGamePresenter>();

        // Button Event Listeners
        NextDayButton.onClick.AddListener(gamePresenter.OnNextDayButton);
        RestartDayButton.onClick.AddListener(gamePresenter.OnRestartDayButton);

        // ���� ��ư�� �̺�Ʈ ����
        for (int i = 0; i < plantButtons.Length; i++)
        {
            int index = i; // Ŭ���� ������ ���ϱ� ���� �ε����� ����
            plantButtons[i].onClick.AddListener(() => HandleBuyPlantButton(plantTypes[index]));
        }

        // ��� ��ư�� �̺�Ʈ ����
        for (int i = 0; i < contractButtons.Length; i++)
        {
            int index = i; // Ŭ���� ������ ���ϱ� ���� �ε����� ����
            contractButtons[i].onClick.AddListener(() => HandleContractPlantButton(contractPlantTypes[index]));
        }

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
        Time.timeScale = active ? 0 : 1;
    }

    // ���� ��ư Ŭ�� �� ó��
    private void HandleBuyPlantButton(string plantType)
    {
        Debug.Log($"Buy Plant button pressed: {plantType}");
        // ���� ���� ó�� ���� �߰� (gamePresenter�� ���� ó��)
        // ��: gamePresenter.OnBuyPlant(plantType);
    }

    // ��� ��ư Ŭ�� �� ó��
    private void HandleContractPlantButton(string plantType)
    {
        Debug.Log($"Contract Plant button pressed: {plantType}");
        // ���� ��� ó�� ���� �߰� (gamePresenter�� ���� ó��)
        // ��: gamePresenter.OnContractPlant(plantType);
    }
}
