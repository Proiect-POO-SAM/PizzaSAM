using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM.Alex
{
    public class Menu
    {
        public List<Pizza> menu;

        public Menu()
        {
            menu = new List<Pizza>();
        }

        public void AddPizza(Pizza pizza)
        {
            menu.Add(pizza);
        }

        public void ShowMenu()
        {
            Console.WriteLine("\nAvailable Menu:");
            foreach (var pizza in Constants.PIZZASLIST)
            {
                bool canBeMade = true;
                Console.WriteLine($"\nPizza: {pizza.getName()}");
                Console.WriteLine("Available ingredients:");
                        
                foreach (var ingredient in pizza.getIngredients())
                {
                    if (ingredient.getQuantity() <= 0)
                    {
                        canBeMade = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"- {ingredient.getName()} (Out of stock!)");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"- {ingredient.getName()}");
                        Console.ResetColor();
                    }
                }
                        
                if (!canBeMade)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This pizza is currently unavailable!");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"Price: {pizza.getPrice()} lei");
                }
            }
        }
        
    }
}
