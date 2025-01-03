using Microsoft.Identity.Client;
using ProiectPOOSAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPOoSAM.Alex
{
    // ADD  / REMOVE  / CHANGE
    public class ARC
    {
        // ================== ORDERS ==================
        public void addOrder(Orders order, List<Orders> ORDERSLIST)
        {
            ORDERSLIST.Add(order);
        }
        public void removeOrder(Orders order, List<Orders> ORDERSLIST)
        {
            foreach (Orders o in ORDERSLIST)
            {
                if (o.getUsername() == order.getUsername())
                {
                    ORDERSLIST.Remove(o);
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
        public void addPizza(Pizza pizza, List<Pizza> PIZZALIST)
        {
            PIZZALIST.Add(pizza);
        }
        public void removePizza(Pizza pizza, List<Pizza> PIZZALIST)
        {
            foreach (Pizza p in PIZZALIST)
            {
                if (p.getName() == pizza.getName())
                {
                    PIZZALIST.Remove(p);
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
        // ================== INGREDIENTS ==================
        public void addIngredient(Ingredients ingredient, List<Ingredients> INGREDIENTSLIST)
        {
            INGREDIENTSLIST.Add(ingredient);
        }
        public void removeIngredient(Ingredients ingredient, List<Ingredients> INGREDIENTSLIST)
        {
            foreach (Ingredients i in INGREDIENTSLIST)
            {
                if (i.getName() == ingredient.getName())
                {
                    INGREDIENTSLIST.Remove(i);
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




    }
}
