/* Participanti: Covaciu Sebastian-Adelin, Crisan Alex-Florin , Ignat Mihai-Alexandru */

using Azure;
using ProiectPOoSAM.Alex;

namespace ProiectPOoSAM;

public partial class Program : ProjectWrap
{
    public static void Main(string[] args)
    {
        // ASTEA 5 RAMAN ACI ! ♥

        ARC AddRemoveChange = new ARC();
        FileTXT file = new FileTXT();
        file.initializeObjects(Constants.PIZZASLIST, Constants.USERLIST);
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
                Console.WriteLine("2.EXIT");
                Console.WriteLine(Environment.NewLine);
                if (!int.TryParse(Console.ReadLine(), out int optiune))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a valid number!");
                    Console.ResetColor();
                    continue;
                }
                switch (optiune)
                {
                    case 1: // Menu
                        if (initResult.user.GetRole() == "Admin")
                        {
                            Project.ShowAdminMenu(initResult.user);
                        }
                        else
                        {
                            Project.ShowClientMenu(initResult.user);
                        }
                        break;
                    case 2: // Exit
                        file.deleteFile();
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
        file.deleteFile();
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





