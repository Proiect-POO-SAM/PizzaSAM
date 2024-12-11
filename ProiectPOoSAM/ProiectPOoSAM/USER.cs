namespace ProiectPOOSAM;

public class USER
{
    protected string username;
    protected string password;
    protected Role role;


    public enum Role
    {
        Admin,
        Client,
        User
    }
    

    public USER(string username, string password, Role role)
    {
        this.username = username;
        this.password = password;
        this.role = Role.Client;
    }

    public string GetUsername() => username;
    public string GetRole() => role.ToString();
    
    
    // schimbarea parolei unui utilizator de catre admin
    public void SetPassword(USER user, string parolaNoua, Role role)
    {
        if(this.role == Role.Admin)
        {
            user.password = parolaNoua;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Parola pentru utilizatorul {user.GetUsername()} a fost schimbata.");
            Console.ResetColor();
        }
        else
            Console.WriteLine("Nu aveti drepturi de administrator");
    }
}