using System.Globalization;
using ProiectPOoSAM.Alex;
using ProiectPOoSAM.Mihai;
using ProiectPOOSAM;
using Twilio.Rest.Api.V2010.Account.Usage.Record;
using Twilio.TwiML.Voice;

namespace ProiectPOoSAM;

public partial class Project 
{
    protected List<string> oldLogs = new List<string>();
    
    
    /// <summary>
    ///
    ///  functia INIT() -> porneste proiectul -> citeste toate datele din fisiere
    ///                 -> returneaza un tip de data RequestResult (USER,string) ; daca user e null inseamna ca a esuat login / register
    ///  functia UNLOAD() -> inchide proiectul -> pretty much the opposite
    /// 
    /// 
    /// </summary>
    /// name: SEBASTIAN.ADELIN
    
    public static HandleRequest.RequestResult INIT()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Se initializeaza programul ...");
        Console.ResetColor();
        string message_ilogger = Wrapper.LoadUsers();
        
        
        //-----------------------------------
        // asta e pt citire meniu
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Initializare finalizata."); 
        Console.ResetColor();
        
        
        //------------------------------------
        
        
        USER user = null;
        string Message = "";

        Message += "\nINIT-PROJECT";
        try
        {
            Console.Write("\n      PIZZA ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("S");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("A");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("M");
            Console.ResetColor();
            
            
            Console.WriteLine("\n----------------------");
            Console.WriteLine(" Log-in  ||  Register ");
            Console.WriteLine(" ------  ||   ------ ");
            Console.WriteLine("  Menu   ||    Quit ");
            Console.WriteLine("----------------------");

            USER this_user = null;
            bool blockPurposeCompleted = false;
            while (this_user == null && blockPurposeCompleted == false)
            {
                Console.Write("->");
                string option = Console.ReadLine();
            
                // in felul asta poti scrie fie cum si programul va recunoaste decizia   EX:  rEgIsTer --> REGISTER
                option = option.ToUpper();
                
                switch (option)
            {
                case "REGISTER":
                    Console.Write("username: ");
                    string username_register = Console.ReadLine();
                    Console.Write("password: ");
                    string password_register = Console.ReadLine();
                    Console.Write("Telefon: ");
                    string phone = Console.ReadLine();
                    
                    // register
                    HandleRequest.RequestResult retrunResultRegister = HandleRequest.Handle_Register(username_register, password_register,phone);
                    this_user = retrunResultRegister.user;
                    blockPurposeCompleted = true;
                    return retrunResultRegister;
                    
                case "LOGIN":
                    Console.Write("username: ");
                    string username = Console.ReadLine();
                    Console.Write("password: ");
                    string password = Console.ReadLine();
                    
                    // login
                    HandleRequest.RequestResult retrunResultLogin = HandleRequest.Handle_Login(username, password);
                    this_user = retrunResultLogin.user;
                    blockPurposeCompleted = true;
                    return retrunResultLogin;
                
                case "LOG-IN":
                    Console.Write("username: ");
                    string username2 = Console.ReadLine();
                    Console.Write("password: ");
                    string password2 = Console.ReadLine();
                    
                    // login
                    HandleRequest.RequestResult returnResultLogIn = HandleRequest.Handle_Login(username2, password2);
                    this_user = returnResultLogIn.user;
                    blockPurposeCompleted = true;
                    return returnResultLogIn;
                    
                
                case "MENIU":
                    
                    break;
                
                case "QUIT":
                    Environment.Exit(0);
                    break;
                
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Optiune invalida.");
                    Console.ResetColor();
                    break;
            }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Message = Message + " " + e.Message;
            Console.WriteLine("\n" + Message);
        }
        
        Console.ResetColor();
        return new HandleRequest.RequestResult { user = user, Message = Message };
    }


    public static HandleRequest.RequestResult UNLOAD()
    {
        FileTXT file = new FileTXT();
        Console.ForegroundColor = ConsoleColor.Green;

        try
        {
            // Golim fișierul înainte de a scrie
            file.deleteFile();

            // USERI - folosim direct HandleRequest.AllUsers
            foreach(var user in Constants.USERLIST)
            {
                file.addCommandToFile(file.breakToPiecesUser(user));
            }
            
            // INGREDIENTE
            foreach(var ingredients in Constants.INGREDIENTSLIST)
            {
                file.addCommandToFile(file.breakToPiecesIngredients(ingredients));
            }
            // PIZZE
            foreach (var pizza in Constants.PIZZASLIST)
            {
                file.addCommandToFile(file.breakToPiecesPizza(pizza));
            }
            // COMENZI
            foreach (var order in Constants.ORDERSLIST)
            {
                file.addCommandToFile(file.breakToPiecesOrders(order));
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            string log = "Error: " + ex.Message;
            Console.ResetColor();
            return new HandleRequest.RequestResult { user = null, Message = log };
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Se inchide programul ...");
        Console.ResetColor();
        return new HandleRequest.RequestResult { user = null, Message = "Unload succesful." };
    }

    public static void ShowAdminMenu(USER user)
    {
        if (user.GetRole() != "Admin")
        {
            Console.WriteLine("Access denied! Only administrators can access this menu.");
            return;
        }

        ARC adminActions = new ARC();
        bool continuare = true;

        while (continuare)
        {
            Console.WriteLine("\n=== Administrator Menu ===");
            Console.WriteLine("1. View complete pizza list");
            Console.WriteLine("2. Add new pizza");
            Console.WriteLine("3. Delete existing pizza");
            Console.WriteLine("4. Modify pizza information");
            Console.WriteLine("5. View ingredients list");
            Console.WriteLine("6. Modify ingredient price");
            Console.WriteLine("7. Add new ingredient");
            Console.WriteLine("8. Delete ingredient");
            Console.WriteLine("9. View Popular Pizzas");
            Console.WriteLine("10. View orders by date");
            Console.WriteLine("11. View orders by username");
            Console.WriteLine("12. Place Order");
            Console.WriteLine("13. View My Orders");
            Console.WriteLine("14. Back to main menu");
            
            Console.Write("\nChoose an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    adminActions.viewPizza(Constants.PIZZASLIST);
                    break;

                case "2":
                    string dimensiune = null;
                    Console.Write("Pizza name: ");
                    string numePizza = Console.ReadLine();
                    Console.Write("Size (small/medium/large): ");
                    do
                    {
                        dimensiune = Console.ReadLine()?.ToLower();
                        if(dimensiune == "small" || dimensiune == "medium" || dimensiune == "large")
                            Console.WriteLine("Invalid pizza size.");
                    }while(dimensiune == "small" || dimensiune == "medium" || dimensiune == "large");

                    List<Ingredients> ingredientePizza = new List<Ingredients>();
                    bool adaugareIngrediente = true;
                    
                    while (adaugareIngrediente)
                    {
                        adminActions.viewIngredients(Constants.INGREDIENTSLIST);
                        Console.Write("\nEnter ingredient ID (0 to finish): ");
                        if (int.TryParse(Console.ReadLine(), out int ingredientId))
                        {
                            if (ingredientId == 0) break;
                            
                            var ingredient = Constants.INGREDIENTSLIST.FirstOrDefault(i => i.getIngredientID() == ingredientId);
                            if (ingredient != null)
                            {
                                ingredientePizza.Add(ingredient);
                                Console.WriteLine($"Ingredient {ingredient.getName()} added.");
                            }
                        }
                    }
                    
                    var newPizza = ARC.CreatePizza(numePizza, dimensiune, ingredientePizza, false);
                    Console.WriteLine("Pizza added successfully!");
                    break;

                case "3":
                    adminActions.viewPizza(Constants.PIZZASLIST);
                    Console.Write("\nEnter pizza ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int pizzaIdToDelete))
                    {
                        var pizzaToDelete = Constants.PIZZASLIST.FirstOrDefault(p => p.getPizzaID() == pizzaIdToDelete);
                        if (pizzaToDelete != null)
                        {
                            adminActions.removePizza(pizzaToDelete, Constants.PIZZASLIST, Constants.file);
                            Console.WriteLine("Pizza deleted successfully!");
                        }
                    }
                    break;

                case "4":
                    adminActions.viewPizza(Constants.PIZZASLIST);
                    Console.Write("\nEnter pizza ID to modify: ");
                    if (int.TryParse(Console.ReadLine(), out int pizzaIdToModify))
                    {
                        var pizzaToModify = Constants.PIZZASLIST.FirstOrDefault(p => p.getPizzaID() == pizzaIdToModify);
                        if (pizzaToModify != null)
                        {
                            Console.Write("New name (press enter to keep current): ");
                            string newName = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newName))
                            {
                                pizzaToModify.modifyName(newName);
                            }
                            
                            Console.WriteLine("Do you want to modify ingredients? (yes/no)");
                            if (Console.ReadLine().ToLower() == "yes")
                            {
                                List<Ingredients> newIngredients = new List<Ingredients>();
                                bool continuareIngrediente = true;
                                
                                while (continuareIngrediente)
                                {
                                    adminActions.viewIngredients(Constants.INGREDIENTSLIST);
                                    Console.Write("\nEnter ingredient ID (0 to finish): ");
                                    if (int.TryParse(Console.ReadLine(), out int ingredientId))
                                    {
                                        if (ingredientId == 0) break;
                                        
                                        var ingredient = Constants.INGREDIENTSLIST.FirstOrDefault(i => i.getIngredientID() == ingredientId);
                                        if (ingredient != null)
                                        {
                                            newIngredients.Add(ingredient);
                                        }
                                    }
                                }
                                pizzaToModify.modifyIngredients(newIngredients);
                            }
                        }
                    }
                    break;

                case "5":
                    adminActions.viewIngredients(Constants.INGREDIENTSLIST);
                    break;

                case "6":
                    adminActions.viewIngredients(Constants.INGREDIENTSLIST);
                    Console.Write("\nEnter ingredient ID to modify price: ");
                    if (int.TryParse(Console.ReadLine(), out int ingredientIdToModify))
                    {
                        var ingredientToModify = Constants.INGREDIENTSLIST.FirstOrDefault(i => i.getIngredientID() == ingredientIdToModify);
                        if (ingredientToModify != null)
                        {
                            Console.Write("New price: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                            {
                                ingredientToModify.modifyPrice(newPrice);
                                Console.WriteLine("Price modified successfully!");
                            }
                        }
                    }
                    break;

                case "7":
                    Console.Write("Ingredient name: ");
                    string numeIngredient = Console.ReadLine();
                    Console.Write("Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int cantitate))
                    {
                        Console.Write("Price: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal pret))
                        {
                            var newIngredient = ARC.CreateIngredients(numeIngredient, cantitate, pret);
                            Console.WriteLine("Ingredient added successfully!");
                        }
                    }
                    break;

                case "8":
                    adminActions.viewIngredients(Constants.INGREDIENTSLIST);
                    Console.Write("\nEnter ingredient ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int ingredientIdToDelete))
                    {
                        var ingredientToDelete = Constants.INGREDIENTSLIST.FirstOrDefault(i => i.getIngredientID() == ingredientIdToDelete);
                        if (ingredientToDelete != null)
                        {
                            adminActions.removeIngredient(ingredientToDelete, Constants.INGREDIENTSLIST, Constants.file);
                            Console.WriteLine("Ingredient deleted successfully!");
                        }
                    }
                    break;

                case "9":
                    var popularPizzas = new RaportPizzaPopulare(Constants.PIZZASLIST, DateTime.Now, Orders.delivery.restaurant, user);
                    popularPizzas.getPizzaPopularity();
                    break;

                case "10":
                    Console.WriteLine("\nEnter start date (dd/MM/yyyy): ");
                    string startDateStr = Console.ReadLine();
                    if (DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime startDate))
                    {
                        Console.WriteLine("Enter end date (dd/MM/yyyy): ");
                        string endDateStr = Console.ReadLine();
                        if (DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime endDate))
                        {
                            // Ajustăm endDate pentru a include toată ziua
                            endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                            
                            var orders = Constants.ORDERSLIST
                                .Where(o => o.getDate().Date >= startDate.Date && o.getDate().Date <= endDate.Date)
                                .OrderBy(o => o.getDate())
                                .ToList();

                            if (orders.Any())
                            {
                                decimal totalAmount = 0;
                                foreach (var order in orders)
                                {
                                    Console.WriteLine($"\n{order}");
                                    totalAmount += order.getTotalPrice();
                                }
                                Console.WriteLine($"\nTotal amount for this period: {totalAmount} lei");
                            }
                            else
                            {
                                Console.WriteLine("No orders found for this period.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid end date format. Please use dd/MM/yyyy");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid start date format. Please use dd/MM/yyyy");
                    }
                    break;

                case "11":
                    Console.WriteLine("\nEnter username to view orders: ");
                    string searchUsername = Console.ReadLine();
                    var userOrders = Constants.ORDERSLIST
                        .Where(o => o.getUsername().Equals(searchUsername, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(o => o.getDate())
                        .ToList();

                    if (userOrders.Any())
                    {
                        foreach (var order in userOrders)
                        {
                            Console.WriteLine($"\n{order}");
                        }
                        
                        decimal totalSpent = userOrders.Sum(o => o.getTotalPrice());
                        Console.WriteLine($"\nTotal amount spent by {searchUsername}: {totalSpent:C}");
                    }
                    else
                    {
                        Console.WriteLine($"No orders found for user {searchUsername}");
                    }
                    break;

                case "12":
                    PlaceOrder(user);
                    break;

                case "13":
                    user.viewOrders();
                    break;

                case "14":
                    continuare = false;
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }

    public static void ShowClientMenu(USER user)
    {
        Menu menu = new Menu();
        if (user.GetRole() != "Client")
        {
            Console.WriteLine("Access denied! This menu is for clients only.");
            return;
        }

        bool continueMenu = true;
        while (continueMenu)
        {
            Console.WriteLine("\n=== Client Menu ===");
            Console.WriteLine("1. View Menu");
            Console.WriteLine("2. Place New Order");
            Console.WriteLine("3. View Order History");
            Console.WriteLine("4. Review Order");
            Console.WriteLine("5. Back to Main Menu");
            
            Console.Write("\nChoose an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    
                    menu.ShowMenu();
                    
                    break;

                case "2":
                    PlaceOrder(user);
                    break;

                case "3":
                    user.viewOrders();
                    break;

                case "4":
                    foreach(var order in Constants.ORDERSLIST)
                    {
                        if(order.getUsername() == user.GetUsername())
                        Console.WriteLine(order.ToString());
                    }
                    Console.WriteLine("\nEnter Order ID to review: ");
                    if (int.TryParse(Console.ReadLine(), out int orderID))
                    {
                        var order = Constants.ORDERSLIST.FirstOrDefault(o => o.getOrderID() == orderID);
                        if (order != null && order.getUsername() == user.GetUsername())
                        {
                            order.feedbackOrder();
                        }
                        else
                        {
                            Console.WriteLine("Order not found or not authorized to review.");
                        }
                    }
                    break;

                case "5":
                    continueMenu = false;
                    break;

                default:
                    Console.WriteLine("Invalid option! Please try again.");
                    break;
            }
        }
    }

    private static void PlaceOrder(USER user)
    {
        List<Pizza> orderPizzas = new List<Pizza>();
        bool ordering = true;
        bool ok = false;
        bool ok2= false;
        foreach (var pizza in Constants.PIZZASLIST)
        {
            if (pizza.isAvailable())
            {
                Console.WriteLine(pizza);
                ok2 = true;
            }
            else ok2 = false;
        }
        if(ok2==true)
        {
            while (ordering)
            {
                Console.WriteLine($"\nSelect a pizza [1-{Constants.PIZZASLIST.Count}]");
                if (int.TryParse(Console.ReadLine(), out int pizzaChoice) &&
                    pizzaChoice >= 1 && pizzaChoice <= Constants.PIZZASLIST.Count)
                {
                    var selectedPizza = Constants.PIZZASLIST[pizzaChoice - 1];
                    if (selectedPizza.CanBeMade())
                    {
                        // Dimensiune handling
                        Pizza.Dimensiune dimensiune = Pizza.Dimensiune.medium;
                        bool validSize = false;
                        do
                        {
                            Console.WriteLine("Select size [small/medium/large]: ");
                            string sizeInput = Console.ReadLine().ToLower();

                            switch (sizeInput)
                            {
                                case "small":
                                    dimensiune = Pizza.Dimensiune.small;
                                    validSize = true;
                                    break;
                                case "medium":
                                    dimensiune = Pizza.Dimensiune.medium;
                                    validSize = true;
                                    break;
                                case "large":
                                    dimensiune = Pizza.Dimensiune.large;
                                    validSize = true;
                                    break;
                                default:
                                    Console.WriteLine("Invalid size! Please choose small, medium, or large.");
                                    break;
                            }
                        } while (!validSize);

                        Console.WriteLine("Personalize? [yes/no]: ");
                        bool personalize = Console.ReadLine().ToLower() == "yes";

                        List<Ingredients> customIngredients = new List<Ingredients>(selectedPizza.getIngredients());

                        if (personalize)
                        {
                            bool addingIngredients = true;
                            while (addingIngredients)
                            {
                                Console.WriteLine("\nCurrent ingredients:");
                                foreach (var ing in customIngredients)
                                {
                                    Console.WriteLine($"- {ing.getName()}");
                                }

                                Console.WriteLine("\nAvailable extra ingredients:");
                                foreach (var ing in Constants.INGREDIENTSLIST)
                                {
                                    if (ing.getQuantity() > 0)
                                    {
                                        Console.WriteLine(
                                            $"{ing.getIngredientID()}. {ing.getName()} - {ing.getPrice()} lei");
                                    }
                                }

                                Console.Write("\nEnter ingredient ID (0 to finish): ");
                                if (int.TryParse(Console.ReadLine(), out int ingredientId))
                                {
                                    if (ingredientId == 0)
                                    {
                                        addingIngredients = false;
                                    }
                                    else
                                    {
                                        var ingredient =
                                            Constants.INGREDIENTSLIST.FirstOrDefault(i =>
                                                i.getIngredientID() == ingredientId);
                                        if (ingredient != null && ingredient.getQuantity() > 0)
                                        {
                                            customIngredients.Add(ingredient);
                                            Console.WriteLine($"{ingredient.getName()} added successfully!");
                                        }
                                        else
                                        {
                                            Console.WriteLine(
                                                "Invalid ingredient selection or ingredient out of stock.");
                                        }
                                    }
                                }
                            }
                        }

                        // Create new pizza with selected options
                        var newPizza = new Pizza(selectedPizza.getName(), customIngredients, dimensiune, true);
                        foreach (Ingredients ingredient in customIngredients)
                        {
                            ingredient.decreaseQuantity();
                        }

                        orderPizzas.Add(newPizza);

                        Console.WriteLine("Add another pizza? [yes/no]: ");
                        ordering = Console.ReadLine().ToLower() == "yes";

                    }
                    else
                    {
                        Console.WriteLine("This pizza cannot be made due to missing ingredients.");
                        Console.WriteLine("Do you want to proceed with the order? [yes/no]: ");
                        ordering = Console.ReadLine().ToLower() == "yes";
                        if (orderPizzas.Any())
                        {
                            Console.WriteLine("Delivery method [home/restaurant]: ");
                            Enum.TryParse(Console.ReadLine().ToLower(), out Orders.delivery deliveryMethod);
                            var newOrder = new Orders(orderPizzas, DateTime.Now, deliveryMethod, user);
                            Constants.ORDERSLIST.Add(newOrder);
                            Console.WriteLine(
                                $"Order placed successfully! Total price: {newOrder.getTotalPrice()} lei");

                        }
                        else
                        {
                            Console.WriteLine("Order is empty.Try again.");
                        }

                        ok = true;
                        break;

                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }

            if (orderPizzas.Any())
            {
                if (ok = false)
                {
                    Console.WriteLine("Delivery method [home/restaurant]: ");
                    if (Enum.TryParse(Console.ReadLine().ToLower(), out Orders.delivery deliveryMethod))
                    {
                        var newOrder = new Orders(orderPizzas, DateTime.Now, deliveryMethod, user);
                        Constants.ORDERSLIST.Add(newOrder);
                        Console.WriteLine($"Order placed successfully! Total price: {newOrder.getTotalPrice()} lei");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No pizza available.");
        }
    }
}

