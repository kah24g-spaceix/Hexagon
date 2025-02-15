using System;
using UnityEngine;

public class HyperFrameDocumentInjector : MonoBehaviour
{
    [SerializeField] private TextAsset _hyperframeDataTextAsset;

    private const string ParserName = "HyperFrameDataParser";
    private HyperFrameController controller;
    private void Awake()
    {
        controller = GetComponent<HyperFrameController>();
    }
    private void Start()
    {
        Type parserType = Type.GetType(ParserName);
        if (parserType == null)
        {
            Debug.LogError($"Parser type {ParserName} not found.");
            return;
        }
        if (!typeof(IDocumentParser<HyperFrameData>).IsAssignableFrom(parserType))
        {
            Debug.LogError($"Type {parserType} is not assignable to IDocumentParser<HyperFrameData>.");
            return;
        }
        
        IDocumentParser<HyperFrameData> parser = Activator.CreateInstance(parserType) as IDocumentParser<HyperFrameData>;
        if (parser == null)
        {
            Debug.LogError("Failed to create an instance of the parser.");
            return;
        }

        BindParser(parser);
    }
    public void BindParser(IDocumentParser<HyperFrameData> documentParser)
    {
        if (_hyperframeDataTextAsset == null)
        {
            Debug.LogError("Text asset for tech data is not assigned.");
            return;
        }

        HyperFrameData data = documentParser.Parse(_hyperframeDataTextAsset.text);
        if (data == null || data.HyperFrameDataLines == null)
        {
            Debug.LogError("Parsed data is invalid.");
            return;
        }

        HyperFrameGroup.Instance.InitializeModel(data);
        controller.Show(data);
    }
}