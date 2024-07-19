using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TechTree;

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
        if (pData == null)
        {
            Debug.LogError("TechData is null.");
            return;
        }

        if (pData.TechDataLines == null)
        {
            Debug.LogError("TechDataLines is null.");
            return;
        }

        Debug.Log("Starting TechDataRoutine.");
        Debug.Log($"TechTree reference: {techTree}");

        if (techTree == null)
        {
            Debug.LogError("TechTree is not initialized.");
            return;
        }

        m_routine = StartCoroutine(TechDataRoutine(pData.TechDataLines));
    }

    private IEnumerator TechDataRoutine(TechDataLine[] pData, int pStartIndex = 0)
    {
        if (pData == null)
        {
            Debug.LogError("TechDataLine array is null.");
            yield break;
        }

        int nodeCount = pData.Length;
        float radius = 3f; // 중심으로부터의 거리
        Vector3 center = Vector3.zero; // 중심점

        int[] techLevels = new int[pData.Length];
        int[] techCaps = pData.Select(t => t.TechCap).ToArray();
        int[] techCosts = pData.Select(t => t.TechCost).ToArray();
        int[] communityOpinions = pData.Select(t => t.CommunityOpinion).ToArray();
        string[] techNames = pData.Select(t => t.TechName).ToArray();
        string[] techDescriptions = pData.Select(t => t.TechDescription).ToArray();
        int[][] techOpens = pData.Select(t => t.TechOpen).ToArray(); // 2차원 배열로 변환

        TechModel techModel = new TechModel(
            techLevels,
            techCaps,
            techCosts,
            communityOpinions,
            techNames,
            techDescriptions,
            techOpens
        );

        for (int i = pStartIndex; i < pData.Length; i++)
        {
            TechDataLine line = pData[i];
            Vector3 position;

            if (i == 0)
            {
                // 첫 번째 기술을 중심에 배치
                position = center;
            }
            else
            {
                // 나머지 기술들을 방사형으로 배치
                float angle = (i - 1) * Mathf.PI * 2f / (nodeCount - 1);
                position = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0) + center;
            }

            // Tech Prefab 생성 및 위치 설정
            GameObject techPrefabInstance = Instantiate(techTree.TechPrefab, position, Quaternion.identity, techTree.TechHolder.transform);

            if (techPrefabInstance == null)
            {
                Debug.LogError("Failed to instantiate TechPrefab.");
                yield break;
            }

            Tech techComponent = techPrefabInstance.GetComponent<Tech>();
            if (techComponent == null)
            {
                Debug.LogError("Tech component not found in the instantiated prefab.");
                yield break;
            }
        }
        yield break;
    }


}
