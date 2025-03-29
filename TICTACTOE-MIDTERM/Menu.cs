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
            
            bool isValid;
            int menuChoice;

            do {
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
                printFormat.printCenterYellow("4.EXIT");
                Console.WriteLine();
                printFormat.print("Enter choice (1-3): ");
                string choice = Console.ReadLine();
                isValid = int.TryParse(choice, out menuChoice) && (menuChoice >= 1 && menuChoice <= 4);

                if (!isValid)
                {
                    printFormat.printCenterRed("Invalid Input. Please try again");
                    Console.WriteLine();
                    Console.WriteLine();
                    printFormat.printCenterRed("Press any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.Clear();
            }
            while (!isValid);

            Console.Clear();

            switch (menuChoice)
            {
                case 1:
                    game.playGame();
                    break;
                case 2:
                    menuOption.howToPlay();
                    break;
                case 3:
                    menuOption.Developers();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }

        }
    }
}
