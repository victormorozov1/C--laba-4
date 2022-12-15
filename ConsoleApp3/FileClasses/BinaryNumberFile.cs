using BaseFileClasses;
using FileElements;

namespace FileClasses;

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