using System.IO;

namespace Functions;

public class Functions
{
    public static int FirstDigit(int number)
    {
        return int.Parse(Math.Abs(number).ToString()[0].ToString());
    }

    public static char ReadSpacesAndFirstChar(StreamReader reader)
    {
        char c = ' ';
        while (!reader.EndOfStream && c == ' ')
        {
            c = (char)reader.Read();
        };
        return c;
    }

    public static IEnumerable<char> ReadWord(StreamReader reader)
    {
        char c = '-';
        while (!reader.EndOfStream && c != ' ')
        {
            c = (char)reader.Read();
            if (c != ' ')
            {
                yield return c;
            }
        }
        yield break;
    }
}