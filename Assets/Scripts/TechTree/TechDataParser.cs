using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
public class TechDataParser : IDocumentParser<TechData>
{
    public TechData Parse(string data)
    {
        try
        {
            TechDataLine[] lines = JsonConvert.DeserializeObject<TechDataLine[]>(data);
            if (lines == null)
            {
                Debug.LogError("Deserialized lines are null.");
                return null;
            }
            return new TechData(lines);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to parse data: {ex.Message}");
            return null;
        }
    }
}
