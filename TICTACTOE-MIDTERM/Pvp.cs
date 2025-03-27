using Spectre.Console;
using System;
using TICTACTOE_MIDTERM.styling;
using static System.Formats.Asn1.AsnWriter;

namespace TICTACTOE_MIDTERM
{
    internal class Pvp
    {
        
        PrintFormat printFormat = new PrintFormat();
        Display display = new Display(); 
        
        Game game;
        public Pvp(Game newGame)
        {
            this.game = newGame;
        }

        int player1Score = 0;
        int player2Score = 0;
        int goalScore = 3;
        int round = 1;
        string playerWinner;
        char currentPlayer = 'X';
        bool isRunning = true;

        static char[,] board =
        {
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' }
        };

        static string GetColoredSymbol(char symbol)
        {
            return symbol == 'X' ? "[cyan]X[/]" :
                   symbol == 'O' ? "[red]O[/]" : " ";
        }

        void displayBoard(string player1,string player2)
        {
            
            var table = new Table();
            table.ShowRowSeparators();
            table.Title("[bold yellow]MAIN BOARD[/]");
            table.AddColumn(GetColoredSymbol(board[0, 0]))
             .AddColumn(GetColoredSymbol(board[0, 1]))
             .AddColumn(GetColoredSymbol(board[0, 2]));
            table.BorderColor(Color.Chartreuse1);
            table.Border(TableBorder.Double);
            
            for (int i = 1; i < 3; i++)
            {
                table.AddRow(
                    GetColoredSymbol(board[i, 0]),
                    GetColoredSymbol(board[i, 1]),
                    GetColoredSymbol(board[i, 2])
                );
            }

            //TABLE PARA COORDINATES, INSTRUCTION BA
            var table2 = new Table();
            table2.ShowRowSeparators();
            table2.Title("[bold yellow]COORDINATES[/]");
            table2.AddColumn("0").AddColumn("1").AddColumn("2");
            table2.BorderColor(Color.Chartreuse1);
            table2.AddRow("3", "4", "5");
            table2.AddRow("6", "7", "8");
            table2.Border(TableBorder.Double);

            //SCOREBOARD KATONG NAKA BARCHART
            var scoreChart = new BarChart()
            .Width(30)
            .Label("[yellow bold]SCOREBOARD[/]")
                .CenterLabel()
                .AddItem("GOAL SCORE", goalScore, Color.Yellow1)
                .AddItem(player1 + ": X", player1Score, Color.Cyan1)
                .AddItem(player2 + ": O", player2Score, Color.Red1);

            //PARA MAUSA ANG TULO KA COMPONENT
            var grid = new Grid();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();

            grid.AddRow(
                new Padder(table2).PadRight(5),  // Add spacing to the right
                new Padder(table).PadLeft(14).PadRight(6), // Centered spacing
                new Padder(scoreChart).PadLeft(3)

            );

            //PANEL SAME SA PANEL SA JAVA
            var panel = new Panel(grid)
            .BorderColor(Color.Blue)
            .Border(BoxBorder.Double);

            int screenWidth = Console.WindowWidth;
            int panelWidth = 85; // Adjust based on content width
            int paddingLeft = Math.Max((screenWidth - panelWidth) / 2, 0);

            AnsiConsole.Write(new Padder(panel).PadLeft(paddingLeft));
        }

        private bool CheckWin(char player)
        {
            
            for (int i = 0; i < 3; i++)
            {
                if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                    (board[0, i] == player && board[1, i] == player && board[2, i] == player))
                {
                    return true;
                }
            }
            if ((board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player))
            {
                return true;
            }
            return false;
        }

        private bool CheckDraw()
        {
            foreach (char cell in board)
            {
                if (cell == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        private void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        public void mainLoop(string play1, string play2)
        {
            while (isRunning)
            {

                if (player1Score >= goalScore || player2Score >= goalScore)
                {

                    playerWinner = (player1Score > player2Score) ? play1 : play2;
                    break;

                }

                Console.Clear();
                printFormat.printCenter($"ROUND {round}");
                displayBoard(play1, play2);
                Console.WriteLine();
                printFormat.printCenter($"{currentPlayer}'s turn");
                printFormat.print("Enter coordinate to place move (0-8): ");
                int move = Convert.ToInt32(Console.ReadLine());

                int row = move / 3;
                int col = move % 3;

                if (board[row, col] == ' ')
                {
                    board[row, col] = currentPlayer;
                    if (CheckWin(currentPlayer))
                    {
                        Console.Clear();
                        printFormat.printCenter($"ROUND {round}");
                        displayBoard(play1, play2);
                        printFormat.printCenter($"{currentPlayer} wins!");
                        printFormat.print("Press any key to continue");
                        Console.ReadKey();

                        if (currentPlayer == 'X')
                        {
                            player1Score++;
                        }
                        else
                        {
                            player2Score++;
                        }

                        ResetBoard();
                        round++;
                    }
                    else if (CheckDraw())
                    {
                        Console.Clear();
                        printFormat.printCenter($"ROUND {round}");
                        displayBoard(play1, play2);
                        printFormat.printCenter("It's a draw!");
                        printFormat.print("Press any key to continue");
                        Console.ReadKey();
                        ResetBoard();
                        round++;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    }
                }
                else
                {
                    printFormat.printCenter("Invalid move, try again.");
                }
            }

        }

        public void pvp()
        {
            display.paddingTop();
            printFormat.print("Enter player1 name: ");
            string player1 = Console.ReadLine();
            printFormat.print("Enter player2 name: ");
            string player2 = Console.ReadLine();
            
            mainLoop(player1, player2);

            Console.Clear();
            displayBoard(player1, player2);
            printFormat.printCenter($"{playerWinner} WINS!!!");
            printFormat.printCenter("Game Over");
            Console.WriteLine();
            printFormat.printCenter("1.Play again");
            printFormat.printCenter("2.BACK TO GAME MENU");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1) 
            {
                player1Score = 0;
                player2Score = 0;
                round = 1;
                mainLoop(player1, player2);
            }
            else if (choice == 2)
            {
                game.playGame();
            }

        }

    }
}
