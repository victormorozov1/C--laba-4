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
    }

    public class Number: FileElementInterface<Number>
    {
        public int y;
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
    }
}
