using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tech : MonoBehaviour, IView<TechModel>
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;

    private IGameModel gameModel;

    public int ID { get; set; }
    public int[] ConnectedTechs { get; set; }

    private Button button;

    private void Awake()
    {
        gameModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Buy);

        ConnectedTechs ??= new int[0];
    }

    public void Bind(TechModel model)
    {
        if (gameModel == null || ID < 0 || ID >= model.TechLevels.Length) return;

        PlayerTechModel playerTechData = gameModel.GetPlayerTechModel();

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
        if (ConnectedTechs == null || ConnectedTechs.Length == 0) return;

        for (int i = 0; i < ConnectedTechs.Length; i++)
        {
            int connectedId = ConnectedTechs[i];
            if (connectedId < 0 || connectedId >= TechTree.Instance.TechList.Count) continue;

            TechTree.Instance.TechList[connectedId].gameObject.SetActive(model.TechLevels[ID] > 0);
        }
    }

    public void Buy()
    {
        AudioManager.Instance.PlaySFX("Select");
        TechModel currentTechModel = TechTree.Instance.TechModel;
        PlayerTechModel playerTechModel = gameModel.GetPlayerTechModel();

        if (playerTechModel.TechPoint - currentTechModel.TechCosts[ID] < 0) return;
        if (currentTechModel.TechLevels[ID] >= currentTechModel.TechCaps[ID]) return;

        currentTechModel.TechLevels[ID]++;
        gameModel.DoTechResult(new PlayerTechModel(
            playerTechModel.TechPoint - currentTechModel.TechCosts[ID],
            playerTechModel.RevenueValue + currentTechModel.Revenue[ID],
            playerTechModel.MaxEmployee + currentTechModel.MaxEmployee[ID],
            currentTechModel.TechLevels
        ));

        TechTree.Instance.UpdateAllUI(currentTechModel);
    }
}
