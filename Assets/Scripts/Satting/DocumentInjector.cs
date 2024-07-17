using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentInjector : MonoBehaviour
{
    [SerializeField] private TextAsset m_techDataTextAsset;
    [SerializeField] private string m_parserName;
    private TechTreeController m_controller;

    private void Awake()
    {
        m_controller = GetComponent<TechTreeController>();
    }

    private void Start()
    {
        Type parserType = Type.GetType(m_parserName);
        if (parserType == null)
        {
            Debug.LogError($"Parser type {m_parserName} not found.");
            return;
        }

        if (typeof(IDocumentParser<TechData>).IsAssignableFrom(parserType))
        {
            var parser = (IDocumentParser<TechData>)Activator.CreateInstance(parserType);
            if (parser == null)
            {
                Debug.LogError("Failed to create an instance of the parser.");
                return;
            }
            BindParser(parser);
        }
        else
        {
            Debug.LogError($"Type {parserType} is not assignable to IDocumentParser<TechData>.");
        }
    }

    public void BindParser(IDocumentParser<TechData> pDocumentParser)
    {
        if (m_techDataTextAsset == null)
        {
            Debug.LogError("Text asset for tech data is not assigned.");
            return;
        }

        TechData data = pDocumentParser.Parse(m_techDataTextAsset.text);
        if (data == null)
        {
            Debug.LogError("Parsed data is null.");
            return;
        }

        if (data.TechDataLines == null)
        {
            Debug.LogError("Parsed TechDataLines is null.");
            return;
        }

        Debug.Log("Parsed data successfully.");
        TechTree.techTree.InitializeTechModel(data);
        m_controller.ShowTech(data);
    }
}
