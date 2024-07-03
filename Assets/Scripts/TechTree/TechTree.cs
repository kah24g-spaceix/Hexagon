using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechTree : MonoBehaviour
{

    public static TechTree techTree;
    private void Awake() => techTree = this;

    public GameObject TechHolder;
    public GameObject TechPrefab;
    public List<Tech> TechList;
    
    

    public TechModel m_techModel;
/*    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;*/
    public int TechPoint = 20;
    public void TechTreeStart()
    {
        /*        TechLevels = new int[6];
                TechCaps = new[] { 1, 5, 5, 2, 10, 10 };

                TechNames = new[] { "Upgrade 1", "Upgrade 2", "Upgrade 3", "Upgrade 4", "Upgrade 5", "Upgrade 6" };
                TechDescriptions = new[]
                {
                    "Does a thing",
                    "Does a cool thing",
                    "Does a really cool thing",
                    "Does an awsome thing",
                    "Does this math thing",
                    "Does this compounding thing"
                };*/
        foreach (Tech tech in TechHolder.GetComponentsInChildren<Tech>())
        {
            TechList.Add(tech);
        }
        /*foreach (RectTransform connector in ConnectorHolder.GetComponentsInChildren<RectTransform>())
            ConnectorList.Add(connector.gameObject);
*/
        for (int i=0; i<TechList.Count;i++)
            TechList[i].id = i;
        for (int i = 0; i < TechList.Count; i++)
        {
            TechList[i].ConnectedTechs = m_techModel.TechOpen;

        }
        UpdateAllTechUI(m_techModel);
    }
    public void UpdateAllTechUI(TechModel pModel)
    {
        foreach (Tech tech in TechList) tech.Bind(pModel);
    }
}
