namespace Files
{
    public class BaseFile<T>
    {
        public readonly string filename;
        public int elementsNum;
        public int[] elementsNumRange;
        public bool debug;
        private Random random;
        public Func<T> GetRandomElement;
        public Func<BinaryReader, T> ReadElement;

        public BaseFile(string filename, int[] elementsNumRange, bool debug = true)
        {
            this.filename = filename;
            this.debug = debug;
            this.elementsNumRange = elementsNumRange;
        }

        public void RandomFillFile()
        {
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
            elementsNum = random.Next(elementsNumRange[0], elementsNumRange[1]);

            T r = GetRandomElement();
            for (int i = 0; i < elementsNum; i++, r = GetRandomElement())
            {
                fout.Write(r.ToString());
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
                Console.WriteLine($"File {filename} contains {elementsNum} numbers");
            }
        }

        public IEnumerable<T> ReadNextElement(int elementsNum = -1)
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

            for (int i = 0; i < elementsNum; i++)
            {
                T t;

                try
                {
                    t = ReadElement(fin);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    yield break;
                }

                yield return t;
            }
        }
    }
}