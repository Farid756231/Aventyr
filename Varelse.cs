using System;
namespace labration;

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