using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssassinationManager : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject RandomHolder;
    [SerializeField] private TextMeshProUGUI priceValue;
    private TextMeshProUGUI[] randomValueTexts;
    private IGameModel gameModel;
    private PlayerSystemModel playerSystemModel;
    private PlayerMaterialModel playerMaterialModel;

    private List<int> min;
    private List<int> max;
    private List<int> randomValue;
    private long price;
    readonly private int firstValue = 0;
    readonly private int middleValue = 5;
    readonly private int lastValue = 10;
    private void Awake()
    {
        gameModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        randomValueTexts = RandomHolder.GetComponentsInChildren<TextMeshProUGUI>();
        min = new List<int>();
        max = new List<int>();
        randomValue = new List<int>();
    }
    private void Start()
    {
        buyButton.onClick.AddListener(Assassinatoin);
        playerSystemModel = gameModel.GetPlayerSystemModel();
        playerMaterialModel = gameModel.GetPlayerMaterialModel();
        for (int i = 0; i < randomValueTexts.Length; i++)
        {
            min.Add(Random.Range(firstValue, middleValue));
            max.Add(Random.Range(middleValue, lastValue));
            randomValue.Add(Random.Range(min[i], max[i]));
            randomValueTexts[i].SetText($"{min[i]} ~ {max[i]}");
        }
        PriceValue();
    }
    private void Assassinatoin()
    {
        playerSystemModel = gameModel.GetPlayerSystemModel();
        playerMaterialModel = gameModel.GetPlayerMaterialModel();
        if (playerSystemModel.Money - price <= 0) 
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.Error);
            return;
        }
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Buy);
        gameModel.DoSystemResult(new
        (
            playerSystemModel.Money - price,
            playerSystemModel.Employees,
            playerSystemModel.Resistance,
            playerSystemModel.CommunityOpinionValue
        ));
        gameModel.DoMaterialResult(new
        (
            playerMaterialModel.Alloy + randomValue[0],
            playerMaterialModel.Microchip + randomValue[1],
            playerMaterialModel.CarbonFiber + randomValue[2],
            playerMaterialModel.ConductiveFiber + randomValue[3],
            playerMaterialModel.Pump + randomValue[4],
            playerMaterialModel.RubberTube + randomValue[5]
        ));
        RandomValue();
    }
    private void RandomValue()
    {
        for (int i = 0; i < randomValueTexts.Length; i++)
        {
            min[i] = Random.Range(firstValue, middleValue);
            max[i] = Random.Range(middleValue, lastValue);
            randomValue[i] = Random.Range(min[i], max[i]);
            randomValueTexts[i].SetText($"{min[i]}~{max[i]}");
        }
    }
    private void PriceValue()
    {
        price = 0;
        for (int i = 0; i < randomValueTexts.Length; i++)
        {
            price += (min[i] + max[i]) / 2;
        }
        price *= 10000;
        priceValue.SetText($"${price}");
    }
}
