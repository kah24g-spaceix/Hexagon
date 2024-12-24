using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour, IView<FactoryModel>
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text constructionCostText;
    [SerializeField] private TMP_Text contractCostText;
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

    public void Bind(FactoryModel model)
    {
        if (model == null || ID < 0 || ID >= model.Names.Length) return;

        string spritePath = $"Resources/Sprites/Factorys/Factory_{ID}";
        string isNotImage = "Resources/Sprites/DebugImage/IsNotImage";
        Sprite loadedSprite = Resources.Load<Sprite>(spritePath) ?? Resources.Load<Sprite>(isNotImage);
        
        image.sprite = loadedSprite;
        nameText.SetText(model.Names[ID]);
        levelText.SetText($"{model.Levels[ID]}/{model.LevelCaps[ID]}");
        if (!model.IsContructions[ID])
        {
            constructionCostText.SetText($"{model.ConstructionCosts[ID]}H$");
        }
        else
        {
            if (model.Levels[ID] < model.LevelCaps[ID])
                constructionCostText.SetText($"{model.UpgradeCosts[ID]}H$");
            else
                constructionCostText.SetText($"Max Level");
        }
        contractCostText.SetText($"{model.ContractCosts[ID]}H$");
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

        playerModel.DoPlantResult(new
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
            playerSystemModel.CommunityOpinionValue,
            playerSystemModel.Day
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
            playerModel.DoPlantContractResult(new(currentFactoryModel.ContractCosts, currentFactoryModel.ContractProducts, currentFactoryModel.IsContracts));
        }
        else
        {
            // 계약 취소 요청 기록
            currentFactoryModel.PendingContractCancellations[ID] = true;
            Debug.Log($"Contract cancellation requested for Plant {ID}. It will take effect the next day.");
        }

        FactoryGroup.Instance.UpdateAllPlantUI(currentFactoryModel);
    }
}
