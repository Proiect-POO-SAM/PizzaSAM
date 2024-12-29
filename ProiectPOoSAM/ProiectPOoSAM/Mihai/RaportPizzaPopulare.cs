using System.Linq.Expressions;
using Microsoft.VisualBasic.CompilerServices;
using ProiectPOOSAM;
using ProiectPOoSAM.Alex;

namespace ProiectPOoSAM.Mihai;

public abstract class RaportPizzaPopulare : Orders
{
    private USER user;
    USER.Role role;
    private Dictionary<string, int> PizzaPopularity;

    protected RaportPizzaPopulare(List<Pizza> pizzas, delivery deliveryMethod, decimal totalPrice, USER user) : base(pizzas, deliveryMethod, totalPrice, user)
    {
        this.user = user ?? throw new ArgumentNullException(nameof(user));
    }

    public string getPizzaPopularity()
    {
        if (role != USER.Role.Admin)
        {
            Console.WriteLine("Nu aveti permisiunea necesara.");
            return "Access denied.Not administrator.";
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