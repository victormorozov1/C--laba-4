using FileElements;
using BaseFileClasses;

namespace FileClasses;

public class TextNumberFile : BaseTextFile<Number>
{
    public int[] numbersRange;
    public TextNumberFile(string filename, int[] numbersNumRange, bool debug = true) : base(filename, numbersNumRange, debug) { }

    public override bool WriteElement(Number element)
    {
        fout.WriteLine(element);
        return true;
    }

    public override bool ReadElement(Number number)
    {
        var line = fin.ReadLine();
        if (line == null)
        {
            return false;
        }
        try
        {
            number.SetVal(int.Parse(line));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return ReadElement(number);
        }
        return true;
    }
}
