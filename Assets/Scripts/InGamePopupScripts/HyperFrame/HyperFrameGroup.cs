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
                _instance = Object.FindFirstObjectByType<HyperFrameGroup>();
                if (_instance == null)
                {
                    Debug.LogError("HyperFrameGroup instance is not found in the scene.");
                }
            }
            return _instance;
        }
    }
    [SerializeField] private GameObject hyperFrameValue;
    [SerializeField] private GameObject hyperFrameHolder;
    [SerializeField] private GameObject costHolder;
    public GameObject HyperFrameValue { get; private set; }
    public List<Frame> FrameList { get; private set; }
    public List<ProductValue> CostList { get; private set;}
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
        HyperFrameValue = hyperFrameValue;
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
            data.HyperFrameDataLines.Select(t => t.Description).ToArray(),
            data.HyperFrameDataLines.Select(t => t.Price).ToArray(),
            data.HyperFrameDataLines.Select(t => t.MaterialsCost).ToArray(),
            new int[data.HyperFrameDataLines.Length]
        );
        FrameList = new List<Frame>(hyperFrameHolder.GetComponentsInChildren<Frame>());
        CostList = new List<ProductValue>(costHolder.GetComponentsInChildren<ProductValue>());
        for (int i = 0; i < FrameList.Count; i++)
        {
            FrameList[i].ID = i;
            FrameList[i].Costs = Model.MaterialsCosts[i];
        }
    }
    public void UpdateAllHyperFrameUI(HyperFrameModel model)
    {
        foreach (var frame in FrameList)
        {
            frame.Bind(model);
        }
    }
}