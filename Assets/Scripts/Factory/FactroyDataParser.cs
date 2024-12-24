using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
public class FactoryDataParser : IDocumentParser<FactoryData>
{
    public FactoryData Parse(string data)
    {
        try
        {
            FactoryDataLine[] lines = JsonConvert.DeserializeObject<FactoryDataLine[]>(data);
            if (lines == null)
            {
                Debug.LogError("Deserialized lines are null.");
                return null;
            }
            return new FactoryData(lines);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to parse data: {ex.Message}");
            return null;
        }
    }
}
