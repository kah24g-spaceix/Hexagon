using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class PlaytimeManager : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private GameObject textHolder;
    [SerializeField] private GameObject upHolder;
    [SerializeField] private GameObject downHolder;
    private TextMeshProUGUI[] numberTexts;
    private Button[] upButtons;
    private Button[] downButtons;
    
    [Header("Input")]
    [SerializeField] private GameObject dataInput;
    [SerializeField] private Button InputFieldButton;
    private TMP_InputField dataInputField;

    private int playtimeValue;
    public int PlaytimeValue => playtimeValue;

    private void Awake()
    {
        numberTexts = textHolder.GetComponentsInChildren<TextMeshProUGUI>();
        upButtons = upHolder.GetComponentsInChildren<Button>();
        downButtons = downHolder.GetComponentsInChildren<Button>();
        dataInputField = dataInput.GetComponent<TMP_InputField>();
    }
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            upButtons[i].onClick.AddListener(() => ChangeValue(index, 1));
            downButtons[i].onClick.AddListener(() => ChangeValue(index, -1));
        }
        InputFieldButton.onClick.AddListener(DoubleClick);
        dataInputField.onEndEdit.AddListener(ValueChanged);
        dataInput.SetActive(false);
        UpdateDisplay();
    }

    private void ChangeValue(int index, int delta)
    {
        int[] digits = GetDigits(playtimeValue);
        digits[index] = Mathf.Clamp(digits[index] + delta, 0, 9);
        playtimeValue = CombineDigits(digits);

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int[] digits = GetDigits(playtimeValue);
        for (int i = 0; i < 3; i++)
        {
            numberTexts[i].text = digits[i].ToString();
        }
        dataInputField.SetTextWithoutNotify($"{playtimeValue}");
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

    readonly float interval = 0.25f;
    float doubleClickedTime = -1.0f;
    private void DoubleClick()
    {
        if ((Time.time - doubleClickedTime) < interval)
        {
            doubleClickedTime = -1.0f;
            InputActive(true);
        }
        else
        {
            doubleClickedTime = Time.time;
        }
    }
    private void ValueChanged(string text)
    {
        if (text == "") 
        {
            InputActive(false);
            return;
        }
        playtimeValue = Convert.ToInt32(text);
        UpdateDisplay();
        InputActive(false);

        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.SetGameState(
                GameStateManager.Instance.IsLoad,
                GameStateManager.Instance.IsStoryMode,
                playtimeValue,
                GameStateManager.Instance.LastDay,
                GameStateManager.Instance.InitialMoney
            );
        }
    }

    private void InputActive(bool isActive)
    {
        upHolder.SetActive(!isActive);
        downHolder.SetActive(!isActive);
        dataInput.SetActive(isActive);
    }
}
