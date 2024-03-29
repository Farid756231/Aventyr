using System;
namespace labration;

public class Spelare : Entitet
{
    public ConsoleColor SymbolFärg { get; set; } = ConsoleColor.Green;
    public int Livskraft { get; set; }

    public int Försvar { get; set; }
    public List<Föremål> Väska { get; set; }

    public Spelare(int x, int y, char symbol) : base(x, y, symbol)
    {
        Livskraft = 100;
          Försvar = 0;
        Väska = new List<Föremål>();
    }
     
     public void ÄndraFärg(ConsoleColor färg)
{
    SymbolFärg = färg;
}
    public void LäggTillIFörråd(Föremål föremål)
    {
        Väska.Add(föremål);
    }

    public void AnvändFöremål(Föremål föremål,Varelse motståndare =null!)
    {
        switch (föremål.Namn)
        {
            case "Hälsodryck":
                Livskraft += 25;
                if (Livskraft > 100)
                    Livskraft = 100;
                Console.WriteLine("Du använde en hälsodryck och återfick 25 hälsa!");
                break;
            case "Rustning":
                // Implementera logik för att använda rustning
                SkyddaDig();
                Console.WriteLine("Du använder Rustningen och ökar ditt försvar!");
                break;
            default:
                Console.WriteLine("Det går inte att använda detta föremål.");
                break;
        }
    }
    private void SkyddaDig()
    {
         // Här antar jag att du har en variabel för spelarens försvar och en variabel för antal drag
        int försvar = 10;
        int antalDrag = 5;
       
       

        // Skriv ut meddelande om den ökade försvarseffekten
        Console.WriteLine($"Du använder Rustningen och ökar ditt försvar med {försvar} enheter under {antalDrag} drag eller turar.");
    }
}