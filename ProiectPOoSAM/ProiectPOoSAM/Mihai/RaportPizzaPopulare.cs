using System.Linq.Expressions;
using Microsoft.VisualBasic.CompilerServices;
using ProiectPOOSAM;
using ProiectPOoSAM.Alex;

namespace ProiectPOoSAM.Mihai;

public class RaportPizzaPopulare : Orders
{
    private USER user;
    USER.Role role;
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
            
        }

        PizzaPopularity = new Dictionary<string, int>();

        try
        {
            foreach (var pizza in pizzas)
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
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "Error: " + ex.Message;
        }
        var sortedPizzaPopularity = PizzaPopularity.OrderByDescending(p => p.Value);
        Console.WriteLine("Raportul celor mai populare pizza:");
        foreach (var entry in sortedPizzaPopularity)
        {
            Console.WriteLine($"Pizza: {entry.Key}, Comenzi: {entry.Value}");
        }

        return "The raport was succesfully created.";
    }
}