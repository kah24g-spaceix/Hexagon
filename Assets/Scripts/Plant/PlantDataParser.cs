using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
public class PlantDataParser : IDocumentParser<PlantData>
{
    public PlantData Parse(string data)
    {
        try
        {
            PlantDataLine[] lines = JsonConvert.DeserializeObject<PlantDataLine[]>(data);
            if (lines == null)
            {
                Debug.LogError("Deserialized lines are null.");
                return null;
            }
            return new PlantData(lines);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to parse data: {ex.Message}");
            return null;
        }
    }
}
