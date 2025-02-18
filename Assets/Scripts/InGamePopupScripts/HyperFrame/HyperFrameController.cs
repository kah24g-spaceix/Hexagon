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

        routine = StartCoroutine(DataRoutine(data.HyperFrameDataLines));
    }

    private IEnumerator DataRoutine(HyperFrameDataLine[] data, int startIndex = 0)
    {
        if (data == null)
        {
            Debug.LogError("HyperFrameDataLine array is null.");
            yield break;
        }

        if (HyperFrameGroup.Instance.FrameList == null)
        {
            Debug.LogError("HyperFrameList is not initialized in HyperFrameGroup.");
            yield break;
        }

        for (int i = startIndex; i < data.Length; i++)
        {
            if (i >= HyperFrameGroup.Instance.FrameList.Count)
            {
                Debug.LogError($"Index {i} is out of range for HyperFrameList.");
                yield break;
            }

            Frame frameComponent = HyperFrameGroup.Instance.FrameList[i];
            if (frameComponent == null)
            {
                Debug.LogWarning($"HyperFrame component at index {i} is null.");
                continue;
            }

            frameComponent.ID = i;
            frameComponent.Bind(HyperFrameGroup.Instance.Model);
        }
    }
}