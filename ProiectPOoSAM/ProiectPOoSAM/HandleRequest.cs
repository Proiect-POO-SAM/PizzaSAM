using System.Diagnostics;
using Azure.Identity;
using ProiectPOOSAM;
using Twilio.TwiML.Messaging;
using System;
using System.Security.Cryptography;
using System.Text;

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
            
            // cautare dupa username
            USER user = AllUsers.FirstOrDefault(u => u.GetUsername() == username);
            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Username/Parola nu exista sau sunt introduse gresit!");
                Console.ResetColor();
                return new RequestResult { user = null, Message = "Invalid username or password. Login failed" };
            }
            
            
            // ia saltul specific utilizatorului
            string storedSALT = user.GetSalt();
            
            // construieste o parola hashuita cu el
            string hasedINPUT = VerifyHashedPassword.HashVerifyHandle(password,storedSALT);
            
            // vf parola construita cu cea stocata
            if (user.GetPassword() != hasedINPUT)
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
                string hash_pass = HashPassword.HashHandle(password).hash;
                string SALT = HashPassword.HashHandle(password).salt;
                
                USER user = new USER(username, hash_pass, phone,USER.Role.Client, SALT);
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


    /// <sumary>
    /// hash-uirea parolelor
    ///
    ///     functia HashHandle -> primeste parola si un salt
    ///                        -> returneaza parola hash-uita si saltul (ei in hexa) sub forma de sting
    /// 
    /// <sumary/>


    public class HashPassword
    {
        public static (string hash, string salt) HashHandle(string password)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);    // genereaza un salt random
            }
            
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                
                // adauga saltul la parola
                byte[] passwordWithSaltBytes = Combine(passwordBytes, saltBytes);
                
                // parola + saltul hash-uit
                byte[] hashBytes = sha256.ComputeHash(passwordWithSaltBytes);

                //                         hash-ul                      salt-ul             string (in format hex)
                return (ConvertToHex(hashBytes), ConvertToHex(saltBytes));
            }
        }
        
        
        private static string ConvertToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2); // alocare memorie
            foreach (byte b in bytes)
            {
                hex.Append(b.ToString("X2")); // 1 byte = 2 caractere hex
            }
            return hex.ToString();
        }
        
        // fct combina doua byte array-uri
        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] combined = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, combined, 0, first.Length);
            Buffer.BlockCopy(second, 0, combined, first.Length, second.Length);
            return combined;
        }
    }


    public class VerifyHashedPassword
    {
        public static string HashVerifyHandle(string enteredPassword, string storedSalt)
        {
            byte[] saltBytes = ConvertFromHex(storedSalt);
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
            byte[] passwordWithSaltBytes = Combine(enteredPasswordBytes, saltBytes);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordWithSaltBytes);
                string enteredHash = ConvertToHex(hashBytes);

                return enteredHash;
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] combined = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, combined, 0, first.Length);
            Buffer.BlockCopy(second, 0, combined, first.Length, second.Length);
            return combined;
        }

        private static string ConvertToHex(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.Append(b.ToString("X2"));
            return hex.ToString();
        }

        private static byte[] ConvertFromHex(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
