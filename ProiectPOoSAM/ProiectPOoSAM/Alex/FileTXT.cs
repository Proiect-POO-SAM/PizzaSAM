using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProiectPOOSAM;

namespace ProiectPOoSAM.Alex
{
    public class FileTXT : Constants
    {
<<<<<<< HEAD
=======
        public PizzaSite SITE;
>>>>>>> 6c145ccf58c2bbfc5daa414438318e6c4a910c84
        // Scriere fisier
        // sintaxa File.addCommandToFile("breakToPieces--obiect--");
        public void addCommandToFile(string content)
        {
            string PathFile = filePath;
            if (!File.Exists(PathFile))
            {
                File.WriteAllText(PathFile, content + Environment.NewLine);
                Console.WriteLine("Fisierul a fost creat cu succes | Comanda a fost adaugata!");
            }
            else
            {
                File.AppendAllText(PathFile, content + Environment.NewLine);
                Console.WriteLine("Comanda a fost adaugata!");
            }

        }
        // Metode pentru a descompune obiectele in stringuri
        public string breakToPiecesOrders(Orders order)
        {
            var listPizzas = order.getPizzas();
            var pizzasString = "";
            foreach (Pizza pizza in listPizzas)
            {
                pizzasString += pizza.getName() + ",";
            }
            return $"ORDER,{order.getUsername()},{listPizzas.Count},{order.getDeliveryMethod()},{order.getTotalPrice()}";
        }


        public string breakToPiecesIngredients(Ingredients ingredient)
        {
            return $"INGREDIENT,{ingredient.getName()},{ingredient.getQuantity()},{ingredient.getPrice()}";
        }


        public string breakToPiecesPizza(Pizza pizza)
        {
            var ingredients = pizza.getIngredients(); // lista de ingrediente
            var ingredientsString = "";
            foreach (var ingredient in ingredients)
            {
                if (ingredients.IndexOf(ingredient) == ingredients.Count - 1)
                {
                    ingredientsString += ingredient.getName();
                    break;
                }
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
            return $"USER,{user.GetUsername()},{user.GetPassword()},{user.GetRole()},{user.GetOrdersCount()}";
        }

<<<<<<< HEAD
=======
        public string breakToPiecesPizzaSite(PizzaSite SITE)
        {
            return $"PizzaSite,{SITE.pizzaSiteName},{SITE.pizzaSiteLocation}";
        }

>>>>>>> 6c145ccf58c2bbfc5daa414438318e6c4a910c84


        // Citire fisier



        public void gettingInformation(string OPTIUNE)
        {
            string[] lines = File.ReadAllLines(filePath);
            bool hasCommands = false;

            foreach (string line in lines)
            {
                var elements = line.Split(',');
                if (elements.Length > 1 && elements[0] == OPTIUNE) // Verificăm dacă linia conține comanda dorită
                {
                    hasCommands = true;
                    switch (OPTIUNE)
                    {
                        case "ORDER":
                            Console.WriteLine("Order Details:");
                            Console.WriteLine($"Username: {elements[1]}");
                            Console.WriteLine($"Delivered Pizza: {elements[2]}");
                            Console.WriteLine($"Delivery Method: {elements[3]}");
                            Console.WriteLine($"Total Price: {elements[4]}");
                            break;
                        case "INGREDIENT":
                            Console.WriteLine("Ingredient Details:");
                            Console.WriteLine($"Name: {elements[1]}");
                            Console.WriteLine($"Quantity: {elements[2]}");
                            Console.WriteLine($"Price: {elements[3]}");
                            break;
                        case "USER":
                            Console.WriteLine("User Details:");
                            Console.WriteLine($"Username: {elements[1]}");
                            Console.WriteLine($"Password: {elements[2]}");
                            Console.WriteLine($"Role: {elements[3]}");
                            Console.WriteLine($"Numar comenzi: {elements[4]}");
                            break;
                        case "PIZZA":
                            Console.WriteLine("Pizza Details:");
                            Console.WriteLine($"Name: {elements[1]}");
                            Console.WriteLine($"Size: {elements[2]}");
                            Console.WriteLine($"Price: {elements[3]}");
                            Console.WriteLine($"Personalized: {elements[4]}");
                            Console.WriteLine("Ingredients:");
                            var ingredients = elements.Skip(5);
                            foreach (var ingredient in ingredients)
                            {
                                Console.WriteLine($"- {ingredient}");
                            }
                            break;
<<<<<<< HEAD
=======
                        case "SITE":
                            Console.WriteLine("Pizza Site Details:");
                            Console.WriteLine($"Name: {elements[1]}");
                            Console.WriteLine($"Location: {elements[2]}");
                            break;
>>>>>>> 6c145ccf58c2bbfc5daa414438318e6c4a910c84
                        default:
                            break;
                    }
                    Console.WriteLine(Environment.NewLine);
                }
            }

            if (!hasCommands)
            {
                Console.WriteLine($"No {OPTIUNE} available");
            }
        }


        // Stergere fisier
        public void deleteFile()
        {
            File.Delete(filePath);
            Console.WriteLine("Fisierul a fost sters!");
        }
    }
}
