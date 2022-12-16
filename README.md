# 4 лабораторная арбота
*Необходимо запускать на net7.0*

### Интерфейс стандартного элемента

Сущность стандартного элемента файла (числа, строки или структуры) описана следующим интерфейсом:



```
public interface FileElementInterface<T>
{
    public static abstract T GetRandom(Random random);

    public string ToString();
}
```

Чтобы создать класс стандапртного элемента файла, соответствующего данному интерфейсу, делаем так:

```
public class Number: FileElementInterface<Number>
{
    private int y;
    public Number(int y)
    {
        this.y = y;
    }
    public static Number GetRandom(Random random)
    {
        if (random == null)
        {
            random = new Random();
        }
        return new Number(random.Next(-100, 100));
    }

    public override string ToString()
    {
        return y.ToString();
    }

    public void SetVal(int val)
    {
        y = val;
    }

    public int ToInt() 
    {
        return y;
    }
}

```

### Классы

#### Базовый класс файла:
```
public class BaseFile<ElementType, ReaderType, WriterType> where ElementType : FileElementInterface<ElementType>
{
    public readonly string filename;
    public int elementsNum;
    public int[] elementsNumRange;
    public bool debug;
    private Random random;

    public WriterType fout;
    public ReaderType fin;

    public BaseFile(string filename, int[] elementsNumRange = null, bool debug = true)
    {
        this.filename = filename;
        this.debug = debug;

        if (elementsNumRange == null)
        {
            this.elementsNumRange = new int[] { 10, 100 };
        }
        else
        {
            this.elementsNumRange = elementsNumRange;
        }
    }

    public virtual void OpenWriter() { }
    public virtual bool WriteElement(ElementType element) => false;
    public virtual void CloseWriter() { }

    public virtual void OpenReader() { }
    public virtual bool ReadElement(ElementType element) => false;
    public virtual void CloseReader() { }

    public void RandomFillFile()
    {
        try
        {
            OpenWriter();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Random random = new Random();
        elementsNum = random.Next(elementsNumRange[0], elementsNumRange[1]);

        var r = ElementType.GetRandom(random);
        for (int i = 0; i < elementsNum; i++, r = ElementType.GetRandom(random))
        {
            WriteElement(r);
            if (debug)
            {
                Console.Write(r + " ");
            }
        }
        if (debug)
        {
            Console.WriteLine();
        }

        CloseWriter();

        if (debug)
        {
            Console.WriteLine($"File {filename} saved into {Directory.GetCurrentDirectory()}");
            Console.WriteLine($"File {filename} contains {elementsNum} elements");
        }
    }

    public void WriteFileTOConsole(string sep = " ")
    {
        foreach (var element in ReadNextElement(elementsNum))
        {
            Console.Write(element + sep);
        }
    }

    public IEnumerable<ElementType> ReadNextElement(int elementsNum = -1)
    {
        if (elementsNum == -1)
        {
            elementsNum = this.elementsNum;
        }

        try
        {
            OpenReader();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        for (int i = 0; i < elementsNum; i++)
        {
            ElementType t = ElementType.GetRandom(random); // govnokod
            try
            {
                ReadElement(t);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                yield break;
            }

            yield return t;
        }

        CloseReader();
    }
}
```

От данного класса я наследовал классы ```BasetextFile``` и ```BinaryBaseFile```. 

Так к примеру я описываю ```BaseTextFile```:
```
public class BaseTextFile<FileElement> : BaseFile<FileElement, StreamReader, StreamWriter>
    where FileElement : FileElementInterface<FileElement>
{
    public BaseTextFile(string filename, int[] numbersNumRange = null, bool debug = true) : base(filename, numbersNumRange, debug) { }

    public override void OpenWriter()
    {
        fout = new StreamWriter(filename);
    }

    public override void CloseWriter()
    {
        fout.Close();
    }

    public override void OpenReader()
    {
        fin = new StreamReader(filename);
    }

    public override void CloseReader()
    {
        fin.Close();
    }
}
```

От данных классов я наследую классы ```BinaryNumberFile```, ```TextFile```, ```TextNumberFile``` и ```ToyFile```.

Например класс ```BinaryNumberFile``` я создаю так:

```
public class BinaryNumberFile : BaseFileClasses.BinaryBaseFile<Number>
{
    public BinaryNumberFile(string filename, int[] numbersNumRange = null, bool debug = true) : base(filename, numbersNumRange, debug) { }

    public override bool WriteElement(Number element)
    {
        fout.Write(element.ToInt());
        return true;
    }

    public override bool ReadElement(Number element)
    {
        element.SetVal(fin.ReadInt32());
        return true;
    }
}
```

Остальные классы описываются аналогично.

### Расаширение класса ```StreamWriter```

Для удобства я расгирил данный класс.

```
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
```

### Вспомогательные функции

```
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

    public static string GetFileName(int taskNum, FileTypes type)
    {
        string t;
        if (type == FileTypes.input)
        {
            t = "in";
        }
        else
        {
            t = "out";
        }
        return $"{taskNum}-task-{t}.txt";
    }

    public enum FileTypes
    {
        input,
        output
    }
}
```

### Итог

Программа полностью работает.

Для запуска в файле ```Program.cs``` запускайте функцию, соответствующую заданию.
