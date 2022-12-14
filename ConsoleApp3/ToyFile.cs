using Files;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    public class ToyFile : BaseFile<Toy, StreamReader, StreamWriter>
    {
        private readonly int[] numbersRange;
        public bool debug;

        public ToyFile(string filename, int[] numbersNumRange, bool debug = true) : base(filename, numbersNumRange, debug) { }

        public override void OpenWriter()
        {
            fout = new StreamWriter(filename);
        }

        public override void WriteElement(Toy element)
        {
            fout.Write(element);
        }

        public override void CloseWriter()
        {
            fout.Close();
        }

        public override void OpenReader()
        {
            fin = new StreamReader(filename);
        }

        public override void ReadElement(Toy toy)
        {
            string word = "";
            for (int i = 0; i < Toy.readFieldsNum; i++)
            {
                char firstChar = fin.ReadSpacesAndFirstChar();
                word += firstChar;

                foreach (char c in fin.ReadWord())
                {
                    word += c;
                }

                if (i != Toy.readFieldsNum - 1)
                {
                    word += " ";
                }
            }

            toy.SetFromString(word);
        }

        public override void CloseReader()
        {
            fin.Close();
        }
    }
}
