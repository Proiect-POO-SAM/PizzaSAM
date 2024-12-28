using Microsoft.VisualBasic;
using ProiectPOOSAM;

namespace ProiectPOoSAM;

public partial class Project : Constants
{
    protected List<string> oldLogs = new List<string>();
    
    public class retrunBack
    {
        public USER user_back { get; set; }
        public string Message { get; set; }
    } 
    
    public static retrunBack INIT()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Se initializeaza programul ...");
        Console.ResetColor();
        USER user = null;
        string Message = "";

        Message += "\nINIT-PROJECT";
        try
        {
            Console.Write("PIZZA "); 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("S");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("A");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("M");
            Console.ResetColor();
            
            
            Console.WriteLine("\n----------------------");
            Console.WriteLine(" Log-in  ||  Register ");
            Console.WriteLine("----------------------");
            
            Console.WriteLine("->");
            string option = Console.ReadLine();
            
            // in felul asta poti scrie fie cum si programul va recunoaste decizia   EX:  rEgIsTer --> REGISTER
            option = option.ToUpper();

            if (option == "REGISTER")
            {
                Console.Write("username: ");
                string username = Console.ReadLine();
                Console.Write("password: ");
                string password = Console.ReadLine();
                
                retrunBack retrunBack = new retrunBack();
                HandleRequest.Handle_Login(username, password);
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Message = Message + " " + e.Message;
            Console.WriteLine("\n" + Message);
        }
        
        
        
        
        
        Console.WriteLine("Initializare finalizata.");
        Console.ResetColor();
        
        return new retrunBack { user_back = user, Message = Message };
    }


    public static void UNLOAD()
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
}