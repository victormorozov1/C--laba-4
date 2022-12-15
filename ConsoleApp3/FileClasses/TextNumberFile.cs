using FileElements;
using BaseFileClasses;
using Extensions;
using System.Security;

namespace FileClasses;

public class TextNumberFile : BaseTextFile<Number>
{
    public int[] numbersRange;
    private string sep;
    public TextNumberFile(string filename, string sep, int[] numbersNumRange = null, bool debug = true) 
        : base(filename, numbersNumRange, debug) 
    {
        this.sep = sep;
    }

    public override bool WriteElement(Number element)
    {
        fout.Write(element + sep);
        return true;
    }

    public override bool ReadElement(Number number)
    {
        var line = fin.ReadString();
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
