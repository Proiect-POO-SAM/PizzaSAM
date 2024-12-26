/* Participanti: Covaciu Sebastian-Adelin, Crisan Alex-Florin , Ignat Mihai-Alexandru */
using ProiectPOoSAM.Alex;
using ProiectPOOSAM;
using System.ComponentModel;
using System.Reflection;
using ProiectPOoSAM;


Console.WriteLine("Daca vezi asta ruleaza programul");
USER u1 = new USER("USER", "1234", "+40711111111", USER.Role.Client);
Ingredients ingredients = new Ingredients("Mozzarella", 100, 10);
Ingredients ingredients1 = new Ingredients("Sunca", 100, 10);
Ingredients ingredients2 = new Ingredients("Ciuperci", 100, 10);
List<Ingredients> ingredientsList = new List<Ingredients>();
ingredientsList.Add(ingredients);
ingredientsList.Add(ingredients1);
ingredientsList.Add(ingredients2);
Pizza p1 = new Pizza("Quattro Stagioni", ingredientsList ,false);
Pizza p2 = new Pizza("Margherita",Pizza.Dimensiune.small, ingredientsList, false);
List<Pizza> pizzas = new List<Pizza>();
pizzas.Add(p1);
pizzas.Add(p2);
Orders o1 = new Orders(pizzas, Orders.delivery.Home, 100, u1);
Menu menu = new Menu();
menu.AddPizza(p1);
FileTXT file = new FileTXT();
file.addCommandToFile(file.breakToPiecesOrders(o1));
file.addCommandToFile(file.breakToPiecesIngredients(ingredients));
file.addCommandToFile(file.breakToPiecesIngredients(ingredients1));
file.addCommandToFile(file.breakToPiecesIngredients(ingredients2));
file.addCommandToFile(file.breakToPiecesPizza(p1));
file.addCommandToFile(file.breakToPiecesPizza(p2));
file.addCommandToFile(file.breakToPiecesMenu(menu));
file.addCommandToFile(file.breakToPiecesUser(u1));
//test read from file
o1.ViewMyCommands("USER");
var elements = new string[100];
file.gettingInformation("INGREDIENT");
file.gettingInformation("USER");
file.gettingInformation("ORDER");
file.gettingInformation("PIZZA");
o1.ViewMyCommands("USER");
file.deleteFile();



/// o clasa "partial" este o clasa care are definitia impartita pe mai multe clase.
/// practic sunt bucati de puzzle pt o clasa mai mare 
public partial class Program : Project
{
    static void Main(string[] args)
    {
        INIT();
    }
}