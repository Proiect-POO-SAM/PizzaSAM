using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM.Alex
{
    public class Ingredients
    {
        private string name;
        private int quantity;
        private decimal price;

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


        // GESTIONARE INGREDIENTE

        public void ViewIngredient()
        {
            Console.WriteLine($"Ingredient: {name} with quantity {quantity} and price {price}");
        }
        public void modifyQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public void modifyPrice(decimal price)
        {
            this.price = price;
        }

        public void modifyName(string name)
        {
            this.name = name;
        }

        // ========================

    }
}

