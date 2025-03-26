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
            printFormat.printCenter("HOW TO PLAY");
            printFormat.printCenter("1. The game is played on a grid that's 3 squares by 3 squares.");

            Console.Write("BACK TO MENU (PRESS ANY KEY)");
            Console.ReadKey();
            Console.Clear();
            menu.displayMenu();
        }

        public void Developers()
        {
            printFormat.printCenter("DEVELOPERS OF THE GAME");
            Console.WriteLine();
            printFormat.printCenter("============================");

            Console.WriteLine();

            printFormat.printCenterRed("ALLEN PAUL BELARMINO");
            printFormat.printCenterRed("----------------------------");
            printFormat.printCenterRed("A Computer engineering student and an");
            printFormat.printCenterRed("aspiring software developer with a");
            printFormat.printCenterRed("strong interest in game mechanics and");
            printFormat.printCenterRed("innovative technologies. Constantly");
            printFormat.printCenterRed("exploring new ways to improve gameplay.");

            Console.WriteLine();

            printFormat.printCenterBlue("VINCE ROSALIJOS");
            printFormat.printCenterBlue("----------------------------");
            printFormat.printCenterBlue("A Computer engineering student with a");
            printFormat.printCenterBlue("passion for software development and");
            printFormat.printCenterBlue("game mechanics. Always exploring new");
            printFormat.printCenterBlue("technologies and innovative ideas.");

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
            Console.WriteLine();

            Console.Write("BACK TO MENU (PRESS ANY KEY)");
            Console.ReadKey();
            Console.Clear();
            menu.displayMenu();
        }

    }
}
