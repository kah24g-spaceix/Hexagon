using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlaytimeManager : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private GameObject textHolder;
    [SerializeField] private GameObject upHolder;
    [SerializeField] private GameObject downHolder;
    
    [Header("Input")]
    [SerializeField] private GameObject InputField;
    [SerializeField] private Button InputFieldButton;
    private InputField inputField;
    private TextMeshProUGUI[] numberTexts;
    private Button[] upButtons;
    private Button[] downButtons;
    private int playtimeValue;
    public int PlaytimeValue => playtimeValue;

    private void Awake()
    {
        numberTexts = textHolder.GetComponentsInChildren<TextMeshProUGUI>();
        upButtons = upHolder.GetComponentsInChildren<Button>();
        downButtons = downHolder.GetComponentsInChildren<Button>();
        inputField = InputField.GetComponent<InputField>();
    }
    private void Start()
    {
        // 버튼에 동적으로 AddListener 연결
        for (int i = 0; i < 3; i++)
        {
            int index = i; // 로컬 변수로 캡처
            upButtons[i].onClick.AddListener(() => ChangeNumber(index, 1));
            downButtons[i].onClick.AddListener(() => ChangeNumber(index, -1));
        }
        //InputFieldButton.onClick.AddListener();
        UpdateDisplay();
    }

    // 특정 자릿수의 숫자를 변경
    private void ChangeNumber(int index, int delta)
    {
        int[] digits = GetDigits(playtimeValue);
        digits[index] = Mathf.Clamp(digits[index] + delta, 0, 9); // 0~9로 제한
        playtimeValue = CombineDigits(digits);

        UpdateDisplay();
    }

    // 화면에 숫자 갱신
    private void UpdateDisplay()
    {
        int[] digits = GetDigits(playtimeValue);
        for (int i = 0; i < 3; i++)
        {
            numberTexts[i].text = digits[i].ToString();
        }
    }

    private int[] GetDigits(int value)
    {
        int[] digits = new int[3];
        for (int i = 0; i < 3; i++)
        {
            digits[2 - i] = value % 10;
            value /= 10;
        }
        return digits;
    }

    private int CombineDigits(int[] digits)
    {
        int value = 0;
        for (int i = 0; i < 3; i++)
        {
            value = value * 10 + digits[i];
        }
        return value;
    }

    float interval = 0.25f;
    float doubleClickedTime = -1.0f;
    bool isDoubleClicked = false;
    private void DoubleClick()
    {
        if ((Time.time - doubleClickedTime) < interval)
        {
            isDoubleClicked = true;
            doubleClickedTime = -1.0f;

            Debug.Log("double click!");
        }
        else
        {
            isDoubleClicked = false;
            doubleClickedTime = Time.time;
        }
    }
}
