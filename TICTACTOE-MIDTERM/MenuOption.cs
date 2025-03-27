using Spectre.Console;
using System;
using TICTACTOE_MIDTERM.styling;

namespace TICTACTOE_MIDTERM
{
    internal class MenuOption
    {
        private Menu menu;
        Display display = new Display();
        PrintFormat printFormat = new PrintFormat();    

        public MenuOption()
        {
            
        }

        public void SetMenu(Menu menu)
        {
            this.menu = menu;
        }

        public void howToPlay()
        {
            AnsiConsole.Write(
            new FigletText("HowToPlay")
            .Centered()
            .Color(Color.Red3));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            printFormat.printCenter("BASIC RULES");
            printFormat.printCenter("------------------------------------------");
            Console.WriteLine();
            printFormat.printCenterRed("1. The game is played on a grid that's 3 squares by 3 squares.");
            printFormat.printCenterRed("        2. Players take turns placing their marks (X or O) in an empty square.");
            printFormat.printCenterRed("         3. The first player to get three of their marks in a row (horizontally,");
            printFormat.printCenterRed("   vertically, or diagonally) wins the game.");
            printFormat.printCenterRed("   4. If all 9 squares are filled and no player has three in a row,");
            printFormat.printCenterRed("   the game is a draw.");

            Console.WriteLine();
            Console.WriteLine();

            printFormat.print("BACK TO MENU (PRESS ANY KEY)");

            Console.ReadKey();
            Console.Clear();
            menu.displayMenu();
        }

        public void Developers()
        {
            AnsiConsole.Write(
            new FigletText("DEVS")
            .Centered()
            .Color(Color.Green));

            printFormat.printCenterGreen("ALLEN PAUL BELARMINO");
            printFormat.printCenterGreen("----------------------------");
            printFormat.printCenterGreen("A Computer engineering student and an");
            printFormat.printCenterGreen("aspiring software developer with a");
            printFormat.printCenterGreen("strong interest in game mechanics and");
            printFormat.printCenterGreen("innovative technologies. Constantly");
            printFormat.printCenterGreen("exploring new ways to improve gameplay.");

            Console.WriteLine();

            printFormat.printCenterRed("JOHN PAUL RAYCO");
            printFormat.printCenterRed("----------------------------");
            printFormat.printCenterRed("A Computer Engineering student");
            printFormat.printCenterRed("and an aspiring software developer");
            printFormat.printCenterRed("with a passion for algorithms and");
            printFormat.printCenterRed("problem-solving. Enjoys optimizing");
            printFormat.printCenterRed("code to create efficient and engaging");
            printFormat.printCenterRed("gameplay mechanics.");

            Console.WriteLine();

            printFormat.printCenterYellow("VINCE ROSALIJOS");
            printFormat.printCenterYellow("----------------------------");
            printFormat.printCenterYellow("A Computer engineering student with a");
            printFormat.printCenterYellow("passion for software development and");
            printFormat.printCenterYellow("game mechanics. Always exploring new");
            printFormat.printCenterYellow("technologies and innovative ideas.");


            printFormat.print("BACK TO MENU (PRESS ANY KEY)");
            Console.ReadKey();
            Console.Clear();
            menu.displayMenu();
        }

    }
}
