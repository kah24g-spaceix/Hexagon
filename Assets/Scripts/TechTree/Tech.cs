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
    private CanvasGroup canvasGroup;

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
        canvasGroup = GetComponent<CanvasGroup>();
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(Buy);
            Debug.Log($"Button listener added to Tech with ID {id}");
        }
        else
        {
            Debug.LogError("Button component not found on Tech object");
        }
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
        costText.text = $"Cost: {TechTree.techTree.TechPoint}/{model.TechCosts[id]} TP";

        Image sprite = GetComponent<Image>();
        if (model.TechLevels[id] >= model.TechCaps[id])
        {
            costText.gameObject.SetActive(false);
            sprite.color = Color.white;
        }
        else if (TechTree.techTree.TechPoint >= model.TechCosts[id])
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
        foreach (var connectedTech in connectedTechs)
        {
            if (connectedTech >= 0 && connectedTech < TechTree.techTree.TechList.Count)
            {
                TechTree.techTree.TechList[connectedTech].gameObject.SetActive(model.TechLevels[id] > 0);
            }
            else
            {
                Debug.LogError($"Tech with ID {connectedTech} is out of range in TechList");
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
