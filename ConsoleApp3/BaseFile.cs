namespace Files
{
    public class BaseFile<ElementType, ReaderType, WriterType> where ElementType: FileElementInterface<ElementType, WriterType>
    {
        public readonly string filename;
        public int elementsNum;
        public int[] elementsNumRange;
        public bool debug;
        private Random random;

        public WriterType fout;
        public ReaderType fin;

        public BaseFile(string filename, int[] elementsNumRange, bool debug = true)
        {
            this.filename = filename;
            this.debug = debug;
            this.elementsNumRange = elementsNumRange;
        }

        public virtual void OpenWriter() { }
        public virtual void WriteElement(ElementType element) { }
        public virtual void CloseWriter() { }

        public virtual void OpenReader() { }
        public virtual void ReadElement(ElementType element) { }
        public virtual void CloseReader() { }

        public void RandomFillFile()
        {
            /*BinaryWriter fout;
            try
            {
                //var file = File.Open("t.txt", FileMode.Create);
                fout = new WriterType(File.Open(filename, FileMode.Create));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }*/

            OpenWriter();

            Random random = new Random();
            elementsNum = random.Next(elementsNumRange[0], elementsNumRange[1]);

            var r = (ElementType.GetRandom(random));
            for (int i = 0; i < elementsNum; i++, r = ElementType.GetRandom(random))
            {
                //fout.Write(r.ToString());
                WriteElement(r);
                if (debug)
                {
                    Console.Write(r + " ");
                }
            }
            if (debug)
            {
                Console.WriteLine();
            }

            //fout.Close();

            CloseWriter();

            if (debug)
            {
                Console.WriteLine($"File {filename} saved into {Directory.GetCurrentDirectory()}");
                Console.WriteLine($"File {filename} contains {elementsNum} numbers");
            }
        }

        public IEnumerable<ElementType> ReadNextElement(int elementsNum = -1)
        {
            /*BinaryReader fin;
            try
            {
                fin = new BinaryReader(File.OpenRead(filename));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                yield break;
            }*/

            if (elementsNum == -1)
            {
                elementsNum = this.elementsNum;
            }

            OpenReader();

            for (int i = 0; i < elementsNum; i++)
            {
                ElementType t = ElementType.GetRandom(random); // govnokod
                try
                {
                    ReadElement(t);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    yield break;
                }

                yield return t;
            }

            CloseReader();
        }
    }
}