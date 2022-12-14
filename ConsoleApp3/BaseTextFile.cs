using Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class BaseTextFile<FileElement> : BaseFile<FileElement, StreamReader, StreamWriter> 
        where FileElement : FileElementInterface<FileElement>
    {
        public BaseTextFile(string filename, int[] numbersNumRange, bool debug = true) : base(filename, numbersNumRange, debug) { }

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
}
