using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM
{
    public class Ingredients
    {
        private string name { get; }
        private int quantity { get; }
        private decimal price { get; }

        public Ingredients(string name, int quantity, decimal price)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;
        }

        public decimal getPrice()
        {
            return price;
        }
        public string getName()
        {
            return name;
        }
        public int getQuantity()
        {
            return quantity;
        }

        public void ViewIngredients()
        {
            Console.WriteLine($"Ingredient: {name} with quantity {quantity} and price {price}");
        }
    }
}

