using System;
using UnityEngine;

public class TechTreeDocumentInjector : MonoBehaviour
{
    [SerializeField] private TextAsset m_techDataTextAsset;

    private const string ParserName = "TechDataParser";
    private TechTreeController controller;

    private void Awake()
    {
        controller = GetComponent<TechTreeController>();
    }

    private void Start()
    {
        Type parserType = Type.GetType(ParserName);
        if (parserType == null)
        {
            Debug.LogError($"Parser type {ParserName} not found.");
            return;
        }

        if (!typeof(IDocumentParser<TechData>).IsAssignableFrom(parserType))
        {
            Debug.LogError($"Type {parserType} is not assignable to IDocumentParser<TechData>.");
            return;
        }

        IDocumentParser<TechData> parser = Activator.CreateInstance(parserType) as IDocumentParser<TechData>;
        if (parser == null)
        {
            Debug.LogError("Failed to create an instance of the parser.");
            return;
        }

        BindParser(parser);
    }

    public void BindParser(IDocumentParser<TechData> documentParser)
    {
        if (m_techDataTextAsset == null)
        {
            Debug.LogError("Text asset for tech data is not assigned.");
            return;
        }

        TechData data = documentParser.Parse(m_techDataTextAsset.text);
        if (data == null || data.TechDataLines == null)
        {
            Debug.LogError("Parsed data is invalid.");
            return;
        }

        TechTree.Instance.InitializeTechModel(data);
        controller.ShowTech(data);
    }
}
