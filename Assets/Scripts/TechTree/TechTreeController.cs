using System.Collections;
using System.Linq;
using UnityEngine;
using static TechTree;

public class TechTreeController : MonoBehaviour
{
    private IView<TechModel> view;
    private Coroutine routine;

    private void Awake()
    {
        view = GetComponent<IView<TechModel>>();
    }

    public void ShowTech(TechData data)
    {
        if (data == null)
        {
            Debug.LogError("TechData is null.");
            return;
        }

        if (data.TechDataLines == null)
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

        routine = StartCoroutine(TechDataRoutine(data.TechDataLines));
    }

    private IEnumerator TechDataRoutine(TechDataLine[] data, int startIndex = 0)
    {
        if (data == null)
        {
            Debug.LogError("TechDataLine array is null.");
            yield break;
        }

        if (techTree.TechList.Count < data.Length)
        {
            Debug.LogError($"TechList size is smaller than data length. / TechList size:{techTree.TechList.Count} / dataLength: {data.Length}");
            yield break;
        }

        int[] techLevels = new int[data.Length];
        int[] techCaps = data.Select(t => t.TechCap).ToArray();
        int[] techCosts = data.Select(t => t.TechCost).ToArray();
        int[] revenue = data.Select(t => t.Revenue).ToArray();
        int[] maxEmployee = data.Select(t => t.MaxEmployee).ToArray();
        string[] techNames = data.Select(t => t.TechName).ToArray();
        string[] techDescriptions = data.Select(t => t.TechDescription).ToArray();
        int[][] techOpens = data.Select(t => t.ConnectedTechs).ToArray();

        TechModel techModel = new TechModel(
            techLevels,
            techCaps,
            techCosts,
            revenue,
            maxEmployee,
            techNames,
            techDescriptions,
            techOpens
        );

        for (int i = startIndex; i < data.Length; i++)
        {
            TechDataLine line = data[i];

            Tech techComponent = techTree.TechList[i].GetComponent<Tech>();
            if (techComponent == null)
            {
                Debug.LogError($"Tech component not found on TechList element at index {i}");
                yield break;
            }

            techComponent.ID = i;
            techComponent.ConnectedTechs = line.ConnectedTechs;
            techComponent.Bind(techModel);
        }

        yield break;
    }

}
