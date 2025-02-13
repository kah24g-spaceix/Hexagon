using System;
using UnityEngine;

public class HyperFrameDocumentInjector : MonoBehaviour
{
    [SerializeField] private TextAsset _hyperframeDataTextAsset;

    private const string PaserName = "HyperFrameDataParser";

    private void Awake()
    {
        controller = GetComponent<HyperFrameController>();
    }
}