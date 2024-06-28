using Newtonsoft.Json;
using System;
public class TechDataParser : IDocumentParser<TechData>
{
    /// <summary>
    /// Ÿ Ŭ�������� ������ Parse�� ȣ��Ǹ� �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public TechData Parse(String data)
    {
        TechDataLine[] lines = null;
        lines = JsonConvert.DeserializeObject<TechDataLine[]>(data);
        return new TechData(lines);
    }
}