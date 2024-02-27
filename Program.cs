using System;
namespace labration;

public class Program
{  


  public static void Main(string[] args)
    {







}
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