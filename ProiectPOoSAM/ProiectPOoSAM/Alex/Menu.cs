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

        public void RemovePizza(Pizza pizza)
        {
            menu.Remove(pizza);
        }

        public void ViewMenu()
        {
            foreach (Pizza pizza in menu)
            {
                pizza.ViewPizza();
            }
        }
        // Function of reading from the file the menu
        // Function of writing in the file the menu

        public string UnloadMenu(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                try
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        var elements = line.Split(',');
                        // incompleta pana se creaza meniul
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Programul se inchide ...");
                    return ex.Message;
                    Console.ResetColor();
                }
            }
            return "Menu unloaded";
        }
        public string UpdateMenu(Menu menu, string filePath)
        {
            using (StreamWriter saver = new StreamWriter(filePath))
            {
                try
                {

                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Programul se inchide ...");
                    return ex.Message;
                    Console.ResetColor();
                }
            }
            return "Menu updated";
        }
    }
}
