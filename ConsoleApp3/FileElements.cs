namespace FileElements;

public interface FileElementInterface<T>
{
    public static abstract T GetRandom(Random random);

    public string ToString();
}

public class Number: FileElementInterface<Number>
{
    private int y;
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

    public override string ToString()
    {
        return y.ToString();
    }

    public void SetVal(int val)
    {
        y = val;
    }

    public int ToInt() 
    {
        return y;
    }
}

public class Toy : FileElementInterface<Toy>
{
    public int minAge, price;
    string name;
    public static int readFieldsNum = 3;

    public static string[] possibleNames = {
        "Spaik",
        "Potato",
        "Веселый_водовоз",
        "Zaxar",
        "Brawl_Stars_Toy",
        "Star"
    };

    public Toy(string name, int price, int minAge) {
        this.name = name;
        this.price = price;
        this.minAge = minAge;
    }

    public static Toy GetRandom(Random random)
    {
        if (random == null)
        {
            random = new Random();
        }

        var name = possibleNames[random.Next(possibleNames.Length)];
        var minAge = random.Next(0, 18);
        var price = random.Next(1, 11964);
        return new Toy(name, price, minAge);
    }

    public override string ToString()
    {
        return $"{name} {price} {minAge} ";
    }

    public static Toy FromString(string str, string sep = " ")
    {
        var splitData = str.Split(sep);
        return new Toy(splitData[0], int.Parse(splitData[1]), int.Parse(splitData[2]));
    }

    public void SetFromString(string str, string sep = " ")
    {
        Clone(Toy.FromString(str, sep: sep));
    }

    public void Clone(Toy other)
    {
        name = other.name;
        minAge = other.minAge;
        price = other.price;
    } 

    public void BinWrite(BinaryWriter fout)
    {
        int nameIndex = Array.IndexOf(possibleNames, name);
        fout.Write(nameIndex);
        fout.Write(price);
        fout.Write(minAge);
    }

    public static Toy BinRead(BinaryReader fin)
    {
        string name = possibleNames[fin.ReadInt32()];
        int price = fin.ReadInt32();
        int minAge = fin.ReadInt32();
        return new Toy(name, price, minAge);
    }

    public void SetFromBinRead(BinaryReader fin)
    {
        Clone(Toy.BinRead(fin));
    }
}
