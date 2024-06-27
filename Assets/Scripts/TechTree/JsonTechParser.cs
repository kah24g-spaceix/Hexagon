using Newtonsoft.Json;
public class JsonTechParser : IDocumentParser<TechData>
{
    /// <summary>
    /// Ÿ Ŭ�������� ������ Parse�� ȣ��Ǹ� �� �Լ��� ȣ��ȴ�.
    /// </summary>
    public TechData Parse(string data)
    {
        TechDataLine[] lines = null;
        lines = JsonConvert.DeserializeObject<TechDataLine[]>(data);
        return new TechData(lines);
    }
}