using Microsoft.VisualBasic;
using ProiectPOOSAM;

namespace ProiectPOoSAM;

public partial class Project : Constants
{
    protected List<string> oldLogs = new List<string>();
    
    
    /// <summary>
    ///
    ///  functia INIT() -> porneste proiectul -> citeste toate datele din fisiere
    ///                 -> returneaza un tip de data RequestResult (USER,string) ; daca user e null inseamna ca a esuat login / register
    ///  functia UNLOAD() -> inchide proiectul -> pretty much the opposite
    /// 
    /// 
    /// </summary>
    /// name: SEBASTIAN.ADELIN
    
    
    
    
    
    
    public static HandleRequest.RequestResult INIT()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Se initializeaza programul ...");
        Console.ResetColor();
        
        //-----------------------------------

        try
        {
            USER.LoadUsers();
            USER.show();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Initializare finalizata."); 
        Console.ResetColor();
        
        
        //------------------------------------
        
        
        USER user = null;
        string Message = "";

        Message += "\nINIT-PROJECT";
        try
        {
            Console.Write("\n      PIZZA ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("S");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("A");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("M");
            Console.ResetColor();
            
            
            Console.WriteLine("\n----------------------");
            Console.WriteLine(" Log-in  ||  Register ");
            Console.WriteLine(" ------  ||   ------ ");
            Console.WriteLine("  Menu   ||    Quit ");
            Console.WriteLine("----------------------");
            
            Console.Write("->");
            string option = Console.ReadLine();
            
            // in felul asta poti scrie fie cum si programul va recunoaste decizia   EX:  rEgIsTer --> REGISTER
            option = option.ToUpper();

            USER conditie_user = null;
            while (conditie_user == null)
            {
                switch (option)
            {
                case "REGISTER":
                    Console.Write("username: ");
                    string username_register = Console.ReadLine();
                    Console.Write("password: ");
                    string password_register = Console.ReadLine();
                    Console.Write("Telefon: ");
                    string phone = Console.ReadLine();
                    
                    // register
                    HandleRequest.RequestResult retrunResultRegister = HandleRequest.Handle_Register(username_register, password_register,phone);
                    conditie_user = retrunResultRegister.user;
                    return retrunResultRegister;
                    
                case "LOGIN":
                    Console.Write("username: ");
                    string username = Console.ReadLine();
                    Console.Write("password: ");
                    string password = Console.ReadLine();
                    
                    // login
                    HandleRequest.RequestResult retrunResultLogin = HandleRequest.Handle_Login(username, password);
                    conditie_user = retrunResultLogin.user;
                    return retrunResultLogin;
                
                case "LOG-IN":
                    Console.Write("username: ");
                    string username2 = Console.ReadLine();
                    Console.Write("password: ");
                    string password2 = Console.ReadLine();
                    
                    // login
                    HandleRequest.RequestResult retrunResultLogin2 = HandleRequest.Handle_Login(username2, password2);
                    conditie_user = retrunResultLogin2.user;
                    return retrunResultLogin2;
                    
                
                case "MENIU":
                    break;
                
                case "QUIT":
                    Environment.Exit(0);
                    break;
                
                default:
                    Console.WriteLine("Optiune invalida.");
                    Console.Write("-> ");
                    option = Console.ReadLine();
                    break;
            }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Message = Message + " " + e.Message;
            Console.WriteLine("\n" + Message);
        }
        
        Console.ResetColor();
        return new HandleRequest.RequestResult { user = user, Message = Message };
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
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Se inchide programul ...");
        Console.ResetColor();
    }
}