


using System;
namespace labration;

public class Program
{

     static Spelare spelaren;

    public static void Main(string[] args)
    {
        // Console.Title = "Fantastiskt Äventyr";
        // Console.ForegroundColor = ConsoleColor.Green;

        char[,] spelplan = new char[20, 20];

        spelaren = new Spelare(1, 1, '@');
        PlaceraEntitet(spelplan, spelaren);

        Varelse monster1 = new Varelse(0, 0, 'M', ConsoleColor.Red);
        monster1.Livskraft = 100;
        monster1.LäggTillFörmåga(new Förmåga("Sparka", s => { s.Livskraft -= 25; }));
        PlaceraSlumpmässigtEntitet(spelplan, monster1);

        Varelse monster2 = new Varelse(0, 0, 'M', ConsoleColor.Red);
        monster2.Livskraft = 100;
        monster2.LäggTillFörmåga(new Förmåga("Sparka", s=> { s.Livskraft -= 25; }));
        PlaceraSlumpmässigtEntitet(spelplan, monster2);

        Varelse monster3 = new Varelse(0, 0, 'M', ConsoleColor.Red);
        monster3.Livskraft = 100;
        monster3.LäggTillFörmåga(new Förmåga("Sparka", s => { s.Livskraft -= 25; }));
        PlaceraSlumpmässigtEntitet(spelplan, monster3);

        Hälsopaket hälsopaket1 = new Hälsopaket(0, 0, '+', ConsoleColor.Blue);
        PlaceraSlumpmässigtEntitet(spelplan, hälsopaket1);

        Hälsopaket hälsopaket2 = new Hälsopaket(0, 0, '+', ConsoleColor.Blue);
        PlaceraSlumpmässigtEntitet(spelplan, hälsopaket2);

        Hälsopaket hälsopaket3 = new Hälsopaket(0, 0, '+', ConsoleColor.Blue);
        PlaceraSlumpmässigtEntitet(spelplan, hälsopaket3);

        Föremål hälsodryck = new Föremål(0, 0, 'H', "Hälsodryck");
        PlaceraSlumpmässigtEntitet (spelplan, hälsodryck);

        Föremål eldpil = new Föremål(0, 0, 'E', "Eldpil");
        PlaceraSlumpmässigtEntitet(spelplan, eldpil);

        Föremål rustning = new Föremål(0, 0, 'R', "Rustning");
        PlaceraSlumpmässigtEntitet(spelplan, rustning);

        Föremål trollstav = new Föremål(0, 0, 'T', "Trollstav");
        PlaceraSlumpmässigtEntitet(spelplan, trollstav);

        Föremål förstoringsglas = new Föremål(0, 0, 'F', "Förstoringsglas");
        PlaceraSlumpmässigtEntitet(spelplan, förstoringsglas);

        while (true)
{
    RitaSpelplan(spelplan, spelaren, new List<Varelse> { monster1, monster2, monster3 });

    ConsoleKeyInfo keyInfo = Console.ReadKey();
    HanteraSpelarensDrag(spelplan, keyInfo);

    foreach (Entitet entitet in new List<Entitet> { monster1, monster2, monster3, hälsopaket1, hälsopaket2, hälsopaket3,hälsodryck, eldpil, rustning, trollstav, förstoringsglas})
    {
        if (spelaren.X == entitet.X && spelaren.Y == entitet.Y)
        {
            if (entitet is Varelse)
            {
                MötVarelse((Varelse)entitet);

                // Uppdatera positionen för varelsen till en ny slumpmässig position
                PlaceraSlumpmässigtEntitet(spelplan, (Varelse)entitet);
            }
            else if (entitet is Hälsopaket)
            {
                MötHälsopaket((Hälsopaket)entitet);

                // Uppdatera positionen för hälsopaketet till en ny slumpmässig position
                PlaceraSlumpmässigtEntitet(spelplan, (Hälsopaket)entitet);
            }
            else if (entitet is Föremål)
            {
                MötFöremål((Föremål)entitet,spelplan);

                // Uppdatera positionen för hälsopaketet till en ny slumpmässig position
                PlaceraSlumpmässigtEntitet(spelplan, (Föremål)entitet);
            }


            RitaSpelplan(spelplan, spelaren, new List<Varelse> { monster1, monster2, monster3 });
        }
    }

            if (spelaren.Livskraft <= 0)
            {
                Console.WriteLine("Du förlorade! Spelet är över.");
                break;
            }

            Console.Clear();
        }
    }
    public static void PlaceraSlumpmässigtEntitet(char[,] spelplan, Entitet entitet)
{
    Random random = new Random();

    int x, y;
    do
    {
        x = random.Next(0, spelplan.GetLength(1));
        y = random.Next(0, spelplan.GetLength(0));
    } while (spelplan[y, x] != '\0'); // Se till att positionen är tom

    entitet.X = x;
    entitet.Y = y;

    PlaceraEntitet(spelplan, entitet);
}


