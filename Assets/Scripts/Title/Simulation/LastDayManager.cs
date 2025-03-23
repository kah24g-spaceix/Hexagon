using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LastDayManager : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject upButton;
    [SerializeField] private GameObject downButton;

    private Button up;
    private Button down;

    [Header("Input")]
    [SerializeField] private GameObject dataInput;
    [SerializeField] private Button InputFieldButton;
    private TMP_InputField dataInputField;

    private int lastDay;
    public int LastDay => lastDay;

    private void Awake()
    {
        dataInputField = dataInput.GetComponent<TMP_InputField>();
        up = upButton.GetComponent<Button>();
        down = downButton.GetComponent<Button>();
    }
    private void Start()
    {
        up.onClick.AddListener(() => ChangeValue(1));
        down.onClick.AddListener(() => ChangeValue(-1));
        InputFieldButton.onClick.AddListener(DoubleClick);
        dataInputField.onEndEdit.AddListener(ValueChanged);
        dataInput.SetActive(false);
    }
    private void UpdateDisplay()
    {
        text.SetText($"{lastDay}");
        dataInputField.SetTextWithoutNotify($"{lastDay}");
    }
    private void ChangeValue(int delta)
    {
        lastDay = Mathf.Clamp(lastDay + delta, 0, 99);
        UpdateDisplay();
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
    lastDay = Convert.ToInt32(text);
    UpdateDisplay();
    InputActive(false);

    if (GameStateManager.Instance != null)
    {
        GameStateManager.Instance.SetGameState(
            GameStateManager.Instance.IsLoad,
            GameStateManager.Instance.IsStoryMode,
            GameStateManager.Instance.Playtime,
            lastDay,
            GameStateManager.Instance.InitialMoney
        );
    }
}
    private void InputActive(bool isActive)
    {
        upButton.SetActive(!isActive);
        downButton.SetActive(!isActive);
        dataInput.SetActive(isActive);
    }
}
