using BaseFileClasses;
using FileElements;

namespace BaseFileClasses;

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