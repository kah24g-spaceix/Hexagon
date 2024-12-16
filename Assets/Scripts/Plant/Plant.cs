using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour, IView<PlantModel>
{
    [SerializeField] private Image plantImage;
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

    public void Bind(PlantModel model)
    {
        if (model == null || ID < 0 || ID >= model.Names.Length) return;

        string spritePath = $"Sprites/Plants/Plant_{ID}";
        Sprite loadedSprite = Resources.Load<Sprite>(spritePath) ?? Resources.Load<Sprite>("Sprites/Default");

        plantImage.sprite = loadedSprite;
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
        PlantModel currentPlantModel = PlantGroup.Instance.PlantModel;
        PlayerSystemModel playerSystemModel = playerModel.GetPlayerSystemModel();
        Debug.Log("Construction method called.");
        int cost;
        if (!currentPlantModel.IsContructions[ID])
        {
            if (playerSystemModel.Money - currentPlantModel.ConstructionCosts[ID] < 0) return;
            currentPlantModel.IsContructions[ID] = true;
            cost = currentPlantModel.ConstructionCosts[ID];

        }
        else
        {
            if (currentPlantModel.Levels[ID] >= currentPlantModel.LevelCaps[ID])
            {
                currentPlantModel.Levels[ID] = currentPlantModel.LevelCaps[ID];
                return;
            }
            if (playerSystemModel.Money - currentPlantModel.UpgradeCosts[ID] < 0) return;
            currentPlantModel.Levels[ID]++;
            currentPlantModel.UpgradeCosts[ID] += currentPlantModel.UpgradeCosts[ID] / 2;
            currentPlantModel.Products[ID] += 2;
            cost = currentPlantModel.UpgradeCosts[ID];
        }

        playerModel.DoPlantResult(new
        (
            currentPlantModel.UpgradeCosts,
            currentPlantModel.Products,
            currentPlantModel.Levels,
            currentPlantModel.IsContructions
            ));
        playerModel.DoSystemResult(new
        (
            playerSystemModel.Money - cost,
            playerSystemModel.Employees,
            playerSystemModel.Resistance,
            playerSystemModel.CommunityOpinionValue,
            playerSystemModel.Day
        ));
        PlantGroup.Instance.UpdateAllPlantUI(currentPlantModel);
    }

    public void Contract()
    {
        PlantModel currentPlantModel = PlantGroup.Instance.PlantModel;
        if (!currentPlantModel.IsContracts[ID])
        {
            // 계약 시작
            Debug.Log($"Contract cancellation requested for Plant {ID}");
            currentPlantModel.IsContracts[ID] = true;
            playerModel.DoPlantContractResult(new(currentPlantModel.ContractCosts, currentPlantModel.ContractProducts, currentPlantModel.IsContracts));
        }
        else
        {
            // 계약 취소 요청 기록
            currentPlantModel.PendingContractCancellations[ID] = true;
            Debug.Log($"Contract cancellation requested for Plant {ID}. It will take effect the next day.");
        }

        PlantGroup.Instance.UpdateAllPlantUI(currentPlantModel);
    }
}
