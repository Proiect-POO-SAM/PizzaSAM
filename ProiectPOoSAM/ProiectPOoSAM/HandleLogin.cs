using System.Diagnostics;
using Azure.Identity;
using ProiectPOOSAM;

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
        
        USER user = new USER(username, password, phone,USER.Role.Client);
        AllUsers.Add(user);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Clientul {username} este intregistrat.");
        Console.ResetColor();
        return "Registration successful.";
    }
}
