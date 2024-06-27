using System;

public interface IDocumentParser<Data>
{
    Data Parse(String pData);
}