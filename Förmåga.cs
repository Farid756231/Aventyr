using System; 

namespace labration
{
    public class Förmåga // Deklarera en klass som heter Förmåga
    {
        // Egenskaper för förmågan
        public string Namn { get; set; } // Namnet på förmågan
        public Action<Spelare> Använd { get; set; } // En delegat som representerar hur förmågan används på en spelare

        // Konstruktor för Förmåga klassen
        public Förmåga(string namn, Action<Spelare> använd)
        {
            Namn = namn; // Sätt namnet på förmågan
            Använd = använd; // Sätt hur förmågan används på en spelare
        }
    }
}
