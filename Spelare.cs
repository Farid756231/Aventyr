using System; 

namespace labration
{
    public class Spelare : Entitet // Deklarera en klass som heter Spelare och ärver från Entitet
    {
        // Egenskaper för spelaren
        public ConsoleColor SymbolFärg { get; set; } = ConsoleColor.Green; // Färgen på spelarens symbol
        public int Livskraft { get; set; } // Spelarens livspoäng
        public int Försvar { get; set; } // Spelarens försvarspoäng
        public List<Föremål> Väska { get; set; } // En lista som representerar spelarens väska

        // Konstruktor för Spelare klassen
        public Spelare(int x, int y, char symbol) : base(x, y, symbol)
        {
            Livskraft = 100; // Spelarens livskraft sätts till 100 när en ny spelare skapas
            Försvar = 0; // Spelarens försvar sätts till 0 som standard
            Väska = new List<Föremål>(); // Skapa en ny tom lista för spelarens föremål
        }

        // Metod för att ändra färgen på spelarens symbol
        public void ÄndraFärg(ConsoleColor färg)
        {
            SymbolFärg = färg;
        }

        // Metod för att lägga till ett föremål i spelarens väska
        public void LäggTillIFörråd(Föremål föremål)
        {
            Väska.Add(föremål);
        }

        // Metod för att använda ett föremål
        public void AnvändFöremål(Föremål föremål, Varelse motståndare = null!)
        {
            switch (föremål.Namn)
            {
                case "Hälsodryck":
                    Livskraft += 25; // Öka spelarens livskraft med 25
                    if (Livskraft > 100)
                        Livskraft = 100; // Se till att spelarens livskraft inte överstiger 100
                    Console.WriteLine("Du använde en hälsodryck och återfick 25 hälsa!");
                    break;
                case "Rustning":
                    SkyddaDig(); // Anropa metoden för att använda rustningen
                    Console.WriteLine("Du använder Rustningen och ökar ditt försvar!");
                    break;
                default:
                    Console.WriteLine("Det går inte att använda detta föremål.");
                    break;
            }
        }

        // Privat metod för att öka spelarens försvar när rustning används
        private void SkyddaDig()
        {
            int försvar = 10; // Försvarspoängen som rustningen ger
            int antalDrag = 5; // Antal drag eller turar som försvarseffekten varar

            // Skriv ut meddelande om den ökade försvarseffekten
            Console.WriteLine($"Du använder Rustningen och ökar ditt försvar med {försvar} enheter under {antalDrag} drag eller turar.");
        }
    }
}
