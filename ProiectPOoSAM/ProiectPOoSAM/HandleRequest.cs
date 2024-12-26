using System.Diagnostics;
using Azure.Identity;
using ProiectPOOSAM;
using Twilio.TwiML.Messaging;

namespace ProiectPOoSAM;

public abstract class HandleRequest : Wrapper
{
    private string username;
    private string password;
    public bool passThrough = false;    // daca operatia a fost un succes --> true
    
    public string Handle_Login(string username, string password)
    {
        USER user = AllUsers.FirstOrDefault(u => u.GetUsername() == username && u.GetPassword() == password);
    
        if (user != null)
        {
            this.passThrough = true;
            return $"User {username} is logged in";
        }
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Username sau parola invalida.");
        Console.ResetColor();
        return "Invalid username or password. Login failed";
    }

    public string Handle_Register(string username, string password, string phone)
    {
        // Verificăm dacă username-ul există deja
        if (AllUsers.Any(u => u.GetUsername() == username))
        {
            return "Registration failed: Username already exists.";
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
            }
            else 
            {
                iloggerMessage = "Invalid Phone Number Format";
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Format numar de telefon invalid");
                return iloggerMessage;
            }
        }
        catch(Exception ex)
        {
            iloggerMessage = iloggerMessage + " " + ex.Message;
        }
        
        return iloggerMessage;
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
