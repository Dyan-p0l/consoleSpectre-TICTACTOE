using System;
using TICTACTOE_MIDTERM.styling;

namespace TICTACTOE_MIDTERM
{
    internal class Game
    {
        PrintFormat printFormat = new PrintFormat();
        public void playGame()
        {

            printFormat.printCenter("1.Player vs. Player");
            printFormat.printCenter("2.Play with Computer");

            printFormat.print("Enter choice: ");
            int playChoice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (playChoice == 1)
            {
                
            }
            else if (playChoice == 2)
            {
                
            }

        }
    }
}
