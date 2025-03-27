using System;
using TICTACTOE_MIDTERM.styling;
using Spectre.Console;

namespace TICTACTOE_MIDTERM
{

    internal class Menu
    {

        private MenuOption menuOption;
        private Game game;
        public Menu(MenuOption menuOption, Game game)
        {
            this.menuOption = menuOption;
            this.game = game;
        }

       

        public void displayMenu()
        {

            Display display = new Display();
            PrintFormat printFormat = new PrintFormat();

            display.paddingTop();

            AnsiConsole.Write(
            new FigletText("TIC-TAC-TOE")
            .Centered()
            .Color(Color.Orange3));

            Console.WriteLine();
            Console.WriteLine();
            printFormat.printCenter("1.PLAY GAME");
            printFormat.printCenterRed(" 2.HOW TO PLAY");
            printFormat.printCenterGreen("3.DEVELOPERS");
            Console.WriteLine();
            printFormat.print("Enter choice: ");
            int menuChoice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            if (menuChoice == 1)
            {
               game.playGame();
            }
            else if (menuChoice == 2)
            {
               menuOption.howToPlay();
            }
            else if (menuChoice == 3)
            {
                menuOption.Developers();
            }

        }
    }
}
