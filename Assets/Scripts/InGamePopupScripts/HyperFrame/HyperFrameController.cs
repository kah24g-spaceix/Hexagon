using System.Collections;
using UnityEngine;

public class HyperFrameController : MonoBehaviour
{
    private Coroutine routine;

    public void Show(HyperFrameData data)
    {
        if (data?.HyperFrameDataLines == null)
        {
            Debug.LogError("HyperFrameData or HyperFrameDataLines is null.");
            return;
        }

        if (HyperFrameGroup.Instance == null)
        {
            Debug.LogError("HyperFrameGroup instance is not initialized.");
            return;
        }

        routine = StartCoroutine(PlantDataRoutine(data.HyperFrameDataLines));
    }

    private IEnumerator PlantDataRoutine(HyperFrameDataLine[] data, int startIndex = 0)
    {
        if (data == null)
        {
            Debug.LogError("HyperFrameDataLine array is null.");
            yield break;
        }

        if (HyperFrameGroup.Instance.HyperFrameList == null)
        {
            Debug.LogError("HyperFrameList is not initialized in HyperFrameGroup.");
            yield break;
        }

        for (int i = startIndex; i < data.Length; i++)
        {
            if (i >= HyperFrameGroup.Instance.HyperFrameList.Count)
            {
                Debug.LogError($"Index {i} is out of range for HyperFrameList.");
                yield break;
            }

            HyperFrame hyperFrameComponent = HyperFrameGroup.Instance.HyperFrameList[i];
            if (hyperFrameComponent == null)
            {
                Debug.LogWarning($"HyperFrame component at index {i} is null.");
                continue;
            }

            hyperFrameComponent.ID = i;
            hyperFrameComponent.Bind(HyperFrameGroup.Instance.Model);
        }
    }
}