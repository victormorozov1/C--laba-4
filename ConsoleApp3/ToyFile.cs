using Files;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
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

        public override void ReadElement(Toy toy) // Сделать через дополнения класса
        {
           // Console.WriteLine("In ReadElement");
            string word = "";
            for (int i = 0; i < Toy.readFieldsNum; i++)
            {
                char firstChar = Functions.Functions.ReadSpacesAndFirstChar(fin);
                word += firstChar;

                foreach (char c in Functions.Functions.ReadWord(fin))
                {
                    word += c;
                }

                if (i != Toy.readFieldsNum - 1)
                {
                    word += " ";
                }
                //Console.WriteLine("Word = " + word);
            }

            //Console.WriteLine("Word created: " + word);

            toy.SetFromString(word);

            //Console.WriteLine("Read end");
        }

        public override void CloseReader()
        {
            fin.Close();
        }
    }
}
