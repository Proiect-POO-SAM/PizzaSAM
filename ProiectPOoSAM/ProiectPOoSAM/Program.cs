/* Participanti: Covaciu Sebastian-Adelin, Crisan Alex-Florin , Ignat Mihai-Alexandru */

using ProiectPOoSAM.Alex;

namespace ProiectPOoSAM;

public partial class Program : ProjectWrap
{
    public static void Main(string[] args)
    {
        // ASTEA 5 RAMAN ACI ! ♥

        ARC AddRemoveChange = new ARC();
        FileTXT file = new FileTXT();
        //file.deleteFile();

        file.initializeObjects(Constants.PIZZASLIST, Constants.USERLIST);

       // Console.WriteLine("Introdu date de test\n username: opel\n password: astra");
        DateTime today = DateTime.Today;
        TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);

        string initStamp = today.ToString() + " " + now.ToString();
        WriteIntoLogger(initStamp);

        var initResult = Project.INIT();

        if (initResult.user != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nBun venit, {initResult.user.GetUsername()}!");
            Console.ResetColor();
            bool ok = true;
            while (ok)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("--Welcome to PIZZERIA SAM !--");
                Console.WriteLine("Please choose one of the following");
                Console.WriteLine("1.MENU");
                Console.WriteLine("2.ORDER PIZZA");
                Console.WriteLine("3.EDIT ACCOUNT");
                Console.WriteLine("4.EXIT");
                Console.WriteLine(Environment.NewLine);
                int optiune = int.Parse(Console.ReadLine());
                switch (optiune)
                {
                    case 1:
                        foreach (var pizza in Constants.PIZZASLIST)
                        {
                            Console.WriteLine(pizza.ToString());
                        }

                        break;
                    case 2:
                        Console.WriteLine("Please choose a pizza from the menu");
                        foreach (var pizza in Constants.PIZZASLIST)
                        {
                            Console.WriteLine(pizza.ToString());
                        }

                        int pizzaChoice = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please choose the size of the pizza");
                        Console.WriteLine("1.Small");
                        Console.WriteLine("2.Medium");
                        Console.WriteLine("3.Large");
                        int sizeChoice = int.Parse(Console.ReadLine());
                        Console.WriteLine("Do you want to modify your pizza?"); // BIG WORK
                        Console.WriteLine("1.Yes");
                        Console.WriteLine("2.No");
                        int personalChoice = int.Parse(Console.ReadLine());
                        if (personalChoice == 0)
                        {
                        }


                        break;
                    case 3:
                        break;
                    case 4:
                        var userForcedUnloadResult = Project.UNLOAD();
                        WriteIntoLogger(userForcedUnloadResult.Message);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option");
                        break;
                }

            }
        }

        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Request failed.");
        }

        Console.ResetColor();

        WriteIntoLogger(initResult.Message);

        var unloadResult = Project.UNLOAD();
        WriteIntoLogger(unloadResult.Message);
        
    }
}


public class ProjectWrap
{
    public static void WriteIntoLogger(string message)
    {
        const string path = "Logger.txt";

        using (var Writer = File.AppendText(path))
        {
            Writer.WriteLine(message);
        }
    }
}

    



