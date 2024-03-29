using System;
namespace labration;


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