using System; 

namespace labration
{
    public class Föremål : Entitet // Deklarera en klass som heter Föremål och ärver från Entitet
    {
        private string _namn; // Privat fält för föremålets namn

        // Egenskap för att få och sätta föremålets namn
        public string Namn 
        { 
            get { return _namn; } // Hämta föremålets namn
            set { _namn = value; } // Sätt föremålets namn
        }

        // Konstruktor för Föremål klassen
        public Föremål(int x, int y, char symbol, string namn) : base(x, y, symbol)
        {
            _namn = namn; // Sätt föremålets namn när en ny föremål skapas
        }
    }
}
