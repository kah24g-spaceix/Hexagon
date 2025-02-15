using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HyperFrameGroup : MonoBehaviour
{
    private static HyperFrameGroup _instance;
    public static HyperFrameGroup Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HyperFrameGroup>();
                if (_instance == null)
                {
                    Debug.LogError("HyperFrameGroup instance is not found in the scene.");
                }
            }
            return _instance;
        }
    }
    [SerializeField] private GameObject hyperFrameHolder;
    public List<HyperFrame> HyperFrameList { get; private set; }
    public HyperFrameModel Model { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void InitializeModel(HyperFrameData data)
    {
        if (data == null)
        {
            Debug.LogError("HyperFrameData is not assigned");
            return;
        }
        if (hyperFrameHolder == null)
        {
            Debug.LogError("Hyper Frame Holder is not assign in HyperFrameGroup");
            return;
        }
        Model = new HyperFrameModel(
            data.HyperFrameDataLines.Select(t => t.Name).ToArray(),
            data.HyperFrameDataLines.Select(t => t.Price).ToArray(),
            data.HyperFrameDataLines.Select(t => t.MaterialsCost).ToArray(),
            new int[data.HyperFrameDataLines.Length]
        );
        HyperFrameList = new List<HyperFrame>(hyperFrameHolder.GetComponentsInChildren<HyperFrame>());
        for (int i = 0; i < HyperFrameList.Count; i++)
        {
            HyperFrameList[i].ID = i;
        }
    }
    public void UpdateAllHyperFrameUI(HyperFrameModel model)
    {
        foreach (var hyperFrame in HyperFrameList)
        {
            hyperFrame.Bind(model);
        }
    }
}