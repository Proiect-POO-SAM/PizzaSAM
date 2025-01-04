using ProiectPOoSAM.Alex;

namespace ProiectPOoSAM;

public static class Constants
{
    public static readonly string filePath = "MenuSource.txt";
    public static int TVA = 19 / 100;
    public static int userCount = 0;
    public static int pizzaCount = 0;
    public static int ingredientCount = 0;
    public static int orderCount = 0;
    public static int menuCount = 0;
    public static List<Ingredients> INGREDIENTSLIST = new List<Ingredients>();
    public static List<Pizza> PIZZASLIST = new List<Pizza>();
    public static List<Orders> ORDERSLIST = new List<Orders>();
    public static FileTXT file = new FileTXT();
}
