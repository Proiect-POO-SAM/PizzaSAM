using System.Linq.Expressions;
using Microsoft.VisualBasic.CompilerServices;
using ProiectPOOSAM;
using ProiectPOoSAM.Alex;

namespace ProiectPOoSAM.Mihai;

public class RaportPizzaPopulare : Orders
{
    private USER user;
    private Dictionary<string, int> PizzaPopularity;
    private DateTime DateTime;
    public RaportPizzaPopulare(List<Pizza> pizzas,DateTime dateTime, delivery deliveryMethod, USER user) : base(pizzas,dateTime,deliveryMethod,user)
    {
        this.user = user ?? throw new ArgumentNullException(nameof(user));
        PizzaPopularity = new Dictionary<string, int>();
    }

    public string getPizzaPopularity()
    {
        if (user.GetRole() is "Client")
        {
            Console.WriteLine("You do not have permission to view the orders.");
            return "Access denied";
        }

        PizzaPopularity = new Dictionary<string, int>();

        try
        {
            foreach (var order in Constants.ORDERSLIST)
            {
                foreach (var pizza in order.getPizzas())
                {
                    if (PizzaPopularity.ContainsKey(pizza.getName()))
                    {
                        PizzaPopularity[pizza.getName()]++;
                    }
                    else
                    {
                        PizzaPopularity[pizza.getName()] = 1;
                    }
                }
            }

            var sortedPizzaPopularity = PizzaPopularity.OrderByDescending(p => p.Value);
            Console.WriteLine("\nRaportul celor mai comandate pizza:");
            foreach (var entry in sortedPizzaPopularity)
            {
                Console.WriteLine($"Pizza: {entry.Key}, Număr comenzi: {entry.Value}");
            }

            return "Raportul a fost creat cu succes.";
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "Error: " + ex.Message;
        }
    }
}