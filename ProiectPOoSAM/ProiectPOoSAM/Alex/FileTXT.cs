using ProiectPOOSAM;
using System.Reflection.Metadata.Ecma335;
using static ProiectPOoSAM.Alex.Pizza;

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
            var pizzaList = order.getPizzas();
            var pizzaString = string.Join(";", pizzaList.Select(pizza => pizza.getName()));

            return $"ORDER,{order.getOrderID()},{order.getUsername() ?? "N/A"},{order.getRole()},{order.getPizzasCount()},{pizzaString},{order.getDeliveryMethod()},{order.getTotalPrice()},{order.getDiscount()}" +
                $",{order.getIsFeedback()},{order.getFeedback() ?? "N/A"},{order.getRating()},{order.getDate():yyyy-MM-dd}";

        }


        public string breakToPiecesIngredients(Ingredients ingredient)
        {
            return $"INGREDIENT,{ingredient.getIngredientID()},{ingredient.getName()},{ingredient.getQuantity()},{ingredient.getPrice()}";
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
            var orders = user.GetOrders();
            var ordersString = "";
            foreach (var order in orders)
            {
                ordersString += order.getOrderID() + ",";
            }
            return $"USER,{user.GetUserID}.{user.GetUsername()},{user.GetPassword()},{user.GetPhoneNumber()},{user.GetRole()},{user.AccessVerification()},{user.GetFidelityCard()},{user.GetSalt()},{ordersString}";
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

        // CITIRE FISIER SI CREARE OBIECT + ADAUGARE IN LISTA DE OBIECTE 

        public static Ingredients CreateIngredients(string line)
        {
            var elements = line.Split(',');

            // Crearea obiectului Ingredients
            return new Ingredients(
                int.Parse(elements[1]),   // ID
                elements[2],              // Name
                int.Parse(elements[3]),   // Quantity
                decimal.Parse(elements[4])// Price
            );
        }

        public static Pizza CreatePizza(string line, List<Ingredients> allIngredients)
        {
            var elements = line.Split(',');

            // Conversia dimensiunii curente
            if (!Enum.TryParse(elements[3], out Dimensiune dimensiuneCurenta))
            {
                throw new ArgumentException($"Dimensiunea '{elements[3]}' nu este validă.");
            }

            // Conversia listei de ingrediente
            var ingredientNames = elements.Skip(5).ToList();
            var pizzaIngredients = allIngredients
                .Where(ingredient => ingredientNames.Contains(ingredient.getName()))
                .ToList();

            // Crearea obiectului Pizza
            return new Pizza(
                int.Parse(elements[1]),        // pizzaID
                elements[2],                   // name
                dimensiuneCurenta,             // dimensiuneCurenta
                pizzaIngredients,              // ingredients
                bool.Parse(elements[5]),       // personalized
                decimal.Parse(elements[4])     // price
            );
        }

        public static Orders CreateOrder(string line, List<USER> allUsers, List<Pizza> allPizzas)
        {
            var elements = line.Split(',');

            // Conversia USER
            var user = allUsers.FirstOrDefault(u => u.GetUsername() == elements[2]);
            if (user == null)
            {
                throw new ArgumentException($"User-ul cu username '{elements[2]}' nu a fost găsit.");
            }

            // Conversia Role
            if (!Enum.TryParse(elements[3], out USER.Role role))
            {
                throw new ArgumentException($"Rolul '{elements[3]}' nu este valid.");
            }

            // Conversia List<Pizza>
            var pizzaNames = elements[4].Split(';').ToList();
            var pizzas = allPizzas
                .Where(pizza => pizzaNames.Contains(pizza.getName()))
                .ToList();

            if (pizzas.Count != pizzaNames.Count)
            {
                throw new ArgumentException("Unele nume de pizza nu au fost găsite în lista de pizze.");
            }

            // Conversia deliveryMethod
            if (!Enum.TryParse(elements[5], out Orders.delivery deliveryMethod))
            {
                throw new ArgumentException($"Metoda de livrare '{elements[5]}' nu este validă.");
            }

            // Crearea obiectului Orders
            return new Orders(
                int.Parse(elements[1]),          // orderID
                user,                            // user
                role,                            // role
                pizzas,                          // pizzas
                deliveryMethod,                  // deliveryMethod
                decimal.Parse(elements[6]),      // totalPrice
                decimal.Parse(elements[7]),      // discount
                bool.Parse(elements[8]),         // isFeedback
                elements[9],                     // feedback
                elements[10],                    // rating
                DateTime.Parse(elements[11])     // date
            );
        }

        public static USER CreateUser(string line)
        {
            var elements = line.Split(',');

            // Conversia Role
            if (!Enum.TryParse(elements[3], out USER.Role role))
            {
                throw new ArgumentException($"Rolul '{elements[3]}' nu este valid.");
            }

            // Crearea obiectului USER
            return new USER(
                int.Parse(elements[1]),   // ID
                elements[2],              // Username
                elements[3],              // Password
                elements[4],              // PhoneNumber
                role,                     // Role
                bool.Parse(elements[6]),  // AccesToken
                bool.Parse(elements[7]),   // FidelityCard
                elements[8]               // Salt
            );
        }





        public void initializeObjects()
        {
            var lines = File.ReadAllLines(Constants.filePath);
            foreach (var line in lines)
            {
                if(line.StartsWith("USER"))
                {
                    var user = CreateUser(line);
                    Constants.USERSLIST.Add(user);
                }
            }
            foreach(var line in lines)
            {
                string[] elements = line.Split(',');
                if (elements[0]== "INGREDIENT")
                {
                    var ingredient = CreateIngredients(line);
                    Constants.INGREDIENTSLIST.Add(ingredient);
                }
            }
            foreach (var line in lines)
            {
                string[] elements = line.Split(',');
                if (elements[0] == "PIZZA")
                {
                    var pizza = CreatePizza(line, Constants.INGREDIENTSLIST);
                    Constants.PIZZALIST.Add(pizza);
                }
            }
            foreach (var line in lines)
            {
                string[] elements = line.Split(',');
                if (elements[0] == "ORDER")
                {
                    var order = CreateOrder(line, Constants.USERSLIST, Constants.PIZZALIST);
                    Constants.ORDERSLIST.Add(order);
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
