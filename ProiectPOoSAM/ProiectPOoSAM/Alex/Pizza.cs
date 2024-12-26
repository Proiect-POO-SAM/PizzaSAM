using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM.Alex
{
    public class Pizza : Constants
    {
        private string name;
        public enum Dimensiune { small, medium, large };
        public Dimensiune dimensiuneCurenta;
        private decimal price;
        private List<Ingredients> ingredients;
        private bool personalized;

        // 2 constructori pentru pizza personalizat | nepersonalizat

        public Pizza(string name, List<Ingredients> ingredients, bool personalized)
        {
            this.name = name;
            this.ingredients = ingredients;
            this.personalized = personalized;
            dimensiuneCurenta = Dimensiune.medium;
            price = calculatePrice();
        }
        public Pizza(string name, Dimensiune dimensiuneCurenta, List<Ingredients> ingredients, bool personalized)
        {
            this.name = name;
            this.dimensiuneCurenta = dimensiuneCurenta;
            this.ingredients = ingredients;
            this.personalized = personalized;
            price = calculatePrice();
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



        // Calculate price pizza
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
                price += 30;
            }
            foreach (var ingredient in ingredients)
            {
                price += ingredient.getPrice();
            }
            return price + price * TVA;
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
