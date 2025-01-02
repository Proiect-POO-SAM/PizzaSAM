using System.Diagnostics;
using Azure.Identity;
using ProiectPOOSAM;
using Twilio.TwiML.Messaging;

namespace ProiectPOoSAM;

public abstract class HandleRequest : Wrapper
{
    private string username;
    private string password;


    /// <summary>
    ///
    /// - mesajele pt logger sunt in engleza in mare parte.
    ///
    /// - functia Handle_Login -> primeste username si parola (stringuri)
    ///                        -> returneaza un tuplu (USER, string)
    ///                        -> daca USER == null inseama ca operatia a esuat
    ///
    /// - funcitia Handle_Register -> primeste username, parola si numarul de telefon (stringuri)
    ///                            -> returneaza un tuplu (USER, string)
    /// 
    /// </summary>
    /// name= SEBASTIAN.ADELIN
    
    
    // nu cred ca ii critic ca astea sa fie private
    public class RequestResult
    {
        public USER user { get; set; }
        public string Message { get; set; }
    }
    
    

    public static RequestResult Handle_Login(string username, string password)
    {
        try
        {
            if (AllUsers == null)
            {
                return new RequestResult { user = null, Message = "User list is not initialized." };
            }

            USER user = AllUsers.FirstOrDefault(u => u.GetUsername() == username && u.GetPassword() == password);
            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("V-ati loggat cu numele de utilizator ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{username}");

                if (user.GetRole() == "Admin")
                {
                    Console.Write(" cu drepturi de administrator.");
                }
                
                Console.ResetColor();
                return new RequestResult { user = user, Message = $"User {username} is logged in" };
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Loggare esuata. Username/Parola incorecta!");
            Console.ResetColor();
            return new RequestResult { user = null, Message = "Invalid username or password. Login failed" };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Loggare esuata. Eroare: " + ex.Message);
            Console.ResetColor();
            return new RequestResult { user = null, Message = $"Error: {ex.Message}" };
        }
    }
    

    public static RequestResult Handle_Register(string username, string password, string phone)
    {
        // Verificăm dacă username-ul există deja
        if (AllUsers.Any(u => u.GetUsername() == username))
        {
            Console.WriteLine("Username deja existent!");
            return new RequestResult { user = null, Message = "Registration failed: Username already exists." };
        }
        
        string iloggerMessage = "";
        try
        {    
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(phone))
            {
                //af msg consola
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Campurile username si numar de telefon sunt obligatorii!");
                Console.ResetColor();
                
                // ilogger
                return new RequestResult { user = null, Message = "Username or phone cannot be empty." };
            }

            if(PhoneFORMAT(phone))
            {
                USER user = new USER(username, password, phone,USER.Role.Client);
                AllUsers.Add(user);
                
                iloggerMessage = "User " + username + " is registered successfully";
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Clientul {username} este intregistrat.");
                Console.ResetColor();
                return new RequestResult { user = user, Message = iloggerMessage };
            }
            
            iloggerMessage = "Invalid Phone Number Format";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Formatul numarului de telefon este invalid");
            return new RequestResult { user = null, Message = iloggerMessage };
        }
        catch(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Eroare in cadrul inregistrarii in aplicatie: " + ex.Message);
            Console.ResetColor();
            return new RequestResult { user = null, Message = iloggerMessage + "\nERROR: " +ex.Message };
        }
    }


    // funtii utile
    public static bool PhoneFORMAT(string phone)
    {
        if (phone.Length == 12)
        {
            if (phone.StartsWith("+40"))
            {
                if (phone.Substring(3, 1).All(char.IsDigit))
                {
                    return true;
                }
            }
        }
        else if(phone.Length == 10 && phone.StartsWith("07") && phone.All(char.IsDigit))
        {
            return true;
        }

        return false;
    }
}
