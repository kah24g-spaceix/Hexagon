using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TechTree;
public class Tech : MonoBehaviour
{
    public int id;

    public TMP_Text LevelText;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public TMP_Text CostText;

    public int[] ConnectedTechs;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Buy);
    }
    public void UpdateUI()
    {
        LevelText.text
            = $"{techTree.TechLevels[id]}/{techTree.TechCaps[id]}";
        TitleText.text
            = $"{techTree.TechNames[id]}";
        DescriptionText.text
            = $"{techTree.TechDescriptions[id]}";
        CostText.text
            = $"Cost: {techTree.TechPoint}/1 TP";

        Image sprite = GetComponent<Image>();
        if (techTree.TechLevels[id] >= techTree.TechCaps[id])
        {
            CostText.gameObject.SetActive(false);
            sprite.color = Color.white;
        }
        else if (techTree.TechPoint >= 1)
        {
            sprite.color = Color.yellow;
        }
        else
        {
            sprite.color = Color.red;
        }

        foreach (int connectedTech in ConnectedTechs)
        {
            techTree.TechList[connectedTech]
                .gameObject.SetActive(techTree.TechLevels[id] > 0);
        }
    }
    public void Buy()
    {
        if (techTree.TechPoint < 1 ||
            techTree.TechLevels[id] >= techTree.TechCaps[id])
            return;
        techTree.TechPoint -= 1;
        techTree.TechLevels[id]++;
        techTree.UpdateAllTechUI();
    }
}
