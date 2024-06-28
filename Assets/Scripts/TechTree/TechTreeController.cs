using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IView<TechModel>))]
public class TechTreeController : MonoBehaviour
{
    private IView<TechModel> m_view;
    private Coroutine m_routine;

    private void Awake()
    {
        m_view = GetComponent<IView<TechModel>>();
    }
    public void ShowTech(TechData pData)
    {
        m_routine = StartCoroutine(TechDataRoutine(pData.TechDataLines));
    }
    private IEnumerator TechDataRoutine(TechDataLine[] pData, int pStartIndex = 0)
    {
        for (int i = pStartIndex; i < pData.Length; i++)
        {
            TechDataLine line = pData[i];

            List<int> levels = new List<int>();
            for (int j = pStartIndex; j < pData.Length; j++)
            {
                levels.Add(0);
            }


            m_view.Bind(new TechModel(
                levels.ToArray(), 
                line.TechCap, 
                line.TechCost, 
                line.CommunityOpinion,
                line.TechList,
                line.TechName, 
                line.TechDescription
                ));
            
        }
        yield break;
    }
}
