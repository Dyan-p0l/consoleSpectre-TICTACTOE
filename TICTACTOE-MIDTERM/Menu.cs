using System;
using TICTACTOE_MIDTERM.styling;
using Spectre.Console;

namespace TICTACTOE_MIDTERM
{

    internal class Menu
    {

        private MenuOption menuOption;

        public Menu(MenuOption menuOption)
        {
            this.menuOption = menuOption;
        }   

        public void displayMenu()
        {

            Display display = new Display();
            Game game = new Game();
            PrintFormat printFormat = new PrintFormat();

            display.paddingTop();

            printFormat.printCenter("==+==+==+==+==+==+==");
            printFormat.printCenter("    TIC - TAC- TOE  ");
            printFormat.printCenter("==+==+==+==+==+==+==");
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
