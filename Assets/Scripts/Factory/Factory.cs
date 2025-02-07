using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour, IView<FactoryModel>
{
    [Header("Image")]
    [SerializeField] private Image image;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI constructionCostText;
    [SerializeField] private TextMeshProUGUI contractCostText;
    [SerializeField] private TextMeshProUGUI contractInfo;
    [SerializeField] private TextMeshProUGUI currentContractInfo;
    
    [Header("Button")]
    [SerializeField] private Button constructionButton;
    [SerializeField] private Button contractButton;
    private IGameModel playerModel;

    public int ID { get; set; }

    private void Awake()
    {
        playerModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        constructionButton.onClick.AddListener(Construction);
        contractButton.onClick.AddListener(Contract);
    }
    private void Update()
    {
        FactoryGroup.Instance.MaterialList[ID].valueText.SetText($"{playerModel.GetProductList()[ID]}");
    }
    public void Bind(FactoryModel model)
    {
        if (model == null || ID < 0 || ID >= model.Names.Length) return;

        string isNotImage = "Sprites/DebugImage/IsNotImage";
        string spriteFactoryPath = $"Sprites/Factorys/Factory_{ID}";
        Sprite loadedFactorySprite = Resources.Load<Sprite>(spriteFactoryPath) ?? Resources.Load<Sprite>(isNotImage);
        image.sprite = loadedFactorySprite;

        string spriteMaterialPath = $"Sprites/FactoryMaterials/FactoryMaterial_{ID}";
        Sprite loadedMaterialSprite = Resources.Load<Sprite>(spriteMaterialPath) ?? Resources.Load<Sprite>(isNotImage);
        FactoryGroup.Instance.MaterialList[ID].image.sprite = loadedMaterialSprite;
        
        if (!model.PendingContractCancellations[ID] && model.IsContracts[ID])
            currentContractInfo.SetText("current contract: O");
        else
            currentContractInfo.SetText("current contract: X");
        
        if (model.IsContracts[ID])
            contractInfo.SetText("[ O ]");
        else
            contractInfo.SetText("[ X ]");

        nameText.SetText(model.Names[ID]);
        levelText.SetText($"{model.Levels[ID]}/{model.LevelCaps[ID]}");
        if (!model.IsContructions[ID])
        {
            constructionCostText.SetText($"{model.ConstructionCosts[ID]:N0}$");
        }
        else
        {
            if (model.Levels[ID] < model.LevelCaps[ID])
                constructionCostText.SetText($"{model.UpgradeCosts[ID]:N0}$");
            else
                constructionCostText.SetText($"Max Level");
        }
        contractCostText.SetText($"{model.ContractCosts[ID]:N0}$");
        
    }

    public void Construction()
    {
        FactoryModel currentFactoryModel = FactoryGroup.Instance.Model;
        PlayerSystemModel playerSystemModel = playerModel.GetPlayerSystemModel();
        Debug.Log("Construction method called.");
        int cost;
        if (!currentFactoryModel.IsContructions[ID])
        {
            if (playerSystemModel.Money - currentFactoryModel.ConstructionCosts[ID] < 0) return;
            currentFactoryModel.IsContructions[ID] = true;
            cost = currentFactoryModel.ConstructionCosts[ID];

        }
        else
        {
            if (currentFactoryModel.Levels[ID] >= currentFactoryModel.LevelCaps[ID])
            {
                currentFactoryModel.Levels[ID] = currentFactoryModel.LevelCaps[ID];
                return;
            }
            if (playerSystemModel.Money - currentFactoryModel.UpgradeCosts[ID] < 0) return;
            currentFactoryModel.Levels[ID]++;
            currentFactoryModel.UpgradeCosts[ID] += currentFactoryModel.UpgradeCosts[ID] / 2;
            currentFactoryModel.Products[ID] += 2;
            cost = currentFactoryModel.UpgradeCosts[ID];
        }

        playerModel.DoFactoryResult(new
        (
            currentFactoryModel.UpgradeCosts,
            currentFactoryModel.Products,
            currentFactoryModel.Levels,
            currentFactoryModel.IsContructions
            ));
        playerModel.DoSystemResult(new
        (
            playerSystemModel.Money - cost,
            playerSystemModel.Employees,
            playerSystemModel.Resistance,
            playerSystemModel.CommunityOpinionValue
        ));
        FactoryGroup.Instance.UpdateAllPlantUI(currentFactoryModel);
    }

    public void Contract()
    {
        FactoryModel currentFactoryModel = FactoryGroup.Instance.Model;
        if (!currentFactoryModel.IsContracts[ID])
        {
            // 계약 시작
            Debug.Log($"Contract cancellation requested for Plant {ID}");
            currentFactoryModel.IsContracts[ID] = true;
            currentFactoryModel.PendingContractCancellations[ID] = false;
            playerModel.DoFactoryContractResult(new(currentFactoryModel.ContractCosts, currentFactoryModel.ContractProducts, currentFactoryModel.IsContracts));
        }
        else
        {
            if (currentFactoryModel.PendingContractCancellations[ID])
                currentFactoryModel.PendingContractCancellations[ID] = false;
            else
            {
                currentFactoryModel.PendingContractCancellations[ID] = true;
                Debug.Log($"Contract cancellation requested for Factory {ID}. It will take effect the next day.");
            }

        }

        FactoryGroup.Instance.UpdateAllPlantUI(currentFactoryModel);
    }
}
