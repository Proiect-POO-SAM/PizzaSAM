﻿using ProiectPOOSAM;
using System.Text;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;




namespace ProiectPOoSAM.Alex
{
    public class Orders : FileTXT
    {
        private int orderID;
        protected USER user;
        USER.Role role;
        protected List<Pizza> pizzas { get; set; }
        public enum delivery { home, restaurant };
        public delivery deliveryMethod;
        private decimal totalPrice;
        private decimal discount;
        private bool isFeedback; 
        private string feedback;
        private string rating;
        public DateTime date;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
 

        public Orders(List<Pizza> pizzas, DateTime dateTime, delivery deliveryMethod, USER user)//Mihai
        {
            this.pizzas = pizzas;
            date = dateTime;
            this.deliveryMethod = deliveryMethod;
            this.user = user;
            Constants.orderCount += 1;
            this.orderID = Constants.orderCount;
            this.totalPrice = calculateTotalPrice();
            this.discount = 0;
            this.isFeedback = false;
            this.feedback = "";
            this.rating = "";
            user.addOrder(this);
            user.SetFidelityCard(FidelityCard());


        }

        // ala pentru fisier
        public Orders(int orderID, USER user, USER.Role role, List<Pizza> pizzas, delivery deliveryMethod, decimal totalPrice, decimal discount, bool isFeedback, string feedback, string rating, DateTime date)
        {
            this.orderID = orderID;
            this.user = user;
            this.role = role;
            this.pizzas = pizzas;
            this.deliveryMethod = deliveryMethod;
            this.totalPrice = totalPrice;
            this.discount = discount;
            this.isFeedback = isFeedback;
            this.feedback = feedback;
            this.rating = rating;
            this.date = date;

        }

        public string getUsername() => user.GetUsername();

        public List<Pizza> getPizzas() => pizzas;

        public delivery getDeliveryMethod() => deliveryMethod;

        public decimal getTotalPrice() => totalPrice;

        public int getOrderID() => orderID;

        public List<Pizza> getPizzasList() => pizzas;

        public int getPizzasCount() => pizzas.Count;

        public decimal getDiscount() => discount;

        public bool getIsFeedback() => isFeedback;

        public string getFeedback() => feedback;

        public string getRating() => rating;

        public DateTime getDate() => date;

        public USER.Role getRole() => role;




        // Calculare pret comanda

        protected decimal calculateTotalPrice()
        {
            decimal pricing = 0;
            foreach (Pizza pizza in pizzas)
            {
                pricing += pizza.getPrice();
            }
            if (deliveryMethod == delivery.home)
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
       
        public void feedbackOrder()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("---- FEEDBACK -------");
            Console.WriteLine("Your rating (1-5):");
            var ratingNumber = Convert.ToInt32(Console.ReadLine());
            rating = new string('★', ratingNumber);
            while (ratingNumber < 1 || ratingNumber > 5)
            {
                Console.WriteLine("Rating must be between 1 and 5. Please enter again:");
                ratingNumber = Convert.ToInt32(Console.ReadLine());
            }
            rating = new string('★', ratingNumber);
            Console.WriteLine("Your feedback message:");
            feedback = Console.ReadLine();
            isFeedback = true;
            Console.WriteLine(rating + Environment.NewLine + feedback);
        }

        public void feedbackOrder(string message, string rating)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            this.feedback = message;
            this.rating = new string(string.IsNullOrEmpty(rating) ? '★' : rating[0], 5);
            this.isFeedback = true;

        }

        private bool FidelityCard()
        {
            if (user.GetOrdersCount() > 5)
            {
                return true;
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


        public decimal AllOrdersPrice(List<Orders> ORDERSLIST)
        {
            if (ORDERSLIST == null)
                throw new InvalidOperationException("The order list is not initialized.");

            decimal totalPrice = 0;
            DateTime today = DateTime.Today;

            foreach (Orders order in ORDERSLIST)
            {
                if (order.date.Date == today)
                {
                    totalPrice += order.totalPrice;
                }
            }

            return totalPrice;
        }

        public void GetTotalIncome(USER user, DateTime startDate, DateTime endDate, List<Orders> ORDERSLIST)
        {
            decimal VenitTotal = 0;

            if (user == null)
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be later than the end date.");

            if (user.GetRole() is "Client")
            {
                Console.WriteLine("You do not have permission to view the orders.");
                return;
            }

            try
            {
                VenitTotal = AllOrdersPrice(ORDERSLIST);
                Console.WriteLine($"The total income for the specified period is: {VenitTotal}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        
        

        // Comenzi modificare order
        public void modifyCommand(List<Pizza> pizzas, delivery deliveryMethod, USER user)
        {
            this.pizzas = pizzas;
            this.deliveryMethod = deliveryMethod;
            this.user = user;
        }

        public void modifyDeliveryMethod(delivery deliveryMethod)
        {
            this.deliveryMethod = deliveryMethod;
        }
        public void modifyTotalPrice(decimal totalPrice)
        {
            this.totalPrice = totalPrice;
        }
        public void modifyDiscount(decimal discount)
        {
            this.discount = discount;
        }

        public void modifyDate(DateTime date)
        {
            this.date = date;
        }
        // ========================

        public override string ToString()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Order ID: {orderID}");
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
