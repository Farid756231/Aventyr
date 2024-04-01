using System; 

namespace labration
{
    public class Varelse : Entitet // Deklarera en klass som heter Varelse och ärver från Entitet
    {
        // Egenskaper för varelsen
        public int Livskraft { get; set; } // Livspoängen för varelsen
        public List<Förmåga> Förmågor { get; set; } // En lista som representerar varelsens förmågor
        public ConsoleColor Färg { get; set; } // Färgen på varelsens symbol

        // Konstruktor för Varelse klassen
        public Varelse(int x, int y, char symbol, ConsoleColor färg) : base(x, y, symbol)
        {
            Livskraft = 100; // Varelsens livskraft sätts till 100 när en ny varelse skapas
            Förmågor = new List<Förmåga>(); // Skapa en ny tom lista för varelsens förmågor
            Färg = färg; // Sätt färgen på varelsens symbol
        }

        // Metod för att lägga till en förmåga till varelsen
        public void LäggTillFörmåga(Förmåga förmåga)
        {
            Förmågor.Add(förmåga);
        }
    }
}
