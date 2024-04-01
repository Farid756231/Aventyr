using System;
namespace labration;

public class Game
{
    static Spelare spelaren = null!; // Deklarera en statisk instans av spelaren
    public void Start()
    {
        // Skapa en tvådimensionell array för spelplanen
        char[,] spelplan = new char[20, 20];
        // Skapa en ny spelare och placera den på spelplanen
        spelaren = new Spelare(1, 1, '@');
        PlaceraEntitet(spelplan, spelaren);
        
        
        // Skapa och placera slumpmässigt varelser, hälsopaket och föremål på spelplanen
        Varelse monster1 = new Varelse(0, 0, 'M', ConsoleColor.Red);
        monster1.LäggTillFörmåga(new Förmåga("Sparka", s => { s.Livskraft -= 25; }));
        PlaceraSlumpmässigtEntitet(spelplan, monster1);

        Varelse monster2 = new Varelse(0, 0, 'Y', ConsoleColor.Blue);
        monster2.LäggTillFörmåga(new Förmåga("Sparka", s => { s.Livskraft -= 25; }));
        PlaceraSlumpmässigtEntitet(spelplan, monster2);

        Varelse monster3 = new Varelse(0, 0, 'S', ConsoleColor.DarkBlue);
        monster3.LäggTillFörmåga(new Förmåga("Sparka", s => { s.Livskraft -= 25; }));
        PlaceraSlumpmässigtEntitet(spelplan, monster3);

        Hälsopaket hälsopaket1 = new Hälsopaket(0, 0, '+', ConsoleColor.Blue);
        PlaceraSlumpmässigtEntitet(spelplan, hälsopaket1);

        Hälsopaket hälsopaket2 = new Hälsopaket(0, 0, '+', ConsoleColor.Blue);
        PlaceraSlumpmässigtEntitet(spelplan, hälsopaket2);

        Hälsopaket hälsopaket3 = new Hälsopaket(0, 0, '+', ConsoleColor.Blue);
        PlaceraSlumpmässigtEntitet(spelplan, hälsopaket3);

        Föremål hälsodryck = new Föremål(0, 0, 'H', "Hälsodryck");
        PlaceraSlumpmässigtEntitet(spelplan, hälsodryck);

        Föremål rustning = new Föremål(0, 0, 'R', "Rustning");
        PlaceraSlumpmässigtEntitet(spelplan, rustning);

        while (true)
        {
             // Rita spelplanen med spelaren och varelserna
            RitaSpelplan(spelplan, spelaren, new List<Varelse> { monster1, monster2, monster3 });
             // Läs in tangenten som spelaren trycker på
            ConsoleKeyInfo keyInfo = Console.ReadKey();
             // Hantera spelarens drag baserat på inmatad tangent
            HanteraSpelarensDrag(spelplan, keyInfo);
             // Loopa igenom alla entiteter på spelplanen och hantera möten med spelaren
            foreach (Entitet entitet in new List<Entitet> { monster1, monster2, monster3, hälsopaket1, hälsopaket2, hälsopaket3, hälsodryck, rustning })
            {
                // Kolla om spelaren möter en entitet
                if (spelaren.X == entitet.X && spelaren.Y == entitet.Y)
                {
                    if (entitet is Varelse)
                    {
                        Varelse varelse = (Varelse)entitet;
                        // Kontrollera om varelsen fortfarande är vid liv
                        if (varelse.Livskraft > 0)
                        {
                            MötVarelse(varelse);

                            // Uppdatera positionen för varelsen till en ny slumpmässig position
                            PlaceraSlumpmässigtEntitet(spelplan, varelse);
                        }
                        else
                        {
                            Console.WriteLine("Varelsen är redan besegrad och utgör inget hot längre.");
                        }
                    }
                    else if (entitet is Hälsopaket)
                    {
                        MötHälsopaket((Hälsopaket)entitet);

                        // Uppdatera positionen för hälsopaketet till en ny slumpmässig position
                        PlaceraSlumpmässigtEntitet(spelplan, (Hälsopaket)entitet);
                    }
                    else if (entitet is Föremål)
                    {
                        MötFöremål((Föremål)entitet, spelplan);

                        // Uppdatera positionen för föremålet till en ny slumpmässig position
                        PlaceraSlumpmässigtEntitet(spelplan, (Föremål)entitet);
                    }

                    RitaSpelplan(spelplan, spelaren, new List<Varelse> { monster1, monster2, monster3 });
                }
            }
             
              // Kontrollera om spelaren har besegrat alla varelser
            if (monster1.Livskraft <= 0 && monster2.Livskraft <= 0 && monster3.Livskraft <= 0)
            {
                Console.WriteLine("Grattis! Du har besegrat alla varelser och vunnit spelet!");
                break;
            }
               // Kontrollera om spelaren har förlorat alla sina livspoäng
            if (spelaren.Livskraft <= 0)
            {
                Console.WriteLine("Du förlorade! Spelet är över.");
                break;
            }

            Console.Clear();
        }

    }
      
