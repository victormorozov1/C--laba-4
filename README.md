# 4 лабораторная арбота
*Необходимо запускать на net7.0*

### Интерфейс стандартного элемента

Сущность стандартного элемента файла (числа, строки или структуры) описана следующим интерфейсом:

```c#
public interface FileElementInterface<T>
{
    public static abstract T GetRandom(Random random);

    public string ToString();
}
```

Метод ```GetRandom``` возвращает случайный экземпляр соответствующего класса.

Метод ```ToString``` возвращает строковое представление.

---

Чтобы создать класс стандартного элемента файла, соответствующего данному интерфейсу, необходимо унаследовать его от выше описанного интерфейса. Например при создании класса ```Number```:

```c#
public class Number: FileElementInterface<Number>
```

У класса ```Number``` есть все методы, необходимые для соответствия интерфейсу:
```c#
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
```

А так-же конструктор:
```c#
public Number(int y)
{
    this.y = y;
}
```

И пара других полезных методов:
```c#
public void SetVal(int val)
{
    y = val;
}

public int ToInt() 
{
    return y;
}
```

Аналогично описываются другие классы элементов файла.

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

Как видно, некоторые методы класса, которые будут одинаковы для всех дочерних классов, уже описаны. Остальные методы будут описываться в дочерних классах.

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

Например класс ```BinaryNumberFile``` я описываю так:

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

Для удобства я расширил данный класс, добавив к нему полезные методы:

1) Первый метод считывает все разделяющие символы (пробелы, переводы строк...) и возвращает первый считанный символ слова.

```c#
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
```

2) Данный метод по очереди возвращает все символы слова после первого символа. Метод удобно использовать после предыдущего, чтобы счиатть все слово.
```c#
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
```
3) Метод, обьединяющий предыдущие 2 в один. Возвращает считанную строку.
```c#
public static string ReadString(this StreamReader reader, char[] sepSymbols = null)
{
    string s = "" + ReadSpacesAndFirstChar(reader, sepSymbols2: sepSymbols);
    foreach (char c in ReadStringAfterFirstChar(reader, sepSymbols2: sepSymbols))
    {
        s += c;
    }
    return s;
}
```

### Вспомогательные функции
Получение первой цифры числа
```c#
public static int FirstDigit(int number)
{
    return int.Parse(Math.Abs(number).ToString()[0].ToString());
}
```

Функция заполнения матрицы числами из файла:
```c#
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
```

Функция получения имени файла для текущего задания (чтобы все имена файлов быди в одном формате)
```c#
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
```
В этой функции используется следующий ```enum```:
```c#
public enum FileTypes
{
    input,
    output
}
```

### Выполнение поставленных задач:

#### Задача 1.

***Переписать в новый файл компоненты исходного, начинающиеся и
   заканчивающиеся на одну и ту же цифру***

Ссоздание и заполнение исходного файла:
```c#
var file = new BinaryNumberFile(Functions.GetFileName(1, Functions.FileTypes.input), new int[] { 100, 1000 }, debug: true);
file.RandomFillFile();
```
Открытие файла, куда будем записывать ответ:
```c#
BinaryWriter fout = new BinaryWriter(File.Open(Functions.GetFileName(1, Functions.FileTypes.output), FileMode.Create));
```

Переписываем числа, соблюдая поставленные условия:
```c#
foreach (var number in file.ReadNextElement())
{
    int absNum = Math.Abs(number.ToInt());
    if (absNum % 10 == Functions.FirstDigit(absNum))
    {
        fout.Write(number.ToInt());
    }
}
```
Закрываем выходной файл:
```c#
fout.Close();
```

#### Задача 2.
***Скопировать элементы заданного файла в квадратную матрицу
размером n×n (если элементов файла недостает, заполнить оставшиеся
элементы матрицы нулями). Заменить все элементы, начинающиеся с
заданной цифры, на эту цифру.***

