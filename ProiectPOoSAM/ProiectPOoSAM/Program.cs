﻿/* Participanti: Covaciu Sebastian-Adelin, Crisan Alex-Florin , Ignat Mihai-Alexandru */
using Microsoft.VisualBasic;
using ProiectPOoSAM.Alex;
using ProiectPOOSAM;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

List<Ingredients> INGREDIENTSLIST = new List<Ingredients>();
List<Pizza> PIZZASLIST = new List<Pizza>();
List<Orders> ORDERSLIST = new List<Orders>();



Console.WriteLine("Daca vezi asta ruleaza programul");
USER u1 = new USER("USER", "1234", "+40711111111", USER.Role.Admin);
USER u2 = new USER("USER2", "12345", "+407222222232", USER.Role.Client);
Ingredients ingredients = new Ingredients("Mozzarella", 100, 10);
Ingredients ingredients1 = new Ingredients("Sunca", 100, 10);
Ingredients ingredients2 = new Ingredients("Ciuperci", 100, 10);
List<Ingredients> ingredientsl = new List<Ingredients>();
ingredientsl.Add(ingredients);
ingredientsl.Add(ingredients1);
ingredientsl.Add(ingredients2);
Pizza p1 = new Pizza("Quattro Stagioni", 0, ingredientsl, false);
Pizza p2 = new Pizza("Patrocle", 0, ingredientsl, false);
Pizza p3 = new Pizza("Quattro Stagioni", 0, ingredientsl, false);
Pizza p4 = new Pizza("Dominic", ingredientsl, false);
Pizza p5 = new Pizza("Diavola", ingredientsl, false);
List<Pizza> pizzas22 = new List<Pizza>();
pizzas22.Add(p1);
pizzas22.Add(p2);
pizzas22.Add(p3);
pizzas22.Add(p4);
pizzas22.Add(p5);
List<Pizza> pizzas1 = new List<Pizza>();
pizzas1.Add(p1);
pizzas1.Add(p1);
pizzas1.Add(p1);
pizzas1.Add(p1);
pizzas1.Add(p1);
pizzas1.Add(p2);
pizzas1.Add(p2);
pizzas1.Add(p3);
pizzas1.Add(p3);
pizzas1.Add(p4);
pizzas1.Add(p5);
List<Pizza> pizzas2 = new List<Pizza>();
pizzas2.Add(p1);
pizzas2.Add(p1);
pizzas2.Add(p1);
pizzas2.Add(p2);
pizzas2.Add(p2);
pizzas2.Add(p2);
Menu menu = new Menu();
Orders o1 = new Orders(new List<Pizza> { p1, p2, p2, p2, p2, p1, p3, p3, p4 },new DateTime(2025/01/04), 0, u1);
ORDERSLIST.Add(o1);
Console.WriteLine(o1);
o1.GetTotalIncome(u1, DateTime.Now, DateTime.Now.AddDays(1),ORDERSLIST);
//Orders o2 = new Orders(new List<Pizza> { p3, p4 }, DateTime.Now, 0, u1);
//Orders o3 = new Orders(new List<Pizza> { p5 }, DateTime.Now, 0, u1);
//Orders o4 = new Orders(new List<Pizza> { p1, p2 }, DateTime.Now, 0, u1);
//o1.feedbackOrder("Buna", "5");
//RaportPizzaPopulare pop = new RaportPizzaPopulare(pizzas, 0, u1);
//RaportPizzaPopulare pop1 = new RaportPizzaPopulare(pizzas1, 0, u1);
//RaportPizzaPopulare pop2 = new RaportPizzaPopulare(pizzas2, 0, u1);
//Console.WriteLine("\n");
//pop.getPizzaPopularity();
//Console.WriteLine("\n");
//pop1.getPizzaPopularity();
//Console.WriteLine("\n");
//pop2.getPizzaPopularity();
//ViewOrders ord = new ViewOrders(u1, USER.Role.Admin, new List<Orders> { o1, o2, o3, o4 }, DateTime.Now);

//o1.GetTotalIncome(u1,DateTime.Now,DateTime.Now);
//FileTXT file = new FileTXT();
//file.addCommandToFile(file.breakToPiecesOrders(o1));
//file.addCommandToFile(file.breakToPiecesOrders(o2));
//file.addCommandToFile(file.breakToPiecesOrders(o3));
//file.addCommandToFile(file.breakToPiecesOrders(o4));
//file.addCommandToFile(file.breakToPiecesIngredients(ingredients));
//file.addCommandToFile(file.breakToPiecesIngredients(ingredients1));
//file.addCommandToFile(file.breakToPiecesIngredients(ingredients2));
//file.addCommandToFile(file.breakToPiecesPizza(p1));
//file.addCommandToFile(file.breakToPiecesPizza(p2));
//file.addCommandToFile(file.breakToPiecesMenu(menu));
//file.addCommandToFile(file.breakToPiecesUser(u1));

//file.gettingInformation("ORDER");
//file.gettingInformation("PIZZA");
//file.gettingInformation("INGREDIENT");
//file.gettingInformation("MENU");
//file.gettingInformation("USER");

//file.deleteFile();


/*
namespace ProiectPOoSAM;
public partial class Program : ProjectWrap
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Introdu date de test\n username: opel\n password: astra");

        DateTime today = DateTime.Today;
        TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
        
        string initStamp = today.ToString() + " " + now.ToString();
        WriteIntoLogger(initStamp);
        
        var initResult = Project.INIT();

        if (initResult.user != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nBun venit, {initResult.user.GetUsername()}!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Request failed.");
        }
        Console.ResetColor();

        WriteIntoLogger(initResult.Message);

        var unloadResult = Project.UNLOAD();
        WriteIntoLogger(unloadResult.Message);
    }
}

public class ProjectWrap 
{
    public static void WriteIntoLogger(string message)
    {
        const string path = "Logger.txt";

        using (var Writer = File.AppendText(path))
        {
            Writer.WriteLine(message);
        }
    }
}
*/