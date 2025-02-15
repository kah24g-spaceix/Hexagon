using Newtonsoft.Json;
using System;
using UnityEngine;
public class HyperFrameDataParser : IDocumentParser<HyperFrameData>
{
    public HyperFrameData Parse(string data)
    {
        try
        {
            HyperFrameDataLine[] lines = JsonConvert.DeserializeObject<HyperFrameDataLine[]>(data);
            if (lines == null)
            {
                Debug.LogError("Deserialized lines are null.");
                return null;
            }
            return new HyperFrameData(lines);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to parse data: {ex.Message}");
            return null;
        }
    }
}