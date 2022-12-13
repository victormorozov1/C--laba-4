namespace Files
{
    
    public class BinaryNumberFile : BaseFile<Number>
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
    }
}