using System;
namespace labration;

public class Föremål : Entitet
    {
           private string _namn;

    public string Namn 
    { 
        get { return _namn; }
        set { _namn = value; }
    }

        public Föremål(int x, int y, char symbol, string namn) : base(x, y, symbol)
        {
            _namn = namn;
            
        }
    }
