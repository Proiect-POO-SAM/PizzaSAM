using ProiectPOOSAM;
using ProiectPOoSAM.Alex;

namespace ProiectPOOSAM
{
    public class USER : Wrapper
    {
        protected string username;
        protected string password;
        private string phoneNumber;
        protected Role role;
        protected bool accesToken;
        private bool fidelityCard;

        Dictionary<string, string> AllUsers = new Dictionary<string, string>();
        List<Orders> listOrders;

        public enum Role  // <-- mai mult pt a eticheta 
        {
            Admin,
            Client
        }
        
        public USER(string username, string password, string phone, Role role)
        {
            this.username = username;
            this.password = password;
            this.role = Role.Client;
            this.phoneNumber = phone;
            this.accesToken = role == Role.Admin ? true : false;
            this.listOrders = null;
            this.fidelityCard = false;
        }
        public USER(string username, string password, string phone, Role role, bool fidelityCard)
        {
            this.username = username;
            this.password = password;
            this.role = Role.Client;
            this.phoneNumber = phone;
            this.accesToken = role == Role.Admin ? true : false;
            this.listOrders = null;
            this.fidelityCard = fidelityCard;
        }


        public string GetUsername() => username;
        public string GetPassword() => password;
        public string GetPhoneNumber() => phoneNumber;
        public string GetRole() => role.ToString();
        public bool AccessVerification() => accesToken;

        
        public int GetOrdersCount() => listOrders==null ? 0 : listOrders.Count;
        
        // vf valabilitatea unui username (pt functia register)
        public bool Exists(string username)
        {
            return AllUsers.ContainsKey(username);
        }

        public bool GetFidelityCard()
        {
            return fidelityCard;
        }

        public bool SetFidelityCard(bool FidelityCard)
        {
            this.fidelityCard = FidelityCard;
            return FidelityCard;
        }




        // schimbarea parolei unui utilizator de catre admin

        public void DeleteAccount(string username, string parola)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            AllUsers.Remove(username);
            Console.WriteLine("Process Finalized.");
            Console.ResetColor();
        }

        public string SaveFormat()
        {
            return GetUsername() + "," + GetPassword() + "," + GetPhoneNumber() + "," + GetRole() ;
        }
    }
}


public abstract class Wrapper
{ 
    private static readonly string SourceFile = "AllUsers.txt";
    protected static List<USER> AllUsers { get; set; } = new List<USER>();

    public static bool IsPhoneNumber(string input)
    {
        return input.StartsWith("+40") && input.Length == 12 && input.Substring(3).All(char.IsDigit);
    }
    
    public static string LoadUsers()
    {
        try
        {
            string additionalMessage = " with addition:";
            
            using (StreamReader Read = new StreamReader(SourceFile))
            {
                string line;
                while ((line = Read.ReadLine()) != null)
                {
                    string[] lines = line.Split(',');

                    if(lines.Length != 4)
                    {
                        additionalMessage += $" Skipped Line '{line}' for wrong format //";
                        continue;
                    }
                    
                    if(string.IsNullOrWhiteSpace(lines[0]) || string.IsNullOrWhiteSpace(lines[1]) || string.IsNullOrWhiteSpace(lines[2]))
                    {
                        additionalMessage += $" Skipped Line '{line}' for missing data //";
                        continue;
                    }
                    
                    if (IsPhoneNumber(lines[2]) == true)
                        continue;
                    else
                    {
                        additionalMessage += $" Skipped Line '{line}' for unvalid phone number //";
                    }
                    
                    if(!Enum.TryParse(lines[3], out USER.Role role))
                    {
                        additionalMessage += $" Skipped Line '{line}' for invalid role //";
                        continue;
                    }
                    try
                    {
                        USER user = new USER(lines[0], lines[1], lines[2], role);
                        AllUsers.Add(user);
                    }
                    catch (Exception ex)
                    {
                        additionalMessage += $" Failed to create user for line '{line}': {ex.Message} //";
                    }
                }
            }
            
            if(additionalMessage != " with addition:")
            {
                return $"Users Loaded to Interface ({AllUsers.Count} users loaded)" + additionalMessage;
            }
            else
            {
                return $"Users Loaded to Interface ({AllUsers.Count} users loaded)";
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.WriteLine();
            return e.Message;
        }
    }
    public string SaveUsers()
    {
        try
        {
            using (StreamWriter Write = new StreamWriter(SourceFile))
            {
                foreach (USER X in AllUsers)
                {
                    Write.WriteLine(X.SaveFormat());
                }

                Console.WriteLine("Data Saved");
                return "Users Saved to file";
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.WriteLine();
            return e.Message;                                   // dau return la messaje pt a putea implemente ILogger mai trz
        }
    }


    public static void show()
    {
        foreach (USER X in AllUsers)
        {
            Console.WriteLine(X.SaveFormat());
        }
    }
}