    public static void RitaSpelplan(char[,] spelplan,Spelare spelaren, List<Varelse> monsters)
    {
      
   
    Console.Clear();

    int rows = spelplan.GetLength(0);
    int cols = spelplan.GetLength(1);

    // Calculate the maximum symbol width
    int maxSymbolWidth = 5; // Default width for symbols without special padding
    foreach (char symbol in new char[] { '@', 'M', '+', 'H', 'E', 'R', 'T', 'F' })
    {
        int symbolWidth = symbol == '@' ? 5 : 3; // Adjust width for special symbols like '@'
        if (symbolWidth > maxSymbolWidth)
            maxSymbolWidth = symbolWidth;
    }

    // Calculate the maximum width of a row
    int maxRowWidth = cols * (maxSymbolWidth + 1) + 1;

    // Draw top border
    Console.WriteLine("+" + new string('-', maxRowWidth) + "+");

    for (int i = 0; i < rows; i++)
    {
        Console.Write("| "); // Draw left border
        for (int j = 0; j < cols; j++)
        {
            char symbol = spelplan[i, j];
            ConsoleColor color = ConsoleColor.Gray;

            switch (symbol)
            {
                case '\0':
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("".PadRight(maxSymbolWidth));
                    break;
                case '@':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("(@)".PadRight(maxSymbolWidth));
                    break;
                case 'M':
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("<<?>>".PadRight(maxSymbolWidth));
                    break;
                case '+':
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("+".PadRight(maxSymbolWidth));
                    break;
                case 'H':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("[o]".PadRight(maxSymbolWidth));
                    break;
                case 'E':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("¤¤¤".PadRight(maxSymbolWidth));
                    break;
                case 'R':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("--¤--".PadRight(maxSymbolWidth));
                    break;
                case 'T':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("//T//".PadRight(maxSymbolWidth));
                    break;
                case 'F':
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("**F**".PadRight(maxSymbolWidth));
                    break;
            }
        }
        Console.WriteLine("                    |"); // Draw right border
    }

    // Draw bottom border
    Console.WriteLine("+" + new string('-', maxRowWidth) + "+");

    // Center-align health message
    Console.WriteLine($"{"Din hälsa: " + spelaren.Livskraft + " % \n"}");

    
 Console.WriteLine("Spelarens väska: ");
    foreach (var item in spelaren.Väska)
    {
        Console.WriteLine($"{item.Namn}: {item.Symbol} ");
        
    }

    Console.WriteLine(" \n  ");

    foreach (var monster in monsters)
    {
        Console.WriteLine($"Monster Hälsa ({monster.Symbol}): {monster.Livskraft}%");
    }
      
    }

     

    public static void PlaceraEntitet(char[,] spelplan, Entitet entitet)
    {
        spelplan[entitet.Y, entitet.X] = entitet.Symbol;
    }

    public static void HanteraSpelarensDrag(char[,] spelplan, ConsoleKeyInfo keyInfo)
    {
        spelplan[spelaren.Y, spelaren.X] = '\0';

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                if (spelaren.Y > 0)
                    spelaren.Y--;
                break;
            case ConsoleKey.DownArrow:
                if (spelaren.Y < spelplan.GetLength(0) - 1)
                    spelaren.Y++;
                break;
            case ConsoleKey.LeftArrow:
                if (spelaren.X > 0)
                    spelaren.X--;
                break;
            case ConsoleKey.RightArrow:
                if (spelaren.X < spelplan.GetLength(1) - 1)
                    spelaren.X++;
                break;
        }

        PlaceraEntitet(spelplan, spelaren);
    }

    public static void MötVarelse(Varelse varelse)
    {
        Console.WriteLine($"Du möter en varelse med symbol {varelse.Symbol}");
        Console.WriteLine($"Varelsen har {varelse.Livskraft }% hälsa.");

        
    // Kontrollera om spelaren har kraft kvar för att fortsätta striden
    if (spelaren.Livskraft <= 0)
    {
        Console.WriteLine("Du är för svag för att fortsätta striden!");
        return;
    }

 
    // Kontrollera om varelsen har kraft kvar för att fortsätta striden
    if (varelse.Livskraft <= 0)
    {
        Console.WriteLine("Varelsen är redan besegrad!");
        return;
    }

    // Reducera varelsens kraft med 25%
    varelse.Livskraft -= 25;

    Console.WriteLine($"Varelsen tar skada och har nu {varelse.Livskraft}% hälsa kvar.");

    // Reducera spelarens kraft med 25%
    spelaren.Livskraft -= 25;

    Console.WriteLine($"Du tar skada och har nu {spelaren.Livskraft}% hälsa kvar.");
      
        if (varelse.Livskraft <= 0)
    {
        Console.WriteLine("Varelsen är besegrad! Du har vunnit!");
        return;
    }
    }

    public static void MötHälsopaket(Hälsopaket hälsopaket)
    {
        Console.WriteLine($"Du hittade ett hälsopaket med symbol {hälsopaket.Symbol}");
        Console.WriteLine($"Tryck 'H' för att använda hälsopaketet.");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.KeyChar == 'H' || keyInfo.KeyChar == 'h')
        {
            hälsopaket.Använd(spelaren);
        }
    }

   public static void MötFöremål(Föremål föremål, char[,] spelplan)
        {

            
            Console.WriteLine($"Du hittade ett föremål: {föremål.Namn}");
            Console.WriteLine("Tryck 'P' för att plocka upp föremålet.");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if ((keyInfo.KeyChar == 'P' || keyInfo.KeyChar == 'p')&& !spelaren.Väska.Contains(föremål))
            {
                spelaren.LäggTillIFörråd(föremål);
                
                 for (int y = 0; y < spelplan.GetLength(0); y++)
                 {
                   for (int x = 0; x < spelplan.GetLength(1); x++)
                    {
                        if (spelplan[y, x] == föremål.Symbol)
                          {
                              spelplan[y, x] = '\0';
                          }
                    }
                }
            }
        }
}







public class Föremål : Entitet
    {
     private string _namn =null;

    public string Namn 
    { 
        get { return _namn; }
        set { _namn = value; }
    }

        public Föremål(int x, int y, char symbol, string namn) : base(x, y, symbol)
        {
            Namn = namn;
        }
    }


public class Förmåga
{
    public string Namn { get; set; }
    public Action<Spelare> Använd { get; set; }

    public Förmåga(string namn, Action<Spelare> använd)
    {
        Namn = namn;
        Använd = använd;
    }


    
}
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

