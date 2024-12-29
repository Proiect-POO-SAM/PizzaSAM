using Microsoft.VisualBasic.CompilerServices;
using ProiectPOOSAM;
using ProiectPOoSAM.Alex;


namespace ProiectPOoSAM.Mihai;


public class RaportTotalVenituri : Orders
{
    private USER user;
    USER.Role role;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    private decimal VenitTotal;
    
    public RaportTotalVenituri(List<Pizza> pizzas, DateTime dateTime, decimal totalPrice, USER user, DateTime startDate, DateTime endDate) : base(pizzas, dateTime, totalPrice, user)
    {
        this.user = user ?? throw new ArgumentNullException(nameof(user));
        this.role = role;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.VenitTotal = 0;
        if (role == USER.Role.Admin)
        {
            CalculateVenitTotal();
        }
        else Console.WriteLine("Nu aveti permisiunea necesara.");
    }

    private void CalculateVenitTotal()
    {
        VenitTotal = AllOrdersPrice();   
        Console.WriteLine($"Venitul total pentru perioada specificată este: {VenitTotal}");

    }
}