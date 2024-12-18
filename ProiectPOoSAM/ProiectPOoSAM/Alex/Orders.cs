using ProiectPOOSAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Orders(List<Pizza> pizzas, delivery deliveryMethod, decimal totalPrice, USER user)
        {
            this.pizzas = pizzas;
            this.deliveryMethod = deliveryMethod;
            this.user = user;
            this.totalPrice = calculateTotalPrice();
        }
        public Orders(List<Pizza> pizzas, delivery deliveryMethod, decimal totalPrice,USER user, decimal discount)
        {
            this.pizzas = pizzas;
            this.deliveryMethod = deliveryMethod;
            this.discount = discount / 100;
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
            if (discount != 0)
            {
                pricing -= pricing * discount;
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








    }
}
