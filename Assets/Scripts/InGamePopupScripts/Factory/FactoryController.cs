using System.Collections;
using UnityEngine;

public class FactoryController : MonoBehaviour
{
    private Coroutine routine;

    public void Show(FactoryData data)
    {
        if (data?.FactoryDataLines == null)
        {
            Debug.LogError("PlantData or PlantDataLines is null.");
            return;
        }

        if (FactoryGroup.Instance == null)
        {
            Debug.LogError("PlantGroup instance is not initialized.");
            return;
        }

        routine = StartCoroutine(PlantDataRoutine(data.FactoryDataLines));
    }

    private IEnumerator PlantDataRoutine(FactoryDataLine[] data, int startIndex = 0)
    {
        if (data == null)
        {
            Debug.LogError("PlantDataLine array is null.");
            yield break;
        }

        if (FactoryGroup.Instance.FactoryList == null)
        {
            Debug.LogError("PlantList is not initialized in PlantGroup.");
            yield break;
        }

        for (int i = startIndex; i < data.Length; i++)
        {
            if (i >= FactoryGroup.Instance.FactoryList.Count)
            {
                Debug.LogError($"Index {i} is out of range for PlantList.");
                yield break;
            }

            Factory factoryComponent = FactoryGroup.Instance.FactoryList[i];
            if (factoryComponent == null)
            {
                Debug.LogWarning($"Plant component at index {i} is null.");
                continue;
            }

            factoryComponent.ID = i;
            factoryComponent.Bind(FactoryGroup.Instance.Model);
        }
    }
}
