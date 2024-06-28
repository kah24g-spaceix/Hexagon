using Newtonsoft.Json;
using System;
public class TechDataParser : IDocumentParser<TechData>
{
    /// <summary>
    /// 타 클래스에서 데이터 Parse가 호출되면 이 함수가 호출된다.
    /// </summary>
    public TechData Parse(String data)
    {
        TechDataLine[] lines = null;
        lines = JsonConvert.DeserializeObject<TechDataLine[]>(data);
        return new TechData(lines);
    }
}