using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM
{
    public class Pizza
    {
        private string name;
        private enum dimensiune { small, medium, large };
        private dimensiune dimensiuneCurenta;
        private decimal price;
        private List<Ingredients> ingredients;
        private bool personalized;

        public Pizza(string name, List<Ingredients> ingredients, bool personalized)
        {
            this.name = name;
            ingredients = new List<Ingredients>();
            this.personalized = personalized;
        }

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
            foreach (var ingredient in ingredients)
            {
                price += ingredient.getPrice();
            }
            return price;
        }



    }
}
