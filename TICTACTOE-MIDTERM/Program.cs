using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using TICTACTOE_MIDTERM;

internal class Program
{
    private static void Main(string[] args)
    {

        MenuOption menuOption = new MenuOption();
        Game game = new Game(menuOption);
        Menu menu = new Menu(menuOption, game);

        menuOption.SetMenu(menu);

        menu.displayMenu();
    }
}