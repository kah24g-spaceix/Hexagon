using System.Collections;
using System.Linq;
using UnityEngine;

public class TechTreeController : MonoBehaviour
{
    private Coroutine routine;

    private void Awake()
    {
        if (TechTree.Instance == null)
        {
            Debug.LogError("TechTree instance is not found. Ensure TechTree is initialized in the scene.");
        }
    }

    public void ShowTech(TechData data)
    {
        if (data?.TechDataLines == null)
        {
            Debug.LogError("TechData or TechDataLines is null.");
            return;
        }

        if (TechTree.Instance == null)
        {
            Debug.LogError("TechTree is not initialized.");
            return;
        }

        Debug.Log("Starting TechDataRoutine.");
        routine = StartCoroutine(TechDataRoutine(data.TechDataLines));
    }

    private IEnumerator TechDataRoutine(TechDataLine[] data, int startIndex = 0)
    {
        if (data == null)
        {
            Debug.LogError("TechDataLine array is null.");
            yield break;
        }

        if (TechTree.Instance.TechList == null || TechTree.Instance.TechList.Count < data.Length)
        {
            Debug.LogError($"TechList size is smaller than data length. TechList size: {TechTree.Instance.TechList?.Count ?? 0}, Data length: {data.Length}");
            yield break;
        }
        for (int i = startIndex; i < data.Length; i++)
        {
            TechDataLine line = data[i];

            if (i >= TechTree.Instance.TechList.Count)
            {
                Debug.LogError($"Index {i} out of range for TechList.");
                yield break;
            }

            Tech techComponent = TechTree.Instance.TechList[i].GetComponent<Tech>();
            if (techComponent == null)
            {
                Debug.LogError($"Tech component not found on TechList element at index {i}");
                yield break;
            }

            techComponent.ID = i;
            techComponent.Bind(TechTree.Instance.TechModel);
        }

        yield break;
    }
}
