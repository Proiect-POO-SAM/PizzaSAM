using ProiectPOOSAM;

namespace ProiectPOoSAM.Alex
{
    public class FileTXT
    {
        public PizzaSite SITE;
        // Scriere fisier
        // sintaxa File.addCommandToFile("breakToPieces--obiect--");
        public void addCommandToFile(string content)
        {
            string PathFile = Constants.filePath;
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
        public string[] readCommandsFromFile()
        {
            string[] lines = File.ReadAllLines(Constants.filePath);
            return lines;
        }
        
        public void removeCommandFromFile(string command)
        {
            string[] lines = File.ReadAllLines(Constants.filePath);
            File.WriteAllText(Constants.filePath, string.Empty);
            foreach (string line in lines)
            {
                if (line != command)
                {
                    File.AppendAllText(Constants.filePath, line + Environment.NewLine);
                }
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
            return $"ORDER,{order.getOrderID()},{order.getUsername()},{listPizzas.Count},{order.getDeliveryMethod()},{order.getTotalPrice()}";
        }


        public string breakToPiecesIngredients(Ingredients ingredient)
        {
            return $"INGREDIENT,{ingredient.getIngredientID()}{ingredient.getName()},{ingredient.getQuantity()},{ingredient.getPrice()}";
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
            return $"PIZZA,{pizza.getPizzaID()},{pizza.getName()},{pizza.dimensiuneCurenta},{pizza.getPrice()},{pizza.getPersonalized()},{ingredientsString}";
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

        public string breakToPiecesPizzaSite(PizzaSite SITE)
        {
            return $"PizzaSite,{SITE.pizzaSiteName},{SITE.pizzaSiteLocation}";
        }



        // Citire fisier



        public void gettingInformation(string OPTIUNE)
        {
            string[] lines = File.ReadAllLines(Constants.filePath);
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
                            Console.WriteLine($"Username: {elements[2]}");
                            Console.WriteLine($"Delivered Pizza: {elements[3]}");
                            Console.WriteLine($"Delivery Method: {elements[4]}");
                            Console.WriteLine($"Total Price: {elements[5]}");
                            break;
                        case "INGREDIENT":
                            Console.WriteLine("Ingredient Details:");
                            Console.WriteLine($"Name: {elements[2]}");
                            Console.WriteLine($"Quantity: {elements[3]}");
                            Console.WriteLine($"Price: {elements[4]}");
                            break;
                        case "USER":
                            Console.WriteLine("User Details:");
                            Console.WriteLine($"Username: {elements[2]}");
                            Console.WriteLine($"Password: {elements[3]}");
                            Console.WriteLine($"Role: {elements[4]}");
                            Console.WriteLine($"Numar comenzi: {elements[5]}");
                            break;
                        case "PIZZA":
                            Console.WriteLine("Pizza Details:");
                            Console.WriteLine($"Name: {elements[2]}");
                            Console.WriteLine($"Size: {elements[3]}");
                            Console.WriteLine($"Price: {elements[4]}");
                            Console.WriteLine($"Personalized: {elements[5]}");
                            Console.WriteLine("Ingredients:");
                            var ingredients = elements.Skip(5);
                            foreach (var ingredient in ingredients)
                            {
                                Console.WriteLine($"- {ingredient}");
                            }
                            break;
                        case "SITE":
                            Console.WriteLine("Pizza Site Details:");
                            Console.WriteLine($"Name: {elements[1]}");
                            Console.WriteLine($"Location: {elements[2]}");
                            break;
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

        public void ReadingInformation()
        {
            string[] lines = File.ReadAllLines(Constants.filePath);
            bool hasCommands = false;

            foreach (string line in lines)
            {
                var elements = line.Split(',');
                if (elements.Length > 1) // Verificăm dacă linia conține comanda dorită
                {
                    hasCommands = true;
                    switch (elements[0])
                    {
                        case "ORDER":
                            
                            break;
                        case "INGREDIENT":
                            
                            break;
                        case "USER":
                            
                            break;
                        case "PIZZA":
                           
                            var ingredients = elements.Skip(5);
                            foreach (var ingredient in ingredients)
                            {
                                Console.WriteLine($"- {ingredient}");
                            }
                            break;
                        case "SITE":
                            Console.WriteLine("Pizza Site Details:");
                            Console.WriteLine($"Name: {elements[1]}");
                            Console.WriteLine($"Location: {elements[2]}");
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(Environment.NewLine);
                }
            }

        }

        // Stergere fisier
        public void deleteFile()
        {
            File.Delete(Constants.filePath);
            Console.WriteLine("Fisierul a fost sters!");
        }

        // WORK IN PROGRESSS

        //public Pizza CreatePizza(string pizzaString)
        //{
        //    // Împarte string-ul primit în elemente pe baza separatorului ','
        //    var elements = pizzaString.Split(',');

        //    // Verifică dacă string-ul conține suficiente elemente
        //    if (elements.Length < 5)
        //    {
        //        throw new ArgumentException("String-ul pizzaString nu conține suficiente elemente pentru a crea o Pizza.");
        //    }

        //    // Extrage numele pizzei
        //    var name = elements[1];

        //    // Determină dimensiunea pizzei pe baza valorii string-ului
        //    Dimensiune size;
        //    if (elements[2].ToLower() == "small")
        //    {
        //        size = Dimensiune.small;
        //    }
        //    else if (elements[2].ToLower() == "medium")
        //    {
        //        size = Dimensiune.medium;
        //    }
        //    else if (elements[2].ToLower() == "large")
        //    {
        //        size = Dimensiune.large;
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"Dimensiunea specificată '{elements[2]}' nu este validă.");
        //    }

        //    // Conversie sigură pentru preț
        //    if (!decimal.TryParse(elements[3], out var price))
        //    {
        //        throw new FormatException($"Valoarea '{elements[3]}' nu este un preț valid.");
        //    }

        //    // Conversie sigură pentru flag-ul personalized
        //    if (!bool.TryParse(elements[4], out var personalized))
        //    {
        //        throw new FormatException($"Valoarea '{elements[4]}' nu este un boolean valid.");
        //    }

        //    // Obține lista de ingrediente (dacă există)
        //    var ingredients = elements.Length > 5 ? elements.Skip(5).ToList() : new List<string> { "DefaultIngredient" };

        //    // Creează obiectul Pizza
        //    var pizza = new Pizza(name, size, ingredients, personalized, price);

        //    return pizza;
        //}

        ////public Pizza(string name, Dimensiune dimensiuneCurenta, List<Ingredients> ingredients, bool personalized)


        //public Ingredients CreateIngredient(string ingredientString)
        //{
        //    var elements = ingredientString.Split(',');
        //    var name = elements[1];
        //    var quantity = int.Parse(elements[2]);
        //    var price = decimal.Parse(elements[3]);

        //    return new Ingredients(name, quantity, price);
        //}
    }
}
