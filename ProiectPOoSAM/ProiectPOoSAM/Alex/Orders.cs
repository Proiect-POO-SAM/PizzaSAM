using ProiectPOOSAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectPOoSAM.Alex;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;




namespace ProiectPOoSAM.Alex
{
    public class Orders : FileTXT
    {
        protected USER user;
        protected List<Pizza> pizzas { get; set; }
        public enum delivery { Home, Restaurant };
        public delivery deliveryMethod;
        private decimal totalPrice;
        private decimal discount;
        private bool isFeedback; 
        private string feedback;
        private string rating;
        public DateTime date;
        static List<Orders> AllOrders = new List<Orders>();//Mihai si aici

        public Orders(List<Pizza> pizzas, DateTime dateTime, delivery deliveryMethod, USER user)//Mihai
        {
            this.pizzas = pizzas;
            this.date = dateTime;
            this.deliveryMethod = deliveryMethod;
            this.user = user;
            this.totalPrice = calculateTotalPrice();
            
        }

        public Orders(List<Pizza> pizzas, delivery deliveryMethod, USER user)
        {
            this.pizzas = pizzas;
            this.deliveryMethod = deliveryMethod;
            this.user = user;
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

        protected decimal calculateTotalPrice()
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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("---- FEEDBACK -------");
            Console.WriteLine("Your rating (1-5):");
            var ratingNumber = Convert.ToInt32(Console.ReadLine());
            rating = new string('★', ratingNumber);
            Console.WriteLine("Your feedback:");
            feedback = Console.ReadLine();
            isFeedback = true;
            Console.WriteLine(rating + " " + feedback);
        }

        private bool FidelityCard()
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


        //Mihai in
        public void AddOrder(Orders order)
        {
            AllOrders.Add(order);
        }
        public decimal AllOrdersPrice()
        {

            decimal totalPrice = 0;

            foreach (Orders ord in AllOrders)
            {
                totalPrice += calculateTotalPrice();
            }

            return totalPrice;
        }
        //Mihai out


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Order Details:");
            sb.AppendLine($"User: {user.GetUsername()}");
            sb.AppendLine("Pizzas:");
            foreach (Pizza pizza in pizzas)
            {
                sb.AppendLine($"- {pizza.getName()}");
            }
            sb.AppendLine($"Delivery Method: {deliveryMethod}");
            sb.AppendLine($"Total Price: {totalPrice}");
            sb.AppendLine($"Feedback: {(isFeedback ? $"{rating} {feedback}" : "No feedback")}");
            sb.AppendLine($"Date: {date}");
            return sb.ToString();
        }
    }
}
