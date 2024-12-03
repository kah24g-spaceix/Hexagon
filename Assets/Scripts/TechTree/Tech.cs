using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TechTree;

public class Tech : MonoBehaviour, IView<TechModel>
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text costText;

    private IGameModel playerModel;

    public int ID { get; set; }
    public int[] ConnectedTechs { get; set; }

    private void Awake()
    {
        playerModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
    }

    public void Bind(TechModel model)
    {
        PlayerTechModel playerTechData = playerModel.GetPlayerTechModel();
        if (ID < 0 || ID >= model.TechLevels.Length)
        {
            Debug.LogError($"Tech ID {ID} is out of range.");
            return;
        }

        levelText.SetText($"{model.TechLevels[ID]}/{model.TechCaps[ID]}");
        titleText.SetText($"{model.TechNames[ID]}");
        descriptionText.SetText($"{model.TechDescriptions[ID]}");
        costText.SetText($"Cost: {playerTechData.TechPoint}/{model.TechCosts[ID]} TP");

        Image sprite = GetComponent<Image>();
        if (model.TechLevels[ID] >= model.TechCaps[ID])
        {
            costText.gameObject.SetActive(false);
            sprite.color = Color.white;
        }
        else if (playerTechData.TechPoint >= model.TechCosts[ID])
        {
            sprite.color = Color.yellow;
        }
        else
        {
            sprite.color = Color.red;
        }

        UpdateConnectedTechs(model);
    }

    private void UpdateConnectedTechs(TechModel model)
    {
        foreach (int connectedTech in ConnectedTechs)
        {
            if (connectedTech > 0 && connectedTech < techTree.TechList.Count)
            {
                techTree.TechList[connectedTech].gameObject.SetActive(model.TechLevels[ID] > 0);
            }
            else
            {
                break;
                //Debug.LogError($"Tech with ID {connectedTech} is out of range in TechList. TechList count: {techTree.TechList.Count}");
            }
        }
    }
    public void Buy()
    {
        Debug.Log("Try Buy");
        TechModel currentTechModel = techTree.TechModel;
        PlayerTechModel playerTechData = playerModel.GetPlayerTechModel();
        Debug.Log($"{playerTechData.TechPoint}");
        if (playerTechData.TechPoint < currentTechModel.TechCosts[ID] 
        || currentTechModel.TechLevels[ID] >= currentTechModel.TechCaps[ID])
        {
            Debug.Log("No Money");
            return;
        }
        currentTechModel.TechLevels[ID]++;

        int techPoint = playerTechData.TechPoint - currentTechModel.TechCosts[ID];
        int revenueValue = playerTechData.RevenueValue + currentTechModel.Revenue[ID];
        int maxEmployees = playerTechData.MaxEmployee + currentTechModel.MaxEmployee[ID];
        int[] techLevels = currentTechModel.TechLevels;
        PlayerTechModel newData = new PlayerTechModel
            (
            techPoint,
            revenueValue,
            maxEmployees,
            techLevels
            );
        playerModel.DoTechResult(newData);
        techTree.UpdateAllTechUI(currentTechModel);
        Debug.Log("Buy Success");
    }
}
