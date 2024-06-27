using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentInjector : MonoBehaviour
{
    [SerializeField] private TextAsset m_textAsset;
    [SerializeField] private String m_parserName;
    private TechTree m_techTree;

    private void Awake()
    {
        m_techTree = GetComponent<TechTree>();
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
        TechData data = pDocumentParser.Parse(m_textAsset.text);
        m_techTree.ShowDialog(data);
    }
}
