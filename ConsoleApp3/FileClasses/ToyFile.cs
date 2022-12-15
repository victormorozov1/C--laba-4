using FileElements;
using BaseFileClasses;

namespace FileClasses;

public class ToyFile : BinaryBaseFile<Toy>
{
    public ToyFile(string filename, int[] numbersNumRange, bool debug = true) : base(filename, numbersNumRange, debug) { }

    public override bool WriteElement(Toy element)
    {
        element.BinWrite(fout);
        return true;
    }

    public override bool ReadElement(Toy toy)
    {
        toy.SetFromBinRead(fin);
        return true;
    }
}
