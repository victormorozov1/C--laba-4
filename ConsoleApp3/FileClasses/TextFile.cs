using FileElements;
using BaseFileClasses;
using Extensions;

namespace FileClasses;

public class TextFile : BaseTextFile<FileString>
{
    public TextFile(string filename, int[] numbersNumRange = null, bool debug = true) : base(filename, numbersNumRange, debug) { }

    public override bool WriteElement(FileString element)
    {
        fout.Write(element + new string[] {" ", "\n" }[new Random().Next(0, 2)]);
        return true;
    }

    public override bool ReadElement(FileString s)
    {
        s.str = fin.ReadString(sepSymbols: new char[] {'\n'});
        return true;
    }
}
