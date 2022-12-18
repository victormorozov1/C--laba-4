using FileClasses;
using System;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace Main;

class Program
{
    public static void FirstTask()
    {
        var file = new BinaryNumberFile(Functions.GetFileName(1, Functions.FileTypes.input), 
            new int[] { 10, 20 }, debug: false);
        file.RandomFillFile();

        Console.WriteLine("Input File:");
        file.WriteFileTOConsole();

        BinaryWriter fout = new BinaryWriter(File.Open(Functions.GetFileName(1, Functions.FileTypes.output), FileMode.Create));
        foreach (var number in file.ReadNextElement())
        {
            int absNum = Math.Abs(number.ToInt());
            if (absNum % 10 == Functions.FirstDigit(absNum))
            {
                fout.Write(number.ToInt());
            }
        }
        fout.Close();

        Console.WriteLine("\n\nOutput File");

        BinaryReader fin = new BinaryReader(
            File.Open(Functions.GetFileName(1, Functions.FileTypes.output), 
            FileMode.Open));
        while (fin.BaseStream.Position < fin.BaseStream.Length)
        {
            Console.WriteLine(fin.ReadInt32());
        }
    }

    public static void SecondTask(int n = 10, int firstDigitToReplace = 1, bool printMatrix = true)
    {
        BinaryNumberFile inputFile = new BinaryNumberFile(Functions.GetFileName(2, Functions.FileTypes.input), 
            new int[] { 10, 100 }, debug: false);
        inputFile.RandomFillFile();

        Console.WriteLine("Input File:");
        inputFile.WriteFileTOConsole();
        Console.WriteLine("\n");

        int[,] m = new int[n, n];
        Functions.FillMatrix(m, n, firstDigitToReplace, inputFile);

        if (printMatrix)
        {
            Console.WriteLine("Output Matrix:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(m[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }

    public static void ThirdTask()
    {
        var inputFile = new ToyFile(Functions.GetFileName(3, Functions.FileTypes.input), debug: false);
        inputFile.RandomFillFile();

        Console.WriteLine("Input File:");
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
        var inputFile = new TextNumberFile(Functions.GetFileName(4, Functions.FileTypes.input), "\n");
        inputFile.RandomFillFile();

        var outputFile = new StreamWriter(Functions.GetFileName(4, Functions.FileTypes.output));

        foreach (var number in inputFile.ReadNextElement())
        {
            outputFile.Write(number.ToInt() / k + "\n");
        }

        outputFile.Close();

        var fin = new StreamReader(Functions.GetFileName(4, Functions.FileTypes.output));
        Console.WriteLine("Output File:");
        Console.WriteLine(fin.ReadToEnd());
    }

    public static void FifthTask()
    {
        Console.WriteLine("\nFifth Task");
        var inputFile = new TextNumberFile(Functions.GetFileName(5, Functions.FileTypes.input), " ");
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
        var inputFile = new TextFile(Functions.GetFileName(6, Functions.FileTypes.input), numbersNumRange: new int[] {100, 200});
        inputFile.RandomFillFile();

        var outputFile = new StreamWriter(Functions.GetFileName(6, Functions.FileTypes.output));
        
        foreach (var line in inputFile.ReadNextElement())
        {
            if (line.str.Length >= 1 && line.str[0] == 'б' || line.str.Length >= 2 && line.str[1] == 'б')
            {
                outputFile.WriteLine(line);
            }
        }

        outputFile.Close();

        Console.WriteLine("Output:");
        var fin = new StreamReader(Functions.GetFileName(6, Functions.FileTypes.output));
        Console.WriteLine(fin.ReadToEnd());
    }

    public static void Main()
    {
        Console.WriteLine("First Task");
        FirstTask();
        Console.WriteLine("\nSecond Task");
        SecondTask();
        Console.WriteLine("\nThird Task");
        ThirdTask();
        Console.WriteLine("\nFourth Task");
        FourhtTask(3);
        Console.WriteLine("\nTFifth Task");
        FifthTask();
        Console.WriteLine("\nSixth Task");
        SixthTask();
    }
}