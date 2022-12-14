using FileClasses;
using System.IO;

namespace Main;
public static class Functions
{
    public static int FirstDigit(int number)
    {
        return int.Parse(Math.Abs(number).ToString()[0].ToString());
    }
    public static void FillMatrix(int[,] m, int n, int firstDigitToReplace, BinaryNumberFile inputFile)
    {
        var numbers = inputFile.ReadNextElement().GetEnumerator();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (numbers.MoveNext())
                {
                    int number = numbers.Current.ToInt();

                    if (FirstDigit(number) == firstDigitToReplace)
                    {
                        number = firstDigitToReplace;
                    }

                    m[i, j] = number;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
