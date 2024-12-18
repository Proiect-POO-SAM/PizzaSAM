using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectPOOSAM;

namespace ProiectPOoSAM
{
    public class FileTXT : Constants
    {
        // Scriere fisier
        // sintaxa File.addCommandToFile("breakToPieces--obiect--");
        public void addCommandToFile(string content)
        {
            string PathFile = filePath;
            if (!System.IO.File.Exists(PathFile))
            {
                System.IO.File.WriteAllText(PathFile, content + Environment.NewLine);
                Console.WriteLine("Fisierul a fost creat cu succes | Comanda a fost adaugata!");
            }
            else
            {
                System.IO.File.AppendAllText(PathFile, content + Environment.NewLine);
                Console.WriteLine("Comanda a fost adaugata!");
            }

        }
        public string breakToPiecesOrders(Orders order)
        {

            return $"ORDER,{order.USER},{order.getDeliveredPizza()},{order.getDeliveryMethod()},{order.getTotalPrice()}";
        }
        public string breakToPiecesIngredients(Ingredients ingredient)
        {
            return $"INGREDIENT,{ingredient.getName()},{ingredient.getQuantity()},{ingredient.getPrice()}";
        }
        public string breakToPiecesPizza(Pizza pizza)
        {
            var ingredients = pizza.getIngredients();
            var ingredientsString = "";
            foreach (Ingredients ingredient in ingredients)
            {
                ingredientsString += ingredient.getName() + ",";

            }
            return $"PIZZA,{pizza.getName()},{pizza.dimensiuneCurenta},{pizza.getPrice()},{pizza.getPersonalized()},{ingredientsString}";
        }
        public string breakToPiecesMenu(Menu menu)
        {
            var pizzas = menu.menu;
            var pizzasString = "";
            foreach (Pizza pizza in pizzas)
            {
                pizzasString += pizza.getName() + ",";
            }
            return $"MENU,{pizzasString}";
        }
        public string breakToPiecesUser(USER user)
        {
            return $"USER,{user.GetUsername()},{user.GetPassword()},{user.GetRole()}";
        }
        // Citire fisier
        public string[] readFromFile(string optiune)
        {
            string PathFile = filePath;
            string[] output = new string[100];
            if (System.IO.File.Exists(PathFile))
            {
                int index = 0;
                foreach (string line in System.IO.File.ReadLines(PathFile))
                {
                    string[] elements = line.Split(',');
                    if (elements[0] == optiune)
                    {
                        output[index] = line;
                        index++;
                    }
                }
            }
            else
            {
                return null;
            }
            return output;
        }

    }
}
