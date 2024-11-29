using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] private Image plantImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text constructionCostText;
    [SerializeField] private TMP_Text contractCostText;
    [SerializeField] private Button contructionButton;
    [SerializeField] private Button contractButton;

    private IGameModel playerModel;

    public int ID { get; set; }

    private void Awake()
    {
        playerModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        plantImage = GetComponent<Image>();
        contructionButton = GetComponent<Button>();
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
}