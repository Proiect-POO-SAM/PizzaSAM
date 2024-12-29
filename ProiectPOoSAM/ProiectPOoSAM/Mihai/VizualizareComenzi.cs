using System;
using System.Collections.Generic;
using Azure.Core;
using ProiectPOOSAM;
using ProiectPOoSAM.Alex;

namespace ProiectPOoSAM.Mihai;

public class VizualizareComenzi
{
    USER user;
    USER.Role Role;
    private DateTime Data;

    private List<Orders> FilterOrdersByDate(List<Orders> orders, DateTime date)
    {
        return orders.Where(order => order.date == date.Date).ToList();
    }

    public VizualizareComenzi(USER user, USER.Role role, List<Orders> orders, DateTime data)
    {
        this.user = user ?? throw new ArgumentNullException(nameof(user));
        this.Role = role;
        this.Data = data;


        if (role != USER.Role.Admin)
        {
            Console.WriteLine("Nu aveti permisiunea sa vizualizati comenzile.");
            return;
        }

        var filteredOrders = FilterOrdersByDate(orders, data);

        if (filteredOrders.Any())
        {
            Console.WriteLine($"Comenzile pentru data {data.ToShortDateString()}:");
            foreach (var order in filteredOrders)
            {
                Console.WriteLine(order.ToString());
            }
        }
        else
        {
            Console.WriteLine($"Nu exista comenzi pentru data {data.ToShortDateString()}.");
        }
    }

}