Создаем и заполняем входной файл, выводим его в консоль чтобы было удобнее отслеживать результат:
```c#
BinaryNumberFile inputFile = new BinaryNumberFile(Functions.GetFileName(2, Functions.FileTypes.input), 
            new int[] { 10, 100 }, debug: false);
inputFile.RandomFillFile();

inputFile.WriteFileTOConsole();
Console.WriteLine("\n");
```

Заполняем, и, если нужно, выводим матрицу:
```c#
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
```

#### Задание 3.

***Файл содержит сведения об игрушках: название игрушки, ее стоимость в
рублях и возрастные границы (например, игрушка может предназначаться для
детей от двух до пяти лет). Для детей какого возраста предназначены кубики?
Указать их среднюю стоимость.***

Создаем и заполняем файл (код не привожу, ибо он аналогичен прошлым задачам)

Ищем и выводим ответ:

```C#
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
```

#### Задание 4.

***Получить новый файл, уменьшив каждый элемент исходного в k раз***

Создаем входной и выходной файлы, фходной заполняем случайными числами.

Затем решаем поставленную задачу:
```c#
foreach (var number in inputFile.ReadNextElement())
{
   outputFile.Write(number.ToInt() / k + "\n");
}
```
Закрываем выходной файл. (Входной закрывается автоматически внутри функции)

#### Задание 5.

***Найти сумму первого и максимального элементов.***

Создаем и заполняем вхродной файл. 

Открываем его для чтения, проверяем, что он не пустой (иначе мы не смогли бы найти ответ):

```c#
var numbers = inputFile.ReadNextElement().GetEnumerator();

if (!numbers.MoveNext())
{
   Console.WriteLine("Файл пуст");
   return;
}
```

Ищем и выводи ответ:

```c#
var firstNum = numbers.Current.ToInt();
int max = firstNum;
while (numbers.MoveNext())
{
   max = Math.Max(max, numbers.Current.ToInt());
}

Console.WriteLine("Сумма первого и максимального элементов: " + (firstNum + max));
```

#### 6 Задание

***В файле хранится произвольный текст. Переписать в другой файл строки, в
которых первой или второй буквой является "б"***

Открываем нужные файлы

Делаем задачу:

```c#
foreach (var line in inputFile.ReadNextElement())
{
   if (line.str.Length >= 1 && line.str[0] == 'б' || line.str.Length >= 2 && line.str[1] == 'б')
   {
       outputFile.WriteLine(line);
   }
}
```

### Итог

Программа полностью работает.

Для запуска в файле ```Program.cs``` запускайте функцию, соответствующую заданию.

