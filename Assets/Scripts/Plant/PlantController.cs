using System.Collections;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    private Coroutine routine;

    public void Show(PlantData data)
    {
        if (data?.PlantDataLines == null)
        {
            Debug.LogError("PlantData or PlantDataLines is null.");
            return;
        }

        if (PlantGroup.Instance == null)
        {
            Debug.LogError("PlantGroup instance is not initialized.");
            return;
        }

        routine = StartCoroutine(PlantDataRoutine(data.PlantDataLines));
    }

    private IEnumerator PlantDataRoutine(PlantDataLine[] data, int startIndex = 0)
    {
        if (data == null)
        {
            Debug.LogError("PlantDataLine array is null.");
            yield break;
        }

        if (PlantGroup.Instance.PlantList == null)
        {
            Debug.LogError("PlantList is not initialized in PlantGroup.");
            yield break;
        }

        for (int i = startIndex; i < data.Length; i++)
        {
            if (i >= PlantGroup.Instance.PlantList.Count)
            {
                Debug.LogError($"Index {i} is out of range for PlantList.");
                yield break;
            }

            Plant plantComponent = PlantGroup.Instance.PlantList[i];
            if (plantComponent == null)
            {
                Debug.LogWarning($"Plant component at index {i} is null.");
                continue;
            }

            plantComponent.ID = i;
            plantComponent.Bind(PlantGroup.Instance.PlantModel);
        }
    }
}
