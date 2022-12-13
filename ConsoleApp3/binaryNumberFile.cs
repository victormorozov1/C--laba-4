namespace Files
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
            try
            {
                fout = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public override void WriteElement(Number element)
        {
            fout.Write(element.y);
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
            element.y = fin.ReadInt32();
        }

        public override void CloseReader()
        {
            fin.Close();
        }
    }
}