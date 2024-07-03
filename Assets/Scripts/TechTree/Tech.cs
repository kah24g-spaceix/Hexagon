using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TechTree;

public class Tech : MonoBehaviour, IView<TechModel>
{
    public int id;
    private CanvasGroup m_canvasGroup;
    public TMP_Text LevelText;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public TMP_Text CostText;

    public int[] ConnectedTechs;
    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Button>().onClick.AddListener(Buy);
    }
    public void Bind(TechModel pModel)
    {
        LevelText.text
    = $"{pModel.TechLevels[id]}/{pModel.TechCaps}";
        TitleText.text
            = $"{pModel.TechName}";
        DescriptionText.text
            = $"{pModel.TechDescription}";
        CostText.text
            = $"Cost: {techTree.TechPoint}/{pModel.TechCost} TP";

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

        foreach (int connectedTech in ConnectedTechs)
        {
            techTree.TechList[connectedTech].gameObject.SetActive(pModel.TechLevels[id] > 0);
        }
    }
    public void Buy()
    {
        if (techTree.TechPoint < techTree.m_techModel.TechCost || techTree.m_techModel.TechLevels[id] >= techTree.m_techModel.TechCaps)
            return;
            techTree.TechPoint -= techTree.m_techModel.TechCost;
        techTree.m_techModel.TechLevels[id]++;
        techTree.UpdateAllTechUI(techTree.m_techModel);
    }
}
