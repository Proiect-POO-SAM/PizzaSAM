using ProiectPOoSAM.Alex;
using ProiectPOOSAM;

namespace ProiectPOoSAM;

public static class Constants
{
    // ================== FILE PATH ==================
    public static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MenuSource.txt");
    // ================== VARIABLES ==================
    public static int TVA = 19 / 100;
    public static int pizzaCount = 0;
    public static int ingredientCount = 0;
    public static int orderCount = 0;
    public static int menuCount = 0;
    // ================== LISTS ==================
    public static List<Ingredients> INGREDIENTSLIST = new List<Ingredients>();
    public static List<Pizza> PIZZASLIST = new List<Pizza>();
    public static List<Orders> ORDERSLIST = new List<Orders>();
    public static List<USER> USERLIST = new List<USER>();
    public static FileTXT file = new FileTXT();
    // ================== USER LISTS ==================
    public static List<Pizza> PIZZALISTUSER = new List<Pizza>();
    public static int pizzaCountUser = 0;
    public static Menu MENU = new Menu();
}
