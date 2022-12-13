using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Files
{
    public interface FileElementInterface<T>
    {
        public static abstract T GetRandom(Random random);
        public string ToString();
        public static abstract T Read(BinaryReader reader);
    }

    /*public interface WriterInterface<T>
    {
        public static 
    }*/

    /*public static class IntExtension
    {

        public static int GetRandom(Random random, int[]range)
        {
            if (random == null)
            {
                random = new Random();
            }
            return random.Next(range[0], range[1]);
        }
    }*/

    public class Number: FileElementInterface<Number>
    {
        int y;
        public Number(int y)
        {
            this.y = y;
        }
        public static Number GetRandom(Random random)
        {
            if (random == null)
            {
                random = new Random();
            }
            return new Number(random.Next(-100, 100));
        }

        public string ToString()
        {
            return y.ToString();
        }

        public static Number Read(BinaryReader reader)
        {
            Number t;
            try
            {
                t = new Number(reader.ReadInt32());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new Number(0);
            }

            return t;
        }
    }
}
