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
        public enum Dimensiune { small, medium, large };
        public Dimensiune dimensiuneCurenta;
        private decimal price;
        private List<Ingredients> ingredients;
        private bool personalized;
        public Orders Orders;

        public Pizza(string name,int price, List<Ingredients> ingredients, bool personalized)
        {
            this.name = name;
            this.price = price;
            ingredients = new List<Ingredients>();
            this.personalized = personalized;
        }

        public string getName()
        {
            return name;
        }
        public decimal getPrice()
        {
            return price;
        }
        public List<Ingredients> getIngredients()
        {
            return ingredients;
        }
        public bool getPersonalized()
        {
            return personalized;
        }



        // Calculate the final price of the pizza
        public decimal calculatePrice()
        {
            if (dimensiuneCurenta == Dimensiune.small)
            {
                price = 10;
            }
            if (dimensiuneCurenta == Dimensiune.medium)
            {
                price = 15;
            }
            if (dimensiuneCurenta == Dimensiune.large)
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
