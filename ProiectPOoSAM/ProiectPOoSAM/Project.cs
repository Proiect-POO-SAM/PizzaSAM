using Microsoft.VisualBasic;
using ProiectPOOSAM;

namespace ProiectPOoSAM;

public class Project : Constants, ILogger
{
    protected List<string> oldLogs = new List<string>();
    
    
    public static string INIT()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Se initializeaza programul ...");
        
        //partea de admin
        //USER admin = new USER(adminUsername, adminPassword, "+40700000000", USER.Role.Admin);
        
            
        //partea de citire Meniu
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var elements = line.Split(',');
                    // incompleta pana se creaza meniul
                }
            }

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Programul se inchide ...");
            return "Failed reading file";
            Console.ResetColor();
        }


        string logger = "INIT-PROJECT";
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            logger = logger + " " + e.Message;
        }
        
        
        
        
        
        Console.WriteLine("Initializare finalizata.");
        Console.ResetColor();
        
        return logger;
    }


    public static void UNLOAD() // <- obiect de tipul meniu
    {
        Console.ForegroundColor = ConsoleColor.Green;
            
            
        // salvare in fisier
        try
        {
            using (StreamWriter saver = new StreamWriter(filePath))
            {
                saver.WriteLine(" ");                         // <- Menu not implemented 
            }

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
            
            
        Console.WriteLine("Se inchide programul ...");
    }

    public void AddToLogger(List<string> oldLogs, string message)
    {
        oldLogs.Add(message);
    }

    public void WriteLogger()
    {
        throw new NotImplementedException();
    }
}