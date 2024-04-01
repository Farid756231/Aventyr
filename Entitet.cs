using System; 

namespace labration
{
    public class Entitet // Deklarera en klass som heter Entitet
    {
        // Egenskaper för entiteten
        public int X { get; set; } // X-koordinaten för entiteten
        public int Y { get; set; } // Y-koordinaten för entiteten
        public char Symbol { get; set; } // Symbolen som representerar entiteten

        // Konstruktor för Entitet klassen
        public Entitet(int x, int y, char symbol)
        {
            X = x; // Sätt X-koordinaten för entiteten
            Y = y; // Sätt Y-koordinaten för entiteten
            Symbol = symbol; // Sätt symbolen för entiteten
        }
    }
}
