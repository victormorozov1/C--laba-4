namespace Files
{ 
    public class BinaryNumberFile : BinaryBaseFile<Number>
    {
        private readonly int[] numbersRange;
        
        public BinaryNumberFile(string filename,
            int[] numbersNumRange,
            int[] numbersRange,
            bool debug = true) : base(filename, numbersNumRange, debug)
        {
            this.numbersRange = numbersRange;
        }

        public override void WriteElement(Number element)
        {
            fout.Write(element.ToInt());
        }

        public override void ReadElement(Number element)
        {
            element.SetVal(fin.ReadInt32());
        }
    }
}