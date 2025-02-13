using System;
using UnityEngine;

public class FactoryDocumentInjector : MonoBehaviour
{
    [SerializeField] private TextAsset _factoryDataTextAsset;

    private const string ParserName = "FactoryDataParser";
    private FactoryController controller;

    private void Awake()
    {
        controller = GetComponent<FactoryController>();
    }

    private void Start()
    {
        if (FactoryGroup.Instance == null)
        {
            Debug.LogError("PlantGroup instance is not initialized.");
            return;
        }



        Type parserType = Type.GetType(ParserName);
        if (parserType == null)
        {
            Debug.LogError($"Parser type {ParserName} not found.");
            return;
        }

        if (!typeof(IDocumentParser<FactoryData>).IsAssignableFrom(parserType))
        {
            Debug.LogError($"Type {parserType} is not assignable to IDocumentParser<PlantData>.");
            return;
        }

        IDocumentParser<FactoryData> parser = Activator.CreateInstance(parserType) as IDocumentParser<FactoryData>;
        if (parser == null)
        {
            Debug.LogError("Failed to create an instance of the parser.");
            return;
        }

        BindParser(parser);
    }

    public void BindParser(IDocumentParser<FactoryData> documentParser)
    {
        if (_factoryDataTextAsset == null)
        {
            Debug.LogError("Text asset for plant data is not assigned.");
            return;
        }

        FactoryData data = documentParser.Parse(_factoryDataTextAsset.text);
        if (data?.FactoryDataLines == null)
        {
            Debug.LogError("Parsed PlantData or PlantDataLines is null.");
            return;
        }

        FactoryGroup.Instance.InitializePlantModel(data);
        controller.Show(data);
    }
}
