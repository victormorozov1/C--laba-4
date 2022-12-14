﻿namespace Files
{ 
    public class BinaryNumberFile : BaseFile<Number, BinaryReader, BinaryWriter>
    {
        private readonly int[] numbersRange;
        public bool debug;
        
        public BinaryNumberFile(string filename,
            int[] numbersNumRange,
            int[] numbersRange,
            bool debug = true) : base(filename, numbersNumRange, debug)
        {
            this.numbersRange = numbersRange;
        }

        public override void OpenWriter()
        {
            fout = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate));
        }

        public override void WriteElement(Number element)
        {
            fout.Write(element.ToInt());
        }

        public override void CloseWriter()
        {
            fout.Close();
        }

        public override void OpenReader()
        {
            fin = new BinaryReader(File.OpenRead(filename));
        }

        public override void ReadElement(Number element)
        {
            element.SetVal(fin.ReadInt32());
        }

        public override void CloseReader()
        {
            fin.Close();
        }
    }
}