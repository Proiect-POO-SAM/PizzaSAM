using ProiectPOOSAM;
namespace ProiectPOoSAM.Alex
{
    // ADD  / REMOVE  / CHANGE
    public class ARC
    {
       

        // ================== ORDERS ==================
        public void addOrder(Orders order, List<Orders> ORDERSLIST, FileTXT file)
        {
            ORDERSLIST.Add(order);
            file.addCommandToFile(file.breakToPiecesOrders(order));
        }
        public void removeOrder(Orders order, List<Orders> ORDERSLIST, FileTXT file)
        {
            foreach(Orders o in ORDERSLIST)
            {
                if (o.getOrderID() == order.getOrderID())
                {
                    ORDERSLIST.Remove(o);
                    Console.WriteLine($"Order#{order.getOrderID()}  has been removed");
                    break;
                }
                else
                {
                    Console.WriteLine($"Order#{order.getOrderID()} has not been found");
                    return;
                }
            }
            string[] lines = file.readCommandsFromFile();
            foreach(string line in lines)
            {
                if (line.Contains(order.getOrderID().ToString()))
                {
                    file.removeCommandFromFile(line);
                    break;
                }

            }
        }
        public void viewOrders(List<Orders> ORDERSLIST)
        {
            foreach (Orders order in ORDERSLIST)
            {
                Console.WriteLine(order);
            }
        }

        // ================== PIZZA ==================
        public void addPizza(Pizza pizza, List<Pizza> PIZZALIST, FileTXT file)
        {
            PIZZALIST.Add(pizza);
            file.addCommandToFile(file.breakToPiecesPizza(pizza));

        }
        public void removePizza(Pizza pizza, List<Pizza> PIZZALIST, FileTXT file)
        {
            foreach (Pizza p in PIZZALIST)
            {
                if(p.pizzaID == pizza.pizzaID)
                {
                    PIZZALIST.Remove(p);
                    Console.WriteLine($"Pizza#{pizza.pizzaID}  has been removed");
                    break;
                }
                else
                {
                    Console.WriteLine($"Pizza#{pizza.pizzaID} has not been found");
                    return;
                }
            }
            string[] lines = file.readCommandsFromFile();
            foreach (string line in lines)
            {
                if (line.Contains(pizza.pizzaID.ToString()))
                {
                    file.removeCommandFromFile(line);
                    break;
                }

            }
        }
        public void viewPizza(List<Pizza> PIZZALIST)
        {
            foreach (Pizza pizza in PIZZALIST)
            {
                Console.WriteLine(pizza);
            }
        }
        // ================== INGREDIENTS LIST ==================
        public void addIngredient(Ingredients ingredient, List<Ingredients> INGREDIENTSLIST, FileTXT file)
        {
            INGREDIENTSLIST.Add(ingredient);
            file.addCommandToFile(file.breakToPiecesIngredients(ingredient));
        }
        public void removeIngredient(Ingredients ingredient, List<Ingredients> INGREDIENTSLIST, FileTXT file)
        {
            foreach (Ingredients i in INGREDIENTSLIST)
            {
                if(i.getIngredientID() == ingredient.getIngredientID())
                {
                    INGREDIENTSLIST.Remove(i);
                    Console.WriteLine($"Ingredient#{ingredient.getIngredientID()}  has been removed");
                    break;
                }
                else
                {
                    Console.WriteLine($"Ingredient#{ingredient.getIngredientID()} has not been found");
                    return;
                }
            }
            string[] lines = file.readCommandsFromFile();
            foreach (string line in lines)
            {
                if (line.Contains(ingredient.getIngredientID().ToString()))
                {
                    file.removeCommandFromFile(line);
                    break;
                }

            }
        }
        public void viewIngredients(List<Ingredients> INGREDIENTSLIST)
        {
            foreach (Ingredients ingredient in INGREDIENTSLIST)
            {
                Console.WriteLine(ingredient);
            }
        }

        // ================== USERS ==================
        public void addUser(USER user, List<USER> USERLIST, FileTXT file)
        {
            USERLIST.Add(user);
            file.addCommandToFile(file.breakToPiecesUser(user));
        }
        public void removeUser(USER user, List<USER> USERLIST, FileTXT file)
        {
            foreach (USER u in USERLIST)
            {
                if (u.GetUsername() == user.GetUsername())
                {
                    USERLIST.Remove(u);
                    break;
                }
            }
            string[] lines = file.readCommandsFromFile();
            foreach (string line in lines)
            {
                if (line.Contains(user.GetUsername()))
                {
                    file.removeCommandFromFile(line);
                    break;
                }

            }
        }
        public void viewUsers(List<USER> USERLIST)
        {
            foreach (USER user in USERLIST)
            {
                user.showUserDetails();
            }
        }

        


    }
}
