﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM.Alex
{
    public class Ingredients
    {
        private int ingredientID;
        private string name;
        private int quantity;
        private decimal price;

        public Ingredients(string name, int quantity, decimal price)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;
            Constants.ingredientCount += 1;
            ingredientID = Constants.ingredientCount;
        }
        public Ingredients(int ingredientID, string name, int quantity, decimal price)
        {
            this.ingredientID = ingredientID;
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
        public int getIngredientID()
        {
            return ingredientID;
        }

        public void setPrice(decimal price)
        {
            this.price = price;
        }

        public void decreaseQuantity()
        {
            this.quantity--;
        }
        public void AddQuantity(int quantity)
        {
            this.quantity += quantity;
        }
        // GESTIONARE INGREDIENTE

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

        public void showIngredient()
        {
            Console.WriteLine($"Ingredient ID: {ingredientID}");
            Console.WriteLine($"Ingredient name: {name}");
            Console.WriteLine($"Price: {price}");
        }
        public override string ToString()
        {
            return $"{name}";
        }

    }
}

