using System;
namespace labration;

public class Program
{


}
    public class Entitet
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Symbol { get; set; }

    public Entitet(int x, int y, char symbol)
    {
        X = x;
        Y = y;
        Symbol = symbol;
    }
}


public class Spelare : Entitet
{
    public int Livskraft { get; set; }

    public Spelare(int x, int y, char symbol) : base(x, y, symbol)
    {
        Livskraft = 100;
    }
}


public class Varelse : Entitet
{
    public int Livskraft { get; set; }
    public List<Förmåga> Förmågor { get; set; }
    public ConsoleColor Färg { get; set; }

    public Varelse(int x, int y, char symbol, ConsoleColor färg) : base(x, y, symbol)
    {
        Livskraft = 100;
        Förmågor = new List<Förmåga>();
        Färg = färg;
    }

    public void LäggTillFörmåga(Förmåga förmåga)
    {
        Förmågor.Add(förmåga);
    }
}

public class Förmåga
{
    public string Namn { get; set; }
    public Action<Spelare> Använd { get; set; }

    public Förmåga(string namn, Action<Spelare> använd)
    {
        Namn = namn;
        Använd = använd;
    }


    
}
public class Hälsopaket : Entitet
{
    public Hälsopaket(int x, int y, char symbol, ConsoleColor färg) : base(x, y, symbol)
    {
        Färg = färg;
    }

    public ConsoleColor Färg { get; set; }

    public void Använd(Spelare spelare)
    {
        spelare.Livskraft += 25;
        if (spelare.Livskraft > 100);
    }
}