using System;


namespace Files
{
    public static class StreamReaderExtension
    {
        public static char ReadSpacesAndFirstChar(this StreamReader reader)
        {
            char c = ' ';
            while (!reader.EndOfStream && c == ' ')
            {
                c = (char)reader.Read();
            };
            return c;
        }

        public static IEnumerable<char> ReadWord(this StreamReader reader)
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
}
