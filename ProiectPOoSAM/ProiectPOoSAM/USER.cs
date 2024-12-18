using ProiectPOoSAM.Alex;

namespace ProiectPOOSAM
{
    public class USER
    {
        protected string username;
        protected string password;
        protected Role role;
        List<Orders> listOrders;

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
            this.listOrders = null;
        }

        public string GetUsername() => username;
        public string GetRole() => role.ToString();

        public string GetPassword() => password;
        
        public int GetOrdersCount() => listOrders==null ? 0 : listOrders.Count;



        // schimbarea parolei unui utilizator de catre admin
        public void SetPassword(USER user, string parolaNoua, Role role)
        {
            if (this.role == Role.Admin)
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
}