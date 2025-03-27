using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using TICTACTOE_MIDTERM;

internal class Program
{
    private static void Main(string[] args)
    {

        MenuOption menuOption = new MenuOption();

        Menu menu = new Menu(menuOption);

        menuOption.SetMenu(menu);

        menu.displayMenu();
    }
}