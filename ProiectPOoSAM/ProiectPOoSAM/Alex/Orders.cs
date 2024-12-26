using ProiectPOOSAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// For sending emails
//using System.Net;
//using System.Net.Mail;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;



namespace ProiectPOoSAM.Alex
{
    public class Orders : FileTXT
    {
        USER user;
        private List<Pizza> pizzas;
        public enum delivery { Home, Restaurant };
        public delivery deliveryMethod;
        private decimal totalPrice;
        private decimal discount;
        private bool isFeedback; // implementare feedback (WORK IN PROGRESS)
        private string feedback;
        private string rating;

        public Orders(List<Pizza> pizzas, delivery deliveryMethod, decimal totalPrice, USER user)
        {
            this.pizzas = pizzas;
            this.deliveryMethod = deliveryMethod;
            this.user = user;
            this.totalPrice = calculateTotalPrice();
        }
        public Orders(List<Pizza> pizzas, delivery deliveryMethod, USER user)
        {
            this.pizzas = pizzas;
            this.deliveryMethod = deliveryMethod;
            this.discount = 10 / 100;
            this.user = user;
            this.totalPrice = calculateTotalPrice();
        }

        public string getUsername() => user.GetUsername();

        public List<Pizza> getPizzas() => pizzas;

        public delivery getDeliveryMethod() => deliveryMethod;

        public decimal getTotalPrice() => totalPrice;



        public void ViewCommand()
        {
            Console.WriteLine("Comanda dumneavoastra:");
            foreach (Pizza pizza in pizzas)
            {
                pizza.ViewPizza();
            }
            Console.WriteLine($"Metoda de livrare: {deliveryMethod}");
            Console.WriteLine($"Pret total: {totalPrice}");

        }

        // Calculare pret comanda

        public decimal calculateTotalPrice()
        {
            decimal pricing = 0;
            foreach (Pizza pizza in pizzas)
            {
                pricing += pizza.getPrice();
            }
            if (deliveryMethod == delivery.Home)
            {
                pricing += 10;
            }
            if (discount != null)
            {
                pricing -= pricing * discount;
            }
            if(user.GetRole() == "Client" && user.GetFidelityCard() == true)
            {
                pricing -= pricing * 10 / 100;
            }
            return pricing;
        }

        public void ViewMyCommands(string USERNAME)
        {
            string PathFile = filePath;

            if (!File.Exists(PathFile))
            {
                Console.WriteLine("There are no orders!");
                return;
            }

            string[] lines = File.ReadAllLines(PathFile);
            bool hasCommands = false;

            foreach (string line in lines)
            {
                var elements = line.Split(',');
                if (elements.Length > 1 && elements[1] == USERNAME && elements[0] == "ORDER")
                {
                    hasCommands = true;
                    Console.WriteLine($"Order for {USERNAME}:");
                    Console.WriteLine($" - Pizza: {elements[2]}");
                    Console.WriteLine($" - Delivery: {elements[3]}");
                    Console.WriteLine($" - Price: {elements[4]} RON");
                    Console.WriteLine("------------------------");
                }
            }

            if (!hasCommands)
            {
                Console.WriteLine($"The user {USERNAME} does not have any orders.");
            }
        }

        public void feedbackOrder()
        {
            Console.WriteLine("---- FEEDBACK -------");
            Console.WriteLine("Your rating (1-5):");
            var ratingNumber = Convert.ToInt32(Console.ReadLine());
            rating = new string('★', ratingNumber);
            Console.WriteLine("Your feedback:");
            feedback = Console.ReadLine();
            isFeedback = true;
            Console.WriteLine(rating + " " + feedback);
        }

        public bool FidelityCard()
        {
            if (user.GetOrdersCount() > 5)
            {
                user.SetFidelityCard(true);
            }
            return false;
        }

        public void SendSMS(string CustomerNumber)
        {
            string PizzaNumber = "+12345678901"; // TREBUIE ACHIZITIONAT NUMAR DE PE TWILIO

            try
            {
                var message = MessageResource.Create(
                    body: "Your order has been processed!",
                    from: new Twilio.Types.PhoneNumber(PizzaNumber),
                    to: new Twilio.Types.PhoneNumber(CustomerNumber)
                );

                Console.WriteLine($"Mesaj trimis cu SID: {message.Sid}");
            }
            catch (TwilioException ex)
            {
                Console.WriteLine($"Eroare la trimiterea mesajului: {ex.Message}");
            }
        }

    }
}


    




//public void sendEmail(string recipientEmail, string subject, string body)
//{
//    string senderEmai="pizzasam2004@gmail.com";
//    string senderPassword="pizzasam";
//            try
//            {
//                // Create an SmtpClient object for Gmail
//                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
//                {
//                    Port = 587,
//                    Credentials = new NetworkCredential(senderEmail, senderPassword),
//                    EnableSsl = true
//                };

//                // Create a MailMessage object for the email
//                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail)
//                {
//                    Subject = subject,
//                    Body = body
//                };

//                // Send the email
//                smtpClient.Send(mailMessage);

//                Console.WriteLine("Email sent successfully!");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error sending email: {ex.Message}");
//            }
//        }
//    }
//}
//}
