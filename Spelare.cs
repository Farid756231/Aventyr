using System;
namespace labration;

public class Spelare : Entitet
{
    public int Livskraft { get; set; }
     public List<Föremål> Väska { get; set; }

    public Spelare(int x, int y, char symbol) : base(x, y, symbol)
    {
        Livskraft = 100;
         Väska = new List<Föremål>();
    }

         public void LäggTillIFörråd(Föremål föremål)
        {
            Väska.Add(föremål);
        }

        public void AnvändFöremål(Föremål föremål)
        {
            // Implementera logik för att använda föremålet och dess effekter på spelaren
        }


}
