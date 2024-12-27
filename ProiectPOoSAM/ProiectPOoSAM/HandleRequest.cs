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
    ///                        -> returneaza un tuplu (bool, string)
    ///
    /// - funcitia Handle_Register -> primeste username, parola si numarul de telefon (stringuri)
    ///                            -> returneaza un tuplu (bool, string)
    /// 
    /// </summary>
    /// name= SEBASTIAN.ADELIN
    
    
    
    // nu cred ca ii critic ca astea sa fie private
    public class RequestResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
    
    

    public RequestResult Handle_Login(string username, string password)
    {
        try
        {
            if (AllUsers == null)
            {
                return new RequestResult { IsSuccessful = false, Message = "User list is not initialized." };
            }

            USER user = AllUsers.FirstOrDefault(u => u.GetUsername() == username && u.GetPassword() == password);
            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("V-ati loggat cu numele de utilizator ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{username}.");
                Console.ResetColor();
                return new RequestResult { IsSuccessful = true, Message = $"User {username} is logged in" };
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Loggare esuata. Username/Parola incorecta!");
            Console.ResetColor();
            return new RequestResult { IsSuccessful = false, Message = "Invalid username or password. Login failed" };
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Loggare esuata. Eroare: " + ex.Message);
            Console.ResetColor();
            return new RequestResult { IsSuccessful = false, Message = $"Error: {ex.Message}" };
        }
    }
    

    public RequestResult Handle_Register(string username, string password, string phone)
    {
        // Verificăm dacă username-ul există deja
        if (AllUsers.Any(u => u.GetUsername() == username))
        {
            return new RequestResult { IsSuccessful = false, Message = "Registration failed: Username already exists." };
        }
        
        string iloggerMessage = "";
        try
        {            
            if(PhoneFORMAT(phone))
            {
                USER user = new USER(username, password, phone,USER.Role.Client);
                AllUsers.Add(user);
                
                iloggerMessage = "User " + username + " is registered successfully";
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Clientul {username} este intregistrat.");
                Console.ResetColor();
                return new RequestResult { IsSuccessful = true, Message = iloggerMessage };
            }
            else 
            {
                iloggerMessage = "Invalid Phone Number Format";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Format numar de telefon invalid");
                return new RequestResult { IsSuccessful = false, Message = iloggerMessage };
            }
        }
        catch(Exception ex)
        {
            iloggerMessage = iloggerMessage + " " + ex.Message;
        }
        
        return new RequestResult { IsSuccessful = false, Message = iloggerMessage };
    }


    // funtii utile
    public bool PhoneFORMAT(string phone)
    {
        if (phone.Length == 11)
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
