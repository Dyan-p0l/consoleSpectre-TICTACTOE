using Spectre.Console;
using System;
using System.Data;
using TICTACTOE_MIDTERM.styling;

namespace TICTACTOE_MIDTERM
{
    internal class Game
    {
        PvComputer pvComp = new PvComputer();
        PrintFormat printFormat = new PrintFormat();
        Pvp pvp;

        public Game() 
        {
            pvp = new Pvp(this);
        }

        public void playGame()
        {
            Console.Clear();
            printFormat.printCenter("1.Player vs. Player");
            printFormat.printCenter("2.Play with Computer");

            printFormat.print("Enter choice: ");
            int playChoice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (playChoice == 1)
            {
                pvp.pvp();
            }
            else if (playChoice == 2)
            {
                pvComp.playComputer();
            }

        }
    }
}
