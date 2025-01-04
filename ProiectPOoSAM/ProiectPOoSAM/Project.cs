using ProiectPOOSAM;

namespace ProiectPOoSAM;

public partial class Project 
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
        
        //asta trebe schimbat cumva ca sa mearga la logger
        string message_ilogger = USER.LoadUsers();
        
        
        //-----------------------------------
        // asta e pt citire meniu
        try
        {
            
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

            USER this_user = null;
            bool blockPurposeCompleted = false;
            while (this_user == null && blockPurposeCompleted == false)
            {
                Console.Write("->");
                string option = Console.ReadLine();
            
                // in felul asta poti scrie fie cum si programul va recunoaste decizia   EX:  rEgIsTer --> REGISTER
                option = option.ToUpper();
                
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
                    this_user = retrunResultRegister.user;
                    blockPurposeCompleted = true;
                    return retrunResultRegister;
                    
                case "LOGIN":
                    Console.Write("username: ");
                    string username = Console.ReadLine();
                    Console.Write("password: ");
                    string password = Console.ReadLine();
                    
                    // login
                    HandleRequest.RequestResult retrunResultLogin = HandleRequest.Handle_Login(username, password);
                    this_user = retrunResultLogin.user;
                    blockPurposeCompleted = true;
                    return retrunResultLogin;
                
                case "LOG-IN":
                    Console.Write("username: ");
                    string username2 = Console.ReadLine();
                    Console.Write("password: ");
                    string password2 = Console.ReadLine();
                    
                    // login
                    HandleRequest.RequestResult retrunResultLogin2 = HandleRequest.Handle_Login(username2, password2);
                    this_user = retrunResultLogin2.user;
                    blockPurposeCompleted = true;
                    return retrunResultLogin2;
                    
                
                case "MENIU":
                    
                    break;
                
                case "QUIT":
                    Environment.Exit(0);
                    break;
                
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Optiune invalida.");
                    Console.ResetColor();
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


    public static HandleRequest.RequestResult UNLOAD()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        string message = USER.SaveUsers();
        
        // salvare in fisier
        try
        {
            using (StreamWriter saver = new StreamWriter(Constants.filePath))
            {
                saver.WriteLine(" ");                         // <- Menu not implemented 
            }

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            string log = "Error: " + ex.Message;
            Console.ResetColor();
            return new HandleRequest.RequestResult { user = null, Message = log };
        }
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Se inchide programul ...");
        Console.ResetColor();
        return new HandleRequest.RequestResult { user = null, Message = "Unload succesful." };
    }
}