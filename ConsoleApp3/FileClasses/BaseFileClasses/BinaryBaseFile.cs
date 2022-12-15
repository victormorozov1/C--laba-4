using FileElements;
using System.Runtime.Serialization.Formatters.Binary;

namespace BaseFileClasses;

public class BinaryBaseFile<ElementType> : BaseFile<ElementType, BinaryReader, BinaryWriter> where ElementType : FileElementInterface<ElementType>
{
    private readonly int[] numbersRange;
    public bool debug;
    BinaryFormatter formatter;

    public BinaryBaseFile(string filename, int[] numbersNumRange = null, bool debug = true) : base(filename, numbersNumRange, debug) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
    }

    public override void OpenWriter()
    {
        fout = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate));
    }

    public override void CloseWriter()
    {
        fout.Close();
    }

    public override void OpenReader()
    {
        fin = new BinaryReader(File.OpenRead(filename));
    }

    public override void CloseReader()
    {
        fin.Close();
    }
}