using Spectre.Console;
using System;
using System.Data;
using TICTACTOE_MIDTERM.styling;

namespace TICTACTOE_MIDTERM
{
    internal class Game
    {

        PvComputer pvComp;
        PrintFormat printFormat = new PrintFormat();
        Display display = new Display();
        Pvp pvp;
        Menu menu;

        public Game(MenuOption menuOption) 
        {
            pvp = new Pvp(this);
            pvComp = new PvComputer(this);
            menu = new Menu(menuOption, this);
        }

        public void playGame()
        {

            int playChoice;
            bool isValid;

            do
            {

                Console.Clear();
                display.paddingTop();
                printFormat.printCenter("1.Player vs. Player");
                printFormat.printCenter("2.Play with Computer");
                printFormat.printCenter("3.Back to Main Menu");
                Console.WriteLine();

                printFormat.print("Enter choice: ");
                string choice = Console.ReadLine();
                isValid = int.TryParse(choice, out playChoice) && (playChoice >= 1 && playChoice <= 3);

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

            } while(!isValid);


            Console.Clear();

            if (playChoice == 1)
            {
                pvp.pvp();
            }
            else if (playChoice == 2)
            {
                pvComp.playComputer();
            }
            else if (playChoice == 3)
            {
                menu.displayMenu();
            }

        }
    }
}
