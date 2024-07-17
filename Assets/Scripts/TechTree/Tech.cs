using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TechTree;

public class Tech : MonoBehaviour, IView<TechModel>
{
    public int id; // id 필드를 사용
    public TMP_Text LevelText;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public TMP_Text CostText;
    public int[] ConnectedTechs;
    private CanvasGroup m_canvasGroup;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Button>().onClick.AddListener(Buy);
        
    }

    public void Bind(TechModel pModel)
    {
        
        LevelText.text = $"{pModel.TechLevels[id]}/{pModel.TechCaps}";
        TitleText.text = $"{pModel.TechName}";
        DescriptionText.text = $"{pModel.TechDescription}";
        CostText.text = $"Cost: {techTree.TechPoint}/{pModel.TechCost} TP";

        Image sprite = GetComponent<Image>();
        if (pModel.TechLevels[id] >= pModel.TechCaps)
        {
            CostText.gameObject.SetActive(false);
            sprite.color = Color.white;
        }
        else if (techTree.TechPoint >= pModel.TechCost)
        {
            sprite.color = Color.yellow;
        }
        else
        {
            sprite.color = Color.red;
        }

        UpdateConnectedTechs(pModel);
        Debug.Log("갱신");
    }
    private void UpdateConnectedTechs(TechModel pModel)
    {
        foreach (int connectedTech in ConnectedTechs)
        {
            techTree.TechList[connectedTech].gameObject.SetActive(pModel.TechLevels[id] > 0);
            
        }
    }
    public void Buy()
    {
        TechModel currentTechModel = techTree.GetTechModel();

        if (techTree.TechPoint < currentTechModel.TechCost || currentTechModel.TechLevels[id] >= currentTechModel.TechCaps)
            return;

        techTree.TechPoint -= currentTechModel.TechCost;
        currentTechModel.TechLevels[id]++;
        techTree.UpdateAllTechUI(currentTechModel);
    }
}

