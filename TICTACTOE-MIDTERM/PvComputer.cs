using Spectre.Console;
using System;
using System.Numerics;
using TICTACTOE_MIDTERM.styling;


namespace TICTACTOE_MIDTERM
{
    internal class PvComputer
    {
        PrintFormat printFormat = new PrintFormat();
        Display display = new Display();

        Game game;
        public PvComputer(Game newGame)
        {
            this.game = newGame;
        }

        static string GetColoredSymbol(char symbol)
        {
            return symbol == 'X' ? "[cyan]X[/]" :
                   symbol == 'O' ? "[red]O[/]" : " ";
        }

        int player1Score = 0;
        int computerScore = 0;
        int goalScore = 3;
        int round = 1;
        string playerWinner;
        char currentPlayer = 'X';
        bool isRunning = true;
        string comp = "Computer";
        static char[,] board =
        {
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' }
        };

        void displayBoard(string player1)
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
                .AddItem(player1 + ": ", player1Score, Color.Cyan1)
                .AddItem("Computer: ", computerScore, Color.Red1);

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

        private int GetComputerMove(int difficulty)
        {
            Random random = new Random();

            if (difficulty == 1) // Easy
            {
                int move;
                do { move = random.Next(0, 9); } while (board[move / 3, move % 3] != ' ');
                return move;
            }
            else if (difficulty == 2 || difficulty == 3) // Medium and Hard
            {
                // Try to win or block opponent
                for (int i = 0; i < 9; i++)
                {
                    if (board[i / 3, i % 3] == ' ')
                    {
                        // Check for a winning move
                        board[i / 3, i % 3] = 'O';
                        if (CheckWin('O'))
                        {
                            board[i / 3, i % 3] = ' '; // Reset the board
                            return i;
                        }
                        board[i / 3, i % 3] = ' ';

                        // Check for a blocking move
                        board[i / 3, i % 3] = 'X';
                        if (CheckWin('X'))
                        {
                            board[i / 3, i % 3] = ' '; // Reset the board
                            return i;
                        }
                        board[i / 3, i % 3] = ' ';
                    }
                }
                // If no strategic move, pick random
                int move;
                do { move = random.Next(0, 9); } while (board[move / 3, move % 3] != ' ');
                return move;
            }
            return -1;
        }
        public void mainLoop(string play1, int difficulty, char playerSymbol)
        {
            
            while (isRunning)
            {
                Console.Clear();
                displayBoard(play1);
                if (player1Score >= goalScore || computerScore >= goalScore)
                {

                    playerWinner = (player1Score > computerScore) ? play1 : comp;
                    break;

                }

                int move;

                if (currentPlayer == playerSymbol)
                {
                    printFormat.printCenter($"{currentPlayer}'s turn");
                    printFormat.print("Enter coordinate to place move (0-8): ");
                    move = Convert.ToInt32(Console.ReadLine());

                }   
                else
                {
                    AnsiConsole.Status()
                    .Start("Thinking lang...", ctx =>
                    {
                        AnsiConsole.MarkupLine("Generating Computer Move...");
                        Thread.Sleep(800);

                        ctx.Spinner(Spinner.Known.Moon);
                        ctx.SpinnerStyle(Style.Parse("red"));
                        Thread.Sleep(1500);
                    });
                    move = GetComputerMove(difficulty);
                }

                int row = move / 3;
                int col = move % 3;

                if (board[row, col] == ' ')
                {
                    board[row, col] = currentPlayer;
                    
                    if (CheckWin(currentPlayer))
                    {
                        Console.Clear();
                        displayBoard(play1);
                        printFormat.printCenter($"{currentPlayer} WINS THIS ROUND!");
                        printFormat.print("Press any key to continue");
                        Console.ReadKey();
                        ResetBoard();
                        if (currentPlayer == playerSymbol)
                        {
                            player1Score++;
                            
                        }
                        else
                        {
                            computerScore++;
                        }
                        
                        round++;
                    }
                    else if (CheckDraw())
                    {
                        Console.Clear();
                        displayBoard(play1);
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
                    printFormat.printCenter("Invalid move, try again.");
                }
            }

        }

        public void playComputer()
        {

            char playerSymbol = '\0';

            display.paddingTop();
            printFormat.print("Enter player name: ");
            string player = Console.ReadLine();
            printFormat.printCenterGreen("Enter difficulty level: ");
            printFormat.printCenterGreen("1.EZ");
            printFormat.printCenterGreen("2.MID FRFR");
            printFormat.printCenterGreen("3.HARD FUCK");
            printFormat.print("Enter choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            int difficulty = 0;
            switch (choice)
            {
                case 1: difficulty += 1; 
                break;
                case 2: difficulty += 2; 
                break;
                case 3: difficulty += 3; 
                break;
            }
            Console.WriteLine();
            printFormat.printCenter("Choose SYMBOL");
            printFormat.printCenter("1. X");
            printFormat.printCenter("2. O");
            printFormat.print("Enter choice: ");
            int symbol = Convert.ToInt32(Console.ReadLine());

            playerSymbol = (symbol == 1) ? 'X' : 'O';
            currentPlayer = playerSymbol;
            Console.Clear();
            mainLoop(player, difficulty, playerSymbol);

            Console.Clear();
            displayBoard(player);
            printFormat.printCenter($"{playerWinner} WINS!!!");
            Console.WriteLine();
            printFormat.printCenter("THE GAME IS OVER");
            Console.WriteLine();
            printFormat.printCenter("1.Play again (Rematch)");
            printFormat.printCenter("2.BACK TO GAME MENU");
            printFormat.print("Enter choice: ");
            int menuchoice = Convert.ToInt32(Console.ReadLine());

            if (menuchoice == 1)
            {
                player1Score = 0;
                computerScore = 0;
                round = 1;
                mainLoop(player, difficulty, playerSymbol);
            }
            else if (choice == 2)
            {
                game.playGame();
            }


        }

    }
}
