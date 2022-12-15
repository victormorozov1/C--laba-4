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
    public int minAge, maxAge, price;
    public string name;
    public static int readFieldsNum = 4;

    public static string[] possibleNames = {
        "Spaik",
        "Potato",
        "Веселый_водовоз",
        "Zaxar",
        "Brawl_Stars_Toy",
        "Star",
        "кубики"
    };

    public Toy(string name, int price, int minAge, int maxAge) {
        this.name = name;
        this.price = price;
        this.minAge = minAge;
        this.maxAge = maxAge;
    }

    public static Toy GetRandom(Random random)
    {
        if (random == null)
        {
            random = new Random();
        }

        var name = possibleNames[random.Next(possibleNames.Length)];
        var minAge = random.Next(0, 18);
        var maxAge = random.Next(minAge, 18);
        var price = random.Next(1, 11964);
        return new Toy(name, price, minAge, maxAge);
    }

    public override string ToString()
    {
        return $"{name} {price} {minAge} {maxAge}";
    }

    public static Toy FromString(string str, string sep = " ")
    {
        var splitData = str.Split(sep);
        return new Toy(splitData[0], int.Parse(splitData[1]), int.Parse(splitData[2]), int.Parse(splitData[3]));
    }

    public void SetFromString(string str, string sep = " ")
    {
        Clone(Toy.FromString(str, sep: sep));
    }

    public void Clone(Toy other)
    {
        name = other.name;
        minAge = other.minAge;
        maxAge = other.maxAge;
        price = other.price;
    } 

    public void BinWrite(BinaryWriter fout)
    {
        int nameIndex = Array.IndexOf(possibleNames, name);
        fout.Write(nameIndex);
        fout.Write(price);
        fout.Write(minAge);
        fout.Write(maxAge);
    }

    public static Toy BinRead(BinaryReader fin)
    {
        string name = possibleNames[fin.ReadInt32()];
        int price = fin.ReadInt32();
        int minAge = fin.ReadInt32();
        int maxAge = fin.ReadInt32();   
        return new Toy(name, price, minAge, maxAge);
    }

    public void SetFromBinRead(BinaryReader fin)
    {
        Clone(Toy.BinRead(fin));
    }
}
