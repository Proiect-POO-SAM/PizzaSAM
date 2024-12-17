using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM
{
    public class Pizza : Constants
    {
        private string name;
        private enum dimensiune { small, medium, large };
        private dimensiune dimensiuneCurenta;
        private decimal price;
        private List<Ingredients> ingredients;
        private bool personalized;
        public Orders Orders;

        public Pizza(string name, List<Ingredients> ingredients, bool personalized)
        {
            this.name = name;
            ingredients = new List<Ingredients>();
            this.personalized = personalized;
        }

        // Calculate the final price of the pizza
        public decimal calculatePrice()
        {
            if (dimensiuneCurenta == dimensiune.small)
            {
                price = 10;
            }
            if (dimensiuneCurenta == dimensiune.medium)
            {
                price = 15;
            }
            if (dimensiuneCurenta == dimensiune.large)
            {
                price = 20;
            }
            if (personalized == true)
            {
                price = 30;
            }
            foreach (var ingredient in ingredients)
            {
                price += ingredient.getPrice();
            }
            if(Orders.deliveryMethod == Orders.delivery.toHome)
            {
                price += 10;
            }
            return price+price*TVA;
        }

        // View the specific pizza
        public void ViewPizza()
        {
            Console.WriteLine($"Pizza: {name} with price {price}");
            foreach (var ingredient in ingredients)
            {
                ingredient.ViewIngredients();
            }
        }


    }
}
