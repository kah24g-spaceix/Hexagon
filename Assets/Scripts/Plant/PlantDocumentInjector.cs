using System;
using UnityEngine;

public class PlantDocumentInjector : MonoBehaviour
{
    [SerializeField] private TextAsset _plantDataTextAsset;

    private const string ParserName = "PlantDataParser";
    private PlantController controller;

    private void Awake()
    {
        controller = GetComponent<PlantController>();
    }

    private void Start()
    {
        if (PlantGroup.Instance == null)
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

        if (!typeof(IDocumentParser<PlantData>).IsAssignableFrom(parserType))
        {
            Debug.LogError($"Type {parserType} is not assignable to IDocumentParser<PlantData>.");
            return;
        }

        IDocumentParser<PlantData> parser = Activator.CreateInstance(parserType) as IDocumentParser<PlantData>;
        if (parser == null)
        {
            Debug.LogError("Failed to create an instance of the parser.");
            return;
        }

        BindParser(parser);
    }

    public void BindParser(IDocumentParser<PlantData> documentParser)
    {
        if (_plantDataTextAsset == null)
        {
            Debug.LogError("Text asset for plant data is not assigned.");
            return;
        }

        PlantData data = documentParser.Parse(_plantDataTextAsset.text);
        if (data?.PlantDataLines == null)
        {
            Debug.LogError("Parsed PlantData or PlantDataLines is null.");
            return;
        }

        PlantGroup.Instance.InitializePlantModel(data);
        controller.Show(data);
    }
}
