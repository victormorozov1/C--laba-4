using System;


namespace Extensions;

public static class StreamReaderExtension
{
    public static bool IsSepSymbol(char c)
    {
        return new char[] { ' ', '\n' }.Contains(c);
    }
    public static char ReadSpacesAndFirstChar(this StreamReader reader)
    {
        char c = ' ';
        while (!reader.EndOfStream && !IsSepSymbol(c))
        {
            c = (char)reader.Read();
        };
        return c;
    }

    public static IEnumerable<char> ReadStringAfterFirstChar(this StreamReader reader)
    {
        char c = '-';
        while (!reader.EndOfStream && !IsSepSymbol(c))
        {
            c = (char)reader.Read();
            if (c != ' ')
            {
                yield return c;
            }
        }
        yield break;
    }

    public static string ReadString(this StreamReader reader)
    {
        string s = "" + ReadSpacesAndFirstChar(reader);
        foreach (char c in ReadStringAfterFirstChar(reader))
        {
            s += c;
        }
        return s;
    }
}
