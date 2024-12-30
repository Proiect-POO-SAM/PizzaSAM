using System;
using System.Collections.Generic;
using Azure.Core;
using ProiectPOOSAM;
using ProiectPOoSAM.Alex;

namespace ProiectPOoSAM.Mihai;

public class ViewOrders
{
    USER user;
    USER.Role Role;
    private DateTime Data;


    private List<Orders> FilterOrdersByDate(List<Orders> orders, DateTime date)
    {
        List<Orders> filteredOrders = new List<Orders>();
        foreach (var order in orders)
        {
            if (order.date.Date == date.Date)
            {
                filteredOrders.Add(order);
            }
        }
        return filteredOrders;
    }



    public ViewOrders(USER user, USER.Role role, List<Orders> orders, DateTime data)
    {
        this.user = user ?? throw new ArgumentNullException(nameof(user));
        this.Role = role;
        this.Data = data;


        if (Role != USER.Role.Admin)
        {
            Console.WriteLine("Nu aveti permisiunea sa vizualizati comenzile.");
            return;
        }

        var filteredOrders = FilterOrdersByDate(orders, Data);

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