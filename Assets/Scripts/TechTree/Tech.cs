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

        levelText.text = $"{model.TechLevels[ID]}/{model.TechCaps[ID]}";
        titleText.text = $"{model.TechNames[ID]}";
        descriptionText.text = $"{model.TechDescriptions[ID]}";
        costText.text = $"Cost: {playerTechData.TechPoint}/{model.TechCosts[ID]} TP";

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
        Debug.Log("누름");
        TechModel currentTechModel = techTree.TechModel;
        PlayerTechModel playerTechData = playerModel.GetPlayerTechModel();
        Debug.Log($"{playerTechData.TechPoint}");
        if (playerTechData.TechPoint < currentTechModel.TechCosts[ID] || currentTechModel.TechLevels[ID] >= currentTechModel.TechCaps[ID])
        {
            Debug.Log("구매 불가");
            return;
        }
        currentTechModel.TechLevels[ID]++;

        int techPoint = playerTechData.TechPoint - currentTechModel.TechCosts[ID];
        int revenueValue = playerTechData.RevenueValue + currentTechModel.Revenue[ID];
        int maxEmployees = playerTechData.MaxEmployee + currentTechModel.MaxEmployee[ID];
        int[] techLevels = currentTechModel.TechLevels;
        PlayerTechModel value = new PlayerTechModel
            (
            techPoint,
            revenueValue,
            maxEmployees,
            techLevels
            );
        playerModel.DoTechResult(value);
        techTree.UpdateAllTechUI(currentTechModel);
        Debug.Log("구매 성공");
    }
}
