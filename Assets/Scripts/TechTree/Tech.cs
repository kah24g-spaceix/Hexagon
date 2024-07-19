using System;
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
    public int[] ConnectedTechs; // int 배열로 변경
    private CanvasGroup m_canvasGroup;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Button>().onClick.AddListener(Buy);
    }

    public void Bind(TechModel pModel)
    {
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
        foreach (int connectedTechs in ConnectedTechs)
            techTree.TechList[connectedTechs].gameObject.SetActive(pModel.TechLevels[id] > 0);
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
