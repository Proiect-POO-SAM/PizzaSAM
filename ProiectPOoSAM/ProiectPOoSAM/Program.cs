/* Participanti: Covaciu Sebastian-Adelin, Crisan Alex-Florin , Ignat Mihai-Alexandru */
using ProiectPOoSAM.Alex;
using ProiectPOOSAM;
using System.ComponentModel;
using System.Reflection;
using ProiectPOoSAM;
using ProiectPOoSAM.Mihai;


namespace ProiectPOoSAM;
public partial class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Introdu date de test\n username: opel\n password: astra");

        var initResult = Project.INIT();

        if (initResult.user != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($". Bun venit, {initResult.user.GetUsername()}!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Request failed.");
        }
        Console.ResetColor();


        Project.UNLOAD();
    }
}