using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TechTree;

public class Tech : MonoBehaviour, IView<TechModel>
{
    public int id;
    public TMP_Text LevelText;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public TMP_Text CostText;
    public int[] ConnectedTechs;
    private CanvasGroup m_canvasGroup;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
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

    public void Bind(TechModel pModel)
    {
        if (id < 0 || id >= pModel.TechLevels.Length)
        {
            Debug.LogError($"Tech ID {id} is out of range.");
            return;
        }

        LevelText.text = $"{pModel.TechLevels[id]}/{pModel.TechCaps[id]}";
        TitleText.text = $"{pModel.TechNames[id]}";
        DescriptionText.text = $"{pModel.TechDescriptions[id]}";
        CostText.text = $"Cost: {techTree.TechPoint}/{pModel.TechCosts[id]} TP";

        Image sprite = GetComponent<Image>();
        if (pModel.TechLevels[id] >= pModel.TechCaps[id])
        {
            CostText.gameObject.SetActive(false);
            sprite.color = Color.white;
        }
        else if (techTree.TechPoint >= pModel.TechCosts[id])
        {
            sprite.color = Color.yellow;
        }
        else
        {
            sprite.color = Color.red;
        }
        UpdateConnectedTechs(pModel);
    }
    private void UpdateConnectedTechs(TechModel pModel)
    {
        foreach (int connectedTech in ConnectedTechs)
        {
            if (connectedTech >= 0 && connectedTech < techTree.TechList.Count)
            {
                techTree.TechList[connectedTech].gameObject.SetActive(pModel.TechLevels[id] > 0);
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
        TechModel currentTechModel = techTree.GetTechModel();

        if (techTree.TechPoint < currentTechModel.TechCosts[id] || currentTechModel.TechLevels[id] >= currentTechModel.TechCaps[id])
        {
            Debug.Log("구매 불가");
            return;
        }

        techTree.TechPoint -= currentTechModel.TechCosts[id];
        currentTechModel.TechLevels[id]++;
        techTree.UpdateAllTechUI(currentTechModel);
        Debug.Log("구매 성공");
    }
}
