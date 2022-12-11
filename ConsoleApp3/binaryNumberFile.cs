namespace Files
{
    public class BinaryNumberFile
    {
        private readonly int[] numbersNumRange, numbersRange;
        public readonly string filename;
        public int numbersNum;
        public bool debug;
        private Random random;
        
        public BinaryNumberFile(string filename,
            int numbersNumLowBorder = 100,
            int numbersNumTopBorder = 1000,
            int numbersLowBorder = -100,
            int numbersTopBorder = 100,
            bool debug = true)
        {
            numbersNumRange = new [] { numbersNumLowBorder, numbersNumTopBorder };
            numbersRange = new [] { numbersLowBorder, numbersTopBorder };

            this.filename = filename;
            this.debug = debug;
        }

        int NextRandomNum()
        {
            if (random == null)
            {
                random = new Random();
            }

            return random.Next(numbersRange[0], numbersRange[1]);
        }
        
        public void FillNumbers(Func<int> NextNumFunc = null)
        {
            if (NextNumFunc == null)
            {
                NextNumFunc = NextRandomNum;
            }
            
            BinaryWriter fout;
            try
            {
                fout = new BinaryWriter(File.Open(filename, FileMode.Create));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Random random = new Random();
            numbersNum = random.Next(numbersNumRange[0], numbersNumRange[1]);

            for (int i = 0, r = NextNumFunc(); i < numbersNum; i++, r = NextNumFunc())
            {
                fout.Write(r);
                if (debug)
                {
                    Console.Write(r + " ");
                }
            }
            if (debug)
            {
                Console.WriteLine();
            }

            fout.Close();

            if (debug)
            {
                Console.WriteLine($"File {filename} saved into {Directory.GetCurrentDirectory()}");
                Console.WriteLine($"File {filename} contains {numbersNum} numbers");
            }
        }

        public IEnumerable<int> ReadNextNum()
        {
            BinaryReader fin;
            try
            {
                fin = new BinaryReader(File.OpenRead(filename));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                yield break;
            }
            
            for (int i = 0; i < numbersNum; i++)
            {
                int number;
                
                try
                {
                    number = fin.ReadInt32();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    yield break;
                }
                
                yield return number;
            }
        }
    }
}