using Files;
using Functions;

class Program
{
    public static void FirstTask()
    {
        var file = new BinaryNumberFile("input.txt", new int[] {100, 1000}, new int[] {0, 100000});
        file.RandomFillFile();

        file.WriteFileTOConsole();
        Console.WriteLine();

        BinaryWriter fout = new BinaryWriter(File.Open("output.txt", FileMode.Create));
        foreach (var number in file.ReadNextElement())
        {
            int absNum = Math.Abs(number.ToInt());
            if (absNum % 10 == Functions.Functions.FirstDigit(absNum))
            {
                fout.Write(number.ToInt());
            }
        }
        fout.Close();
    }

    /*public static void FillMatrix(int[,] m, int n, int firstDigitToReplace, BinaryNumberFile inputFile)
    {
        var numbers = inputFile.ReadNextElement().GetEnumerator();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {

                if (numbers.MoveNext())
                {
                    var number = numbers.Current;

                    if (Functions.Functions.FirstDigit(number) == firstDigitToReplace)
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
    }*/
    
    /*public static void SecondTask(string filename = "input.txt",
        int n = 10, int firstDigitToReplace = 1, 
        bool printMatrix = true)
    {
        BinaryNumberFile inputFile = new BinaryNumberFile(filename);
        inputFile.FillNumbers();
        
        int[,] m = new int[n, n];
        FillMatrix(m, n, firstDigitToReplace, inputFile);

        if (printMatrix)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(m[i, j] + " ");
                }
                Console.WriteLine();
            }
        } 
    }*/
    
    public static void Main()
    {
        FirstTask();
    }
}