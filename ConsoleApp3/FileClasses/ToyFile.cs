using FileElements;
using BaseFileClasses;

namespace FileClasses;

public class ToyFile : BinaryBaseFile<Toy>
{
    public ToyFile(string filename, int[] numbersNumRange, bool debug = true) : base(filename, numbersNumRange, debug) { }

    public override void WriteElement(Toy element)
    {
        fout.Write(element.ToString());
    }

    public override void ReadElement(Toy toy)
    {
        toy.SetFromString(fin.ReadString());
    }
}
