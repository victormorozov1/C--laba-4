using System;
using System.Runtime.CompilerServices;

namespace Extensions;

public static class StreamReaderExtension
{
    public static char[] sepSymbols = new char[] { ' ', '\n' };
    public static char ReadSpacesAndFirstChar(this StreamReader reader, char[] sepSymbols2 = null)
    {
        if (sepSymbols2 == null)
        {
            sepSymbols2 = sepSymbols;
        }

        char c = sepSymbols2[0];

        while (!reader.EndOfStream && sepSymbols2.Contains(c))
        {
            c = (char)reader.Read();
        };
        return c;
    }

    public static IEnumerable<char> ReadStringAfterFirstChar(this StreamReader reader, char[] sepSymbols2 = null)
    {
        if (sepSymbols2 == null)
        {
            sepSymbols2 = sepSymbols;
        }

        char c = '-';
        while (!reader.EndOfStream)
        {
            c = (char)reader.Read();
            if (sepSymbols2.Contains(c))
            {
                yield break;
            }
            
            yield return c;
            
        }
        yield break;
    }

    public static string ReadString(this StreamReader reader, char[] sepSymbols = null)
    {
        string s = "" + ReadSpacesAndFirstChar(reader, sepSymbols2: sepSymbols);
        foreach (char c in ReadStringAfterFirstChar(reader, sepSymbols2: sepSymbols))
        {
            s += c;
        }
        return s;
    }
}
