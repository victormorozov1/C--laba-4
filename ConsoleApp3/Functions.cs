using System.IO;

namespace Functions;

public class Functions
{
    public static int FirstDigit(int number)
    {
        return int.Parse(Math.Abs(number).ToString()[0].ToString());
    }
}