      // Metod för att placera en entitet slumpmässigt på spelplanen
    public virtual void PlaceraSlumpmässigtEntitet(char[,] spelplan, Entitet entitet)
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
    public static void RitaSpelplan(char[,] spelplan, Spelare spelaren, List<Varelse> monsters)
    {

        Console.Clear();

        int rows = spelplan.GetLength(0);
        int cols = spelplan.GetLength(1);

        // Calculate the maximum symbol width
        int maxSymbolWidth = 5; // Default width for symbols without special padding
        foreach (char symbol in new char[] { '@', 'M', '+', 'H', 'R' })
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
                // ConsoleColor color = ConsoleColor.Gray;

                switch (symbol)
                {
                    case '\0':
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("".PadRight(maxSymbolWidth));
                        break;
                    case '@':
                        Console.Write("\U0001F645".PadRight(maxSymbolWidth));
                        break;
                    case 'M':
                        Console.Write("\U0001F47B".PadRight(maxSymbolWidth));
                        break;
                    case 'Y':
                        Console.Write("\U0001F480".PadRight(maxSymbolWidth));
                        break;
                    case 'S':
                        Console.Write("\U0001F40D".PadRight(maxSymbolWidth));
                        break;
                    case '+':
                        Console.Write("\U0001F3E5".PadRight(maxSymbolWidth));
                        break;
                    case 'H':
                        Console.Write("\U0001F691".PadRight(maxSymbolWidth));
                        break;
                    case 'R':
                        Console.Write("\U0001F300".PadRight(maxSymbolWidth));
                        break;

                }
            }
            Console.WriteLine("                    |"); // Draw right border
        }

        // Draw bottom border
        Console.WriteLine("+" + new string('-', maxRowWidth) + "+");

        // Center-align health message
        Console.WriteLine($"{"Din hälsa: " + spelaren.Livskraft + " % : \U0001F49A"}");

        Console.WriteLine("Spelarens väska: \U0001F392");
        foreach (var item in spelaren.Väska)
        {

            string emoji = GetEmojiForItem(item); // Get emoji for the item
            Console.WriteLine($"{item.Namn}: {emoji} ");

        }

        Console.WriteLine(" \n  ");

        foreach (var monster in monsters)
        {
            Console.WriteLine($"Monster Hälsa ({monster.Symbol}): {monster.Livskraft}% : \U0001F49B");
        }

    }

    private static string GetEmojiForItem(Föremål föremål)
    {
        switch (föremål.Symbol)
        {
            case 'H': return "\U0001F691"; // Hälsodryck emoji
            case 'R': return "\U0001F300"; // Rustning emoji
            default: return föremål.Symbol.ToString(); // Use the item symbol as is if no emoji found
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
        Console.WriteLine($"Varelsen har {varelse.Livskraft}% hälsa.");

        // Kontrollera om spelaren har några föremål i väskan
        if (spelaren.Väska.Any())
        {
            Console.WriteLine("Du har föremål i din väska som du kan använda för att bekämpa varelsen.");
            Console.WriteLine("Tryck 'A' för att använda ett föremål eller valfri annan tangent för att fortsätta striden.");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.KeyChar == 'A' || keyInfo.KeyChar == 'a')
            {
                // Visa spelaren vilka föremål de kan välja från
                Console.WriteLine("Välj ett föremål att använda:");
                for (int i = 0; i < spelaren.Väska.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {spelaren.Väska[i].Namn}");
                }

                // Låt spelaren välja ett föremål att använda
                keyInfo = Console.ReadKey();
                int index = keyInfo.KeyChar - '1';
                if (index >= 0 && index < spelaren.Väska.Count)
                {
                    // Använd det valda föremålet
                    spelaren.AnvändFöremål(spelaren.Väska[index]);
                    // Avsluta MötVarelse-metoden eftersom spelaren använde ett föremål
                    return;
                }
                else
                {
                    Console.WriteLine("Ogiltigt val. Striden fortsätter.");
                }
            }
        }


        // Exempel på standardattack
        int skadaMotVarelse = 20; // Anta att standardattacken orsakar 20 skada
        varelse.Livskraft -= skadaMotVarelse;
        if (varelse.Livskraft < 0)
        {
            varelse.Livskraft = 0;
        }
        Console.WriteLine($"Du attackerar varelsen och orsakar {skadaMotVarelse} skada.");

        // Exempel på varelsens attack
        int skadaMotSpelare = 15; // Anta att varelsens attack orsakar 10 skada
        spelaren.Livskraft -= skadaMotSpelare;

        if (spelaren.Livskraft < 0)
        {
            spelaren.Livskraft = 0;
        }


        Console.WriteLine($"Varelsen attackerar dig och orsakar {skadaMotSpelare} skada.");

        // Kontrollera om spelaren eller varelsen har blivit besegrad
        if (spelaren.Livskraft <= 0)
        {
            Console.WriteLine("Du har förlorat! Spelet är över.");
            return;
        }

        if (varelse.Livskraft <= 0)
        {
            Console.WriteLine("Varelsen är besegrad! Du har vunnit striden.");
            return;
        }
    }


       // Metod för att hantera mötet med ett hälsopaket
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
        if ((keyInfo.KeyChar == 'P' || keyInfo.KeyChar == 'p') && !spelaren.Väska.Contains(föremål))
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
            Console.WriteLine("Föremålet har lagts till i din väska.");

            // Fråga spelaren om de vill använda föremålet omedelbart
            Console.WriteLine("Vill du använda föremålet nu? Tryck 'J' för ja och 'N' för nej.");
            keyInfo = Console.ReadKey();
            if (keyInfo.KeyChar == 'J' || keyInfo.KeyChar == 'j')
            {
                spelaren.AnvändFöremål(föremål);
                // Uppdatera spelarens status efter att ha använt föremålet
                Console.WriteLine($"Din hälsa: {spelaren.Livskraft}%");
            }
        }
    }




}
