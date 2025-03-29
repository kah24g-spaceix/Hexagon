using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetManager : MonoBehaviour
{
    [SerializeField] private GameObject amountHolder;
    public List<Button> amountButtons { get; set; }
    public Button resetButton;
    public TextMeshProUGUI currentBetText;
    public TextMeshProUGUI multipleText;
    private void Awake()
    {
        amountButtons = new List<Button>(amountHolder.GetComponentsInChildren<Button>());
    }
    private void Start()
    {
        multipleText.SetText("?");
    }

}
