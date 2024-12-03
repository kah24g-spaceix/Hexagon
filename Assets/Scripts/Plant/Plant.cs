using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlantList;

public class Plant : MonoBehaviour
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
        plantImage = GetComponent<Image>();
        constructionButton = GetComponent<Button>();
        contractButton = GetComponent<Button>();
    }

    public void Bind(PlantModel model)
    {
        PlayerPlantModel playerPlantModel = playerModel.GetPlayerPlantModel();

        plantImage.sprite = Resources.Load<Sprite>($"Resources/Sprites/Plants/Plant_{ID}");
        nameText.SetText($"{model.Names[ID]}");
        levelText.SetText($"{model.Levels[ID]}");
        constructionCostText.SetText($"{model.ConstructionCosts}H$");
        contractCostText.SetText($"{model.ContractCosts}H$");

    }

    public void Construction()
    {
        Debug.Log("Construction");
        PlantModel currentPlantModel = plantList.PlantModel;
        PlayerPlantModel playerPlantData = playerModel.GetPlayerPlantModel();
        PlayerSystemModel playerSystemData = playerModel.GetPlayerSystemModel();
        
        if (playerSystemData.Money < currentPlantModel.ConstructionCosts[ID])
        {
            Debug.Log("No Money");
            return;
        }
        if (!currentPlantModel.IsContructions[ID])
        {
            currentPlantModel.IsContructions[ID] = true;
        }
        else 
        {
            currentPlantModel.Levels[ID]++;
        }
        plantList.UpdateAllPlantUI(currentPlantModel);
        Debug.Log("Construction Success");
    }
    public void Contract()
    {
        
        Debug.Log("Contract");
        PlantModel currentPlantModel = plantList.PlantModel;
        PlayerPlantContractModel playerPlantData = playerModel.GetPlayerPlantModel();
        PlayerSystemModel playerSystemData = playerModel.GetPlayerSystemModel();
        
        if (playerSystemData.Money < currentPlantModel.ConstructionCosts[ID])
        {
            Debug.Log("Contract Money");
            return;
        }
        if (!currentPlantModel.IsContracts[ID])
            currentPlantModel.IsContracts[ID] = true;
        else
            currentPlantModel.IsContracts[ID] = false;
        
        PlayerPlantContractModel newData = new PlayerPlantContractModel
        {
            
        };
        playerModel.DoPlantResult(newData);
        plantList.UpdateAllPlantUI(currentPlantModel);
        Debug.Log("Construction Success");

    }
}