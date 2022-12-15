using FileElements;

namespace BaseFileClasses;

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