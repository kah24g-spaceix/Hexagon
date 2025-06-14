using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InitialMoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField dataInputField;
    
    private int initialMoney;
    public int InitialMoney => initialMoney;
    void Start()
    {
        dataInputField.onEndEdit.AddListener(ValueChanged);
    }

    private void ValueChanged(string text)
    {
        if (text == "") {initialMoney = 0; return;}
        initialMoney = Convert.ToInt32(text);

        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.SetGameState(
                GameStateManager.Instance.IsLoad,
                GameStateManager.Instance.IsStoryMode,
                GameStateManager.Instance.Playtime,
                GameStateManager.Instance.LastDay,
                initialMoney
            );
        }
    }
}
