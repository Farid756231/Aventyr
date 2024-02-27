﻿using System;
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