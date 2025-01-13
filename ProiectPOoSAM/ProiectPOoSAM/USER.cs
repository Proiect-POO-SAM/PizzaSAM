using ProiectPOOSAM;
using ProiectPOoSAM.Alex;
using Microsoft.VisualBasic;
using ProiectPOoSAM;
using Constants = ProiectPOoSAM.Constants;
namespace ProiectPOOSAM
{
    public class USER
    {
        protected string username;
        protected string password;
        private string phoneNumber;
        protected Role role;
        protected bool accesToken;
        private bool fidelityCard;
        private string salt;

        private List<Orders> listOrders = new List<Orders>();
        public enum Role  // <-- mai mult pt a eticheta 
        {
            Client,   // (prima e default pt Enum)
            Admin
        }
        
        public USER(string username, string password, string phone, Role role)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            this.phoneNumber = phone;
            this.accesToken = role == Role.Admin ? true : false;
            this.listOrders = null;
            this.fidelityCard = false;
        }
        public USER(string username, string password, string phone, Role role, string salt)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            this.phoneNumber = phone;
            this.accesToken = role == Role.Admin ? true : false;
            this.listOrders = null;
            this.fidelityCard = false;
            this.salt = salt;
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

        public USER(string username, string password, string phoneNumber, Role role, bool accesToken, bool fidelityCard, string salt)
        {
            this.username = username;
            this.password = password;
            this.phoneNumber = phoneNumber;
            this.role = role;
            this.accesToken = accesToken;
            this.fidelityCard = fidelityCard;
            this.salt = salt;
        }

        public USER(string username, string password, string phoneNumber, Role role, bool accesToken, bool fidelityCard, string salt, List<Orders> orders)
        {
            this.username = username;
            this.password = password;
            this.phoneNumber = phoneNumber;
            this.role = role;
            this.accesToken = accesToken;
            this.fidelityCard = fidelityCard;
            this.salt = salt;
            this.listOrders = orders;
        }

        public string GetUsername() => username;
        public string GetPassword() => password;
        public string GetPhoneNumber() => phoneNumber;
        public string GetRole() => role.ToString();
        public bool AccessVerification() => accesToken;



        public int GetOrdersCount() => listOrders==null ? 0 : listOrders.Count;
        
        // vf valabilitatea unui username (pt functia register)
        public bool GetFidelityCard()
        {
            return fidelityCard;
        }
        public string GetSalt() => salt;


        public List<Orders> GetOrders()
        {
            return listOrders;
        }




        public bool SetFidelityCard(bool FidelityCard)
        {
            this.fidelityCard = FidelityCard;
            return FidelityCard;
        }


        public void addOrder(Orders order)
        {
            if (listOrders == null)
            {
                listOrders = new List<Orders>();
            }
            else
            {
                listOrders.Add(order);
            }

        }

public void viewOrders()
{
    if (Constants.ORDERSLIST == null || Constants.ORDERSLIST.Count == 0)
    {
        Console.WriteLine("No orders found.");
        return;
    }

    var userOrders = Constants.ORDERSLIST
        .Where(o => o.getUsername() == this.GetUsername())
        .OrderByDescending(o => o.getDate())
        .ToList();

    if (!userOrders.Any())
    {
        Console.WriteLine("You have no orders.");
        return;
    }

    Console.WriteLine("\n=== Your Orders ===");
    foreach (var order in userOrders)
    {
        Console.WriteLine(order.ToString());
    }
}



        public string SaveFormat()
        {
            return GetUsername() + "," + GetPassword() + "," + GetPhoneNumber() + "," + GetRole().ToString() + "," + GetSalt();
        }

        public void showUserDetails()
        {
            Console.WriteLine("Username: " + GetUsername());
            Console.WriteLine("Phone Number: " + GetPhoneNumber());
            Console.WriteLine("Role: " + GetRole());
            Console.WriteLine("Fidelity Card: " + GetFidelityCard());
            Console.WriteLine("Orders: " + GetOrdersCount() + " orders");
            Console.WriteLine(Environment.NewLine);
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

    public List<USER> GetAllUsers()
    {
        return AllUsers;
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

                    if(lines.Length != 5)
                    {
                        additionalMessage += $" Skipped Line '{line}' for wrong format //";
                        continue;
                    }
                    
                    if(string.IsNullOrWhiteSpace(lines[0]) || string.IsNullOrWhiteSpace(lines[1]) || string.IsNullOrWhiteSpace(lines[2]))
                    {
                        additionalMessage += $" Skipped Line '{line}' for missing data //";
                        continue;
                    }
                    
                    if (!IsPhoneNumber(lines[2]))
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
                        // Găsește utilizatorul în Constants.USERLIST după username
                        var existingUser = Constants.USERLIST.FirstOrDefault(user => user.GetUsername() == lines[0]);

                        if (existingUser != null)
                        {
                            // Dacă utilizatorul există, doar adăugăm mesajul de skip
                            additionalMessage += $" Skipped Line '{line}' for duplicate username //";
                            continue; // Trecem la următoarea linie
                        }
                        else
                        {
                            // Dacă utilizatorul nu există, creează unul nou și îl adaugă
                            USER userAdd = new USER(lines[0], lines[1], lines[2], role, lines[4]);
                            Constants.USERLIST.Add(userAdd);
                        }


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
    public static string SaveUsers()
    {
        FileTXT file = new FileTXT();
        try
        {
            using (StreamWriter Write = new StreamWriter(SourceFile))
            {
                foreach (USER X in AllUsers)
                {
                    Write.WriteLine(X.SaveFormat());
                }
                Console.WriteLine("Data Saved");
                foreach (USER user in AllUsers)
                    file.addCommandToFile(file.breakToPiecesUser(user));
                return "Users Saved to file";
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error while saving users: " + e.Message);
            Console.WriteLine(e.StackTrace);
            Console.ResetColor();
            return "Error: " + e.Message;
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