Тесты:
```
First Task
Input File:
85 14 96 -25 -52 27 -20 -58 -66 30 -99 71 -81 1 -79

Output File
-66
-99
1

Second Task
Input File:
-42 51 -31 14 47 -40 -50 78 37 23 -37 -66 -33 20 -36 11 77 -77 89 -54 9 -21 -33 -43 43 42 46 -78 39 -41 63 13 98 -71 10 26 -28 51 83 90 -99 32 -3 -8 -4 -78 77 -44 79 -35 95 -17 19 12 -7 30 -17 -24 -43 -85 -24

Output Matrix:
-42 51 -31 1 47 -40 -50 78 37 23
-37 -66 -33 20 -36 1 77 -77 89 -54
9 -21 -33 -43 43 42 46 -78 39 -41
63 1 98 -71 1 26 -28 51 83 90
-99 32 -3 -8 -4 -78 77 -44 79 -35
95 1 1 1 -7 30 1 -24 -43 -85
-24 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0

Third Task
Input File:
Zaxar 1878 4 13
кубики 11029 2 10
Potato 9179 12 12
Spaik 4856 8 14
Brawl_Stars_Toy 2370 8 15
кубики 5666 7 9
Star 1376 7 9
кубики 9977 5 11
Star 8788 14 15
Веселый_водовоз 9063 12 15
Zaxar 6757 0 17
кубики 7152 1 1
Zaxar 7766 0 9
кубики 5286 5 15
Веселый_водовоз 1272 16 16
Star 6092 14 16
Spaik 5667 16 16
Spaik 8062 12 15
Brawl_Stars_Toy 4541 2 5
Brawl_Stars_Toy 1290 17 17
кубики 1716 10 11
Spaik 7637 0 7
Spaik 429 4 5
Spaik 4822 16 16
Star 6630 10 12
Spaik 2183 2 17
Brawl_Stars_Toy 1919 9 16
Zaxar 8606 8 8
Spaik 7864 5 13
кубики 4966 17 17
кубики 10416 10 11
Веселый_водовоз 1071 7 16
Star 9696 0 1
Веселый_водовоз 2403 1 2
Spaik 8074 13 16
Potato 770 3 12
Spaik 4738 9 13
Spaik 3217 7 15
Star 3466 4 5
Brawl_Stars_Toy 5336 13 14
Кубики подходят для детей возрастом от 1 до 17
Сркдняя цена наборов 7026

Fourth Task
60 99 -97 39 57 -99 29 87 42 -3 -56 41 -8 4 -89 54 44 -53 93 10 47 89 84 -92 -54 16 21 -14 -74 -15 -51 0 -83 -64 -24 73 73 -82 -59 67 -8 39 79 -30 -19 -100 46
File 4-task-in.txt saved into C:\Users\danil\RiderProjects\ConsoleApp3\ConsoleApp3\bin\Debug\net7.0
File 4-task-in.txt contains 47 elements
Output File:
20
33
-32
13
19
-33
9
29
14
-1
-18
13
-2
1
-29
18
14
-17
31
3
15
29
28
-30
-18
5
7
-4
-24
-5
-17
0
-27
-21
-8
24
24
-27
-19
22
-2
13
26
-10
-6
-33
15


TFifth Task

Fifth Task
-81 69 62 58 -31 -22 -15 16 81 -18 70 -72 -75 98 -97 5 -91 -48 -55 94 -18 -76 37 30 45 97 -27 -57 -61 50 -83 -97 48 -3 54 -60 -83 52 -10 -71 11 20 7 15 83 61 -63 61 75 -16 48 22 -64 98 76 -30 10 40 54 38 -28 -67 -81 2 69 41 -7 -81 -75 16 -37 -85 -30 -95 6 -15 34 -81 -52 11 -26 10 -25 -55 50 30 41 -39 -12 -33 -37
File 5-task-in.txt saved into C:\Users\danil\RiderProjects\ConsoleApp3\ConsoleApp3\bin\Debug\net7.0
File 5-task-in.txt contains 91 elements
Сумма первого и максимального элементов: 17

Sixth Task
кь алддеолйе щигш ййбдс цифтюйгту яс йсд иъ жъщ еэзижп рпш дхш бмо нрбзббфф кэтпжех ь хфоц ыощллъуща юцгчявзм бдъофсвущ мжыамшш щсс ьйел юнюшьгхвд ъ ао рдууут фщюя ншв цхржн снывъ эъоейбмря зл йзбдз ыивэпнь б э укъ ь йъ кыдод з езщэ ж цксщмйю хк ифм ф й нлфйхке внп гр хуьй кеб ч рч кллэююр оъкбйлзтн вжэспьчн нхэ ьнс уьтюбуб щвюзэяав эуэюжшоиы лгязйсмц шяэщчшхгц бъцюреег ыощэт вщщдо мъцш яхаеблпп еещюоия фаыьц тгьреыхж жлвыччэ щауиэщт х свъуф жа аукоыъош юхэдюй няччф фзкпдгжюу эетт фидшаь югнэе лгмгфабял нюзуи ыяпгкфгму эйпукь щшрж кчноываву цшыыртйгю жбшп эптъй ещшь зис жы фйъыллъбб ю вбц аоыон плъшу яыулцэмж жюфесдфи йеесчвея ммяиэс тиогад гщкгцз дщнщйчлю э вюхъкдз нйуж
File 6-task-in.txt saved into C:\Users\danil\RiderProjects\ConsoleApp3\ConsoleApp3\bin\Debug\net7.0
File 6-task-in.txt contains 113 elements
Output:
б э
жбшп
```
