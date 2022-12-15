using FileClasses;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace Main;

class Program
{
    public static void FirstTask()
    {
        var file = new BinaryNumberFile("input.txt", new int[] { 100, 1000 });
        file.RandomFillFile();

        file.WriteFileTOConsole();
        Console.WriteLine();

        BinaryWriter fout = new BinaryWriter(File.Open("output.txt", FileMode.Create));
        foreach (var number in file.ReadNextElement())
        {
            int absNum = Math.Abs(number.ToInt());
            if (absNum % 10 == Functions.FirstDigit(absNum))
            {
                fout.Write(number.ToInt());
            }
        }
        fout.Close();
    }

    public static void SecondTask(string filename = "input.txt",
        int n = 10, int firstDigitToReplace = 1,
        bool printMatrix = true)
    {
        BinaryNumberFile inputFile = new BinaryNumberFile(filename, new int[] { 10, 100 }, debug: false);
        inputFile.RandomFillFile();

        inputFile.WriteFileTOConsole();
        Console.WriteLine("\n");

        int[,] m = new int[n, n];
        Functions.FillMatrix(m, n, firstDigitToReplace, inputFile);

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
    }

    public static void ThirdTask()
    {
        var inputFile = new ToyFile("toys_input.txt", debug: false);
        inputFile.RandomFillFile();
        inputFile.WriteFileTOConsole(sep:"\n");

        int minAge = 999, maxAge = 0;
        int priceSum = 0, toyNum = 0;

        foreach (var toy in inputFile.ReadNextElement()) 
        { 
            if (toy.name == "кубики")
            {
                minAge = Math.Min(minAge, toy.minAge);
                maxAge = Math.Max(maxAge, toy.maxAge);
                priceSum += toy.price;
                toyNum++;
            }
        }

        if (toyNum > 0)
        {
            Console.WriteLine($"Кубики подходят для детей возрастом от {minAge} до {maxAge}");
            Console.WriteLine($"Сркдняя цена наборов {priceSum / toyNum}");
        }
        else
        {
            Console.WriteLine("Кубики не найдены(");
        }
    }

    public static void FourhtTask(int k)
    {
        var inputFile = new TextNumberFile("4-task-in.txt", "\n");
        inputFile.RandomFillFile();

        var outputFile = new StreamWriter("4-task-out.txt");

        foreach (var number in inputFile.ReadNextElement())
        {
            Console.WriteLine("writing: " + number.ToInt() / k);
            outputFile.Write(number.ToInt() / k + "\n");
        }

        outputFile.Close();
    }

    public static void FifthTask()
    {
        var inputFile = new TextNumberFile("5-task-in.txt", " ");
        inputFile.RandomFillFile();

        var numbers = inputFile.ReadNextElement().GetEnumerator();

        if (!numbers.MoveNext())
        {
            Console.WriteLine("Файл пуст");
            return;
        }

        var firstNum = numbers.Current.ToInt();
        int max = firstNum;
        while (numbers.MoveNext())
        {
            max = Math.Max(max, numbers.Current.ToInt());
        }

        Console.WriteLine("Сумма первого и максимального элементов: " + (firstNum + max));
    }

    public static void SixthTask()
    {
        var inputFile = new TextFile(Functions.GetFileName(6, Functions.FileTypes.input), numbersNumRange: new int[] {1000, 2000});
        inputFile.RandomFillFile();

        var outputFile = new StreamWriter(Functions.GetFileName(6, Functions.FileTypes.output));
        
        foreach (var line in inputFile.ReadNextElement())
        {
            if (line.str.Length >= 1 && line.str[0] == 'б' || line.str.Length >= 2 && line.str[1] == 'б')
            {
                outputFile.WriteLine(line);
            }
            Console.WriteLine(line);
        }

        outputFile.Close();
    }

    public static void Main()
    {
        SixthTask();
    }
}