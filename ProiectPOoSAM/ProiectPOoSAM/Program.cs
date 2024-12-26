/* Participanti: Covaciu Sebastian-Adelin, Crisan Alex-Florin , Ignat Mihai-Alexandru */
using ProiectPOoSAM.Alex;
using ProiectPOOSAM;
using System.ComponentModel;
using System.Reflection;


Console.WriteLine("Daca vezi asta ruleaza programul");
USER u1 = new USER("USER", "1234", "+40711111111", USER.Role.Client);
Ingredients ingredients = new Ingredients("Mozzarella", 100, 10);
Ingredients ingredients1 = new Ingredients("Sunca", 100, 10);
Ingredients ingredients2 = new Ingredients("Ciuperci", 100, 10);
List<Ingredients> ingredientsList = new List<Ingredients>();
ingredientsList.Add(ingredients);
ingredientsList.Add(ingredients1);
ingredientsList.Add(ingredients2);
Pizza p1 = new Pizza("Quattro Stagioni", 10, ingredientsList,false);
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
