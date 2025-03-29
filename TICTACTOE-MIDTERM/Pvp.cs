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
        string playerWinner = "";
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
            table.BorderColor(Color.Yellow1);
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
            table2.BorderColor(Color.Yellow1);
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
            .Border(BoxBorder.Double)
            .Header($"   [bold yellow]ROUND {round}[/]   ", Justify.Center);

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
            bool isValid;
            int move;
            while (isRunning)
            {

                if (player1Score >= goalScore || player2Score >= goalScore)
                {

                    playerWinner = (player1Score > player2Score) ? play1 : play2;
                    break;

                }

                do
                {
                    Console.Clear();
                    displayBoard(play1, play2);
                    Console.WriteLine();
                    printFormat.printCenter($"{currentPlayer}'s turn");
                    printFormat.print("Enter coordinate to place move (0-8): ");
                    string placeMove = Console.ReadLine();
                    isValid = int.TryParse(placeMove, out move) && (move >= 0 && move <= 8); 
                    if (!isValid)
                    {
                        printFormat.printCenterRed("Invalid move!!");
                        printFormat.printCenterRed("Press any key to try again.");
                        Console.ReadKey();
                    }
                } while (!isValid);

                int row = move / 3;
                int col = move % 3;

                if (board[row, col] == ' ')
                {
                    board[row, col] = currentPlayer;
                    if (CheckWin(currentPlayer))
                    {
                        Console.Clear();
                        displayBoard(play1, play2);
                        printFormat.printCenter($"{currentPlayer} WINS THIS ROUND!");
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
                        displayBoard(play1, play2);
                        printFormat.printCenter("IT'S A DRAW FOR THIS ROUND!");
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
                    printFormat.printCenter("Cell already occupied.");
                    printFormat.print("Press any key to continue");
                    Console.ReadKey();

                }
            }

        }

        public void pvp()
        {
            bool isNotEmpty1, isNotEmpty12  = false;
            string player1 = "", player2 = "";
            bool isValid = true;
            int choice;

            do
            {
                display.paddingTop();
                printFormat.print("Enter player1 (X) name: ");
                player1 += Console.ReadLine();
                isNotEmpty1 = !string.IsNullOrEmpty(player1);
                if (!isNotEmpty1)
                {
                    printFormat.printCenterRed("Field must not be empty");
                    printFormat.printCenterRed("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    printFormat.print("Enter player2 (O) name: ");
                    player2 += Console.ReadLine();
                    check:
                    isNotEmpty12 = !string.IsNullOrEmpty(player2);
                    if (!isNotEmpty12)
                    {

                        printFormat.printCenterRed("Field must not be empty");
                        printFormat.printCenterRed("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        display.paddingTop();
                        printFormat.print("Enter player1 (X) name: " + player1 + "\n");
                        printFormat.print("Enter player2 (O) name: ");
                        player2 += Console.ReadLine();
                        goto check;
                    }
                }
                Console.Clear();
            } while (!isNotEmpty1 && !isNotEmpty12);

            display.paddingTop();

            mainLoop(player1, player2);

            do
            {

                Console.Clear();
                displayBoard(player1, player2);
                printFormat.printCenter($"{playerWinner} WINS!!!");
                Console.WriteLine();
                printFormat.printCenter("THE GAME IS OVER");
                Console.WriteLine();
                printFormat.printCenter("1.Play again (Rematch)");
                printFormat.printCenter("2.BACK TO GAME MENU");
                printFormat.print("Enter choice: ");
                string option = Console.ReadLine();
                isValid  = int.TryParse(option, out choice) && (choice >= 1 && choice <= 2);
                if (!isValid)
                {
                    printFormat.printCenterRed("Invalid input, try again.");
                    printFormat.printCenterRed("Press any key to continue...");
                    Console.ReadKey();
                }
            } while (!isValid);

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
