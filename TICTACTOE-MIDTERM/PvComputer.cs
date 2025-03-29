using Spectre.Console;
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
        public void mainLoop(string play1, int difficulty, char playerSymbol, string diff)
        {
            
            while (isRunning)
            {
                Console.Clear();
                printFormat.printCenterGreen($"DIFFICULTY: {diff}");
                displayBoard(play1);
                if (player1Score >= goalScore || computerScore >= goalScore)
                {

                    playerWinner = (player1Score > computerScore) ? play1 : comp;
                    break;

                }

                int move;
                bool isValidMove;
                
                if (currentPlayer == playerSymbol)
                {

                    do
                    {
                        printFormat.printCenter($"{currentPlayer}'s turn");
                        printFormat.print("Enter coordinate to place move (0-8): ");
                        check:
                        string placeMove = Console.ReadLine();
                        isValidMove = int.TryParse(placeMove, out move) && (move >= 0 && move <= 8);
                        if (!isValidMove)
                        {
                            printFormat.printCenterRed("Invalid input!");
                            printFormat.printCenterRed("Press any key to try again...");
                            Console.ReadKey();
                            Console.Clear();
                            //RE RENDER SA BOARD IF SAYUP ANG INPUT MOVE
                            printFormat.printCenterGreen($"DIFFICULTY: {diff}");
                            displayBoard(play1);
                            printFormat.printCenter($"{currentPlayer}'s turn");
                            printFormat.print("Enter coordinate to place move (0-8): ");
                            goto check;
                        }   

                    } while (!isValidMove);
                    

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
                    printFormat.printCenter("Cell already occupied.");
                    printFormat.print("Press any key to continue");
                    Console.ReadKey();
                }
            }

        }

        public void playComputer()
        {

            char playerSymbol = '\0';
            bool isDiffValid, isValid;
            bool isNotEmpty = true;
            int choice;
            int menChoice;
            string player = "";
            do
            {
                Console.Clear();
                display.paddingTop();
                printFormat.printCenterGreen("Enter difficulty level: ");
                printFormat.printCenterGreen("1.EZ");
                printFormat.printCenterGreen("2.MID FRFR");
                printFormat.printCenterGreen("3.HARD FUCK");
                printFormat.print("Enter choice: ");
                string diffchoice = Console.ReadLine();
                isDiffValid = int.TryParse(diffchoice, out choice) && (choice >= 1 && choice <= 3);

                if (!isDiffValid)
                {
                    printFormat.printCenterRed("Invalid input!");
                    printFormat.printCenterRed("Press any key to try again...");
                    Console.ReadKey();
                }
                
            } while (!isDiffValid);

            int difficulty = 0;
            string diff= "";
            switch (choice)
            {
                case 1:
                    difficulty += 1;
                    diff += "EASY";
                    break;
                case 2:
                    difficulty += 2;
                    diff += "MODERATE";
                    break;
                case 3:
                    difficulty += 3;
                    diff += "HARD";
                    break;
            }

            int symbol = 0;
            bool validSymbol;
            do
            {
                Console.Clear();
                printFormat.printCenterGreen($"DIFFICULTY: {diff}");
                printFormat.print("Enter player name: ");
                player += Console.ReadLine();

                isNotEmpty = !string.IsNullOrEmpty(player);
                if (!isNotEmpty)
                {
                    printFormat.printCenterRed("Field must not be empty");
                    printFormat.printCenterRed("Press any key to try again...");
                    Console.ReadKey();
                    Console.Clear();

                }
                else
                {
                    Console.WriteLine();
                    printFormat.printCenter("Choose SYMBOL");
                    printFormat.printCenter("1. X");
                    printFormat.printCenter("2. O");
                    printFormat.print("Enter choice: ");
                    check:
                    string symbolChoice = Console.ReadLine();
                    validSymbol = int.TryParse(symbolChoice, out symbol) && (symbol >= 1 && symbol <= 2);
                    if (!validSymbol)
                    {
                        printFormat.printCenterRed("Invalid input!");
                        printFormat.printCenterRed("Press any key to try again...");
                        Console.ReadKey();
                        Console.Clear();

                        printFormat.printCenterGreen($"DIFFICULTY: {diff}");
                        printFormat.print($"Enter player name: {player}");
                        Console.WriteLine();
                        Console.WriteLine();
                        printFormat.printCenter("Choose SYMBOL");
                        printFormat.printCenter("1. X");
                        printFormat.printCenter("2. O");
                        printFormat.print("Enter choice: ");
                        goto check;
                    }
                }

            } while (!isNotEmpty);

            playerSymbol = (symbol == 1) ? 'X' : 'O';
            currentPlayer = playerSymbol;
            Console.Clear();
            mainLoop(player, difficulty, playerSymbol, diff);
            
            do
            {
                Console.Clear();
                displayBoard(player);
                printFormat.printCenter($"{playerWinner} WINS!!!");
                Console.WriteLine();
                printFormat.printCenter("THE GAME IS OVER");
                Console.WriteLine();
                printFormat.printCenter("1.Play again (Rematch)");
                printFormat.printCenter("2.BACK TO GAME MENU");
                printFormat.print("Enter choice: ");
                string menuchoice = Console.ReadLine();
                isValid = int.TryParse(menuchoice, out menChoice) && (menChoice >= 1 && menChoice <= 2);
                if (!isValid)
                {
                    printFormat.printCenterRed("Invalid input!");
                    printFormat.printCenterRed("Press any key to try again...");
                    Console.ReadKey();
                }
            } while (!isValid);

            if (menChoice == 1)
            {
                player1Score = 0;
                computerScore = 0;
                round = 1;
                mainLoop(player, difficulty, playerSymbol, diff);
            }
            else if (menChoice == 2)
            {
                game.playGame();
            }


        }

    }
}
