using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TechTree;

public class Tech : MonoBehaviour, IView<TechModel>
{
    [SerializeField] private int id;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private int[] connectedTechs;

    public int Id
    {
        get => id;
        set => id = value;
    }

    public int[] ConnectedTechs
    {
        get => connectedTechs;
        set => connectedTechs = value;
    }

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
    }

    public void Bind(TechModel model)
    {
        if (id < 0 || id >= model.TechLevels.Length)
        {
            Debug.LogError($"Tech ID {id} is out of range.");
            return;
        }

        levelText.text = $"{model.TechLevels[id]}/{model.TechCaps[id]}";
        titleText.text = $"{model.TechNames[id]}";
        descriptionText.text = $"{model.TechDescriptions[id]}";
        costText.text = $"Cost: {techTree.TechPoint}/{model.TechCosts[id]} TP";

        Image sprite = GetComponent<Image>();
        if (model.TechLevels[id] >= model.TechCaps[id])
        {
            costText.gameObject.SetActive(false);
            sprite.color = Color.white;
        }
        else if (techTree.TechPoint >= model.TechCosts[id])
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
        foreach (int connectedTech in connectedTechs)
        {
            if (connectedTech > 0 && connectedTech < techTree.TechList.Count)
            {
                techTree.TechList[connectedTech].gameObject.SetActive(model.TechLevels[id] > 0);
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

        if (techTree.TechPoint < currentTechModel.TechCosts[id] || currentTechModel.TechLevels[id] >= currentTechModel.TechCaps[id])
        {
            Debug.Log("구매 불가");
            return;
        }

        techTree.techPoint -= currentTechModel.TechCosts[id];
        currentTechModel.TechLevels[id]++;
        techTree.UpdateAllTechUI(currentTechModel);
        Debug.Log("구매 성공");
    }
}
