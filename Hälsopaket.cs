using System;
namespace labration;
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
