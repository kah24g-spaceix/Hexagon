using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTree : MonoBehaviour
{

    public static TechTree techTree;
    private void Awake() => techTree = this;

    public List<int> TechLevels;
    public int[] TechCaps;
    public string[] TechNames;
    public string[] TechDescriptions;
    public int[] TechCost;

    public GameObject TechPrefab;
    public List<Tech> TechList;
    public GameObject TechHolder;

/*    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;*/
    public int TechPoint = 20;
    private void Start()
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
            //Instantiate(TechPrefab);
            TechList.Add(tech);
        }
        /*foreach (RectTransform connector in ConnectorHolder.GetComponentsInChildren<RectTransform>())
            ConnectorList.Add(connector.gameObject);
*/
        for (int i=0; i<TechList.Count;i++)
            TechList[i].id = i;

        TechList[0].ConnectedTechs = new[] { 1, 2, 3 };
        TechList[2].ConnectedTechs = new[] { 4, 5 };


        UpdateAllTechUI();
    }

    public void UpdateAllTechUI()
    {
        foreach (Tech tech in TechList) tech.UpdateUI();
    }
}
