using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentInjector : MonoBehaviour
{
    [SerializeField] private TextAsset m_techDataTextAsset;
    [SerializeField] private TextAsset m_techInformationTextAsset;
    [SerializeField] private String m_parserName;
    private TechTreeController m_controller;

    private void Awake()
    {
        m_controller = GetComponent<TechTreeController>();
    }
    private void Start()
    {
        Type parserType = Type.GetType(m_parserName);

        if (typeof(IDocumentParser<TechData>).IsAssignableFrom(parserType))
        {
            var parser = (IDocumentParser<TechData>)Activator.CreateInstance(parserType);

            BindParser(parser);
        }
        else
        {
            Debug.LogError($"could not find parser : found [{parserType}] from [{m_parserName}], but not assignable");
        }
    }
    public void BindParser(IDocumentParser<TechData> pDocumentParser)
    {
        TechData data;
        data = pDocumentParser.Parse(m_techDataTextAsset.text);
        data = pDocumentParser.Parse(m_techInformationTextAsset.text);
        m_controller.ShowTech(data);

    }
}
