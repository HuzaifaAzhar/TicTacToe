using System;
using System.Collections.Generic;
using System.IO;

class ComputerWildTicTacToe
{
  public static char[,] board = new char[3, 3];
  public static char currentPlayer = '2';
public void SaveGame(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(currentPlayer);

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    writer.Write(board[row, col]);
                }
                writer.WriteLine();
            }
        }
    }

    // Load a game state from a file
    public void LoadGame(string filename)
    {
         if (File.Exists(filename))
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            currentPlayer = reader.ReadLine()[0];

            for (int row = 0; row < 3; row++)
            {
                string line = reader.ReadLine();
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = line[col];
                }
            }
        }
        Console.WriteLine("Game loaded.");
    }
    }
  public void Play ()
  {
    InitBoard ();
    Console.WriteLine ("Welcome to Wild Tic-Tac-Toe!");
    LoadGame("savedgame1.txt");
    while (true)
      {
        SaveGame("savedgame1.txt");
        DrawBoard ();
        if (CheckWin ())
          {
            if (currentPlayer == '2')
              {
                Console.WriteLine ("Computer wins!");
              }
            else
              {
                Console.WriteLine ($"Player {currentPlayer} wins!");
              }
            break;
          }
        currentPlayer = (currentPlayer == '2') ? '1' : '2';
        if (IsBoardFull ())
          {
            Console.WriteLine ("The game is a draw.");
            break;
          }
        GetNextMove ();
      }

    Console.WriteLine ("Thanks for playing!");
    Console.ReadLine ();
  }

  private void
  InitBoard ()
  {
    for (int row = 0; row < 3; row++)
      {
        for (int col = 0; col < 3; col++)
          {
            board[row, col] = '-';
          }
      }
  }

  private void
  DrawBoard ()
  {
    Console.WriteLine ();
    Console.WriteLine ("   1 2 3");
    for (int row = 0; row < 3; row++)
      {
        Console.Write ($"{row + 1}  ");
        for (int col = 0; col < 3; col++)
          {
            Console.Write (board[row, col] + " ");
          }
        Console.WriteLine ();
      }
    Console.WriteLine ();
  }

  private bool
  CheckWin ()
  {
    // Check rows
    for (int row = 0; row < 3; row++)
      {
        if ((board[row, 0] == 'X' && board[row, 1] == 'X'
             && board[row, 2] == 'X')
            || (board[row, 0] == 'O' && board[row, 1] == 'O'
                && board[row, 2] == 'O'))
          {
            return true;
          }
      }
    // Check columns
    for (int col = 0; col < 3; col++)
      {
        if ((board[0, col] == 'X' && board[1, col] == 'X'
             && board[2, col] == 'X')
            || (board[0, col] == 'O' && board[1, col] == 'O'
                && board[2, col] == 'O'))
          {
            return true;
          }
      }
    // Check diagonals
    if ((board[0, 0] == 'X' && board[1, 1] == 'X' && board[2, 2] == 'X')
        || (board[0, 0] == 'O' && board[1, 1] == 'O' && board[2, 2] == 'O'))
      {
        return true;
      }
    if ((board[0, 2] == 'X' && board[1, 1] == 'X' && board[2, 0] == 'X')
        || (board[0, 2] == 'O' && board[1, 1] == 'O' && board[2, 0] == 'O'))
      {
        return true;
      }
    return false;
  }

  private static bool
  IsBoardFull ()
  {
    for (int row = 0; row < 3; row++)
      {
        for (int col = 0; col < 3; col++)
          {
            if (board[row, col] == '-')
              {
                return false;
              }
          }
      }
    return true;
  }

  private static void
  GetNextMove ()
  {
    int row, col;
    char piece;
    if (currentPlayer == '1')
      {
        // Player 1's turn
        do
          {
            Console.Write ($"Player {currentPlayer}, enter row (1-3): ");
            row = GetIntInput ();
            Console.Write ($"Player {currentPlayer}, enter column (1-3): ");
            col = GetIntInput ();
            Console.Write ($"Player {currentPlayer}, enter piece (X or O): ");
            piece = Console.ReadLine ().ToUpper ()[0];
            if (!IsValidMove (row - 1, col - 1, piece))
              {
                Console.WriteLine (
                    "That space is already occupied. Please try again.");
              };
          }
        while (!IsValidMove (row - 1, col - 1, piece));
        board[row - 1, col - 1] = piece;
      }
    else
      {
        // Computer's turn
        Random rand = new Random ();
        do
          {
            row = rand.Next (0, 3);
            col = rand.Next (0, 3);
            int ch = rand.Next (0, 2);
            if (ch == 1)
              {
                piece = 'X';
              }
            else
              {
                piece = 'O';
              }
          }
        while (!IsValidMove (row, col, piece));
        Console.WriteLine (
            $"Computer chose row {row+1}, column {col+1}, piece {piece}");
        board[row, col] = piece;
      }
  }

  private static int
  GetIntInput ()
  {
    int value;
    while (!int.TryParse (Console.ReadLine (), out value) || value < 1
           || value > 3)
      {
        Console.WriteLine (
            "Invalid input. Please enter a number between 1 and 3.");
      }
    return value;
  }

  private static bool
  IsValidMove (int row, int col, char piece)
  {
    if (board[row, col] != '-')
      {
        return false;
      }
    if (piece != 'X' && piece != 'O')
      {
        Console.WriteLine ("Invalid piece. Please enter X or O.");
        return false;
      }
    return true;
  }
}

class HumanWildTicTacToe
{
  private static char[,] board = new char[3, 3];
  private static char currentPlayer = '2';
  private static Stack<char[,]> boardStates = new Stack<char[,]>();

public void SaveGame(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(currentPlayer);

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    writer.Write(board[row, col]);
                }
                writer.WriteLine();
            }
        }
    }

    // Load a game state from a file
    public void LoadGame(string filename)
    {
         if (File.Exists(filename))
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            currentPlayer = reader.ReadLine()[0];

            for (int row = 0; row < 3; row++)
            {
                string line = reader.ReadLine();
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = line[col];
                }
            }
        }
        Console.WriteLine("Game loaded.");
    }
    }

  public void
  Play ()
  {
    InitBoard();
    boardStates.Push((char[,])board.Clone());
    Console.WriteLine ("Welcome to Wild Tic-Tac-Toe!");
    LoadGame("savedgame2.txt");
    while (true)
      {
        SaveGame("savedgame2.txt");
        DrawBoard ();
        if (CheckWin ())
          {
            Console.WriteLine ($"Player {currentPlayer} wins!");
            break;
          }
        currentPlayer = (currentPlayer == '2') ? '1' : '2';
        if (IsBoardFull ())
          {
            Console.WriteLine ("The game is a draw.");
            break;
          }
         
        Console.WriteLine($"Player {currentPlayer}'s turn.");

        Console.Write("Enter any key for inserting  or 'undo' to undo: ");
        string input = Console.ReadLine();

        if (input == "undo")
        {
            Undo();
        }
        else
        {
            GetNextMove ();
        }
      }

    Console.WriteLine ("Thanks for playing!");
    Console.ReadLine ();
  }

  private void
  InitBoard ()
  {
    for (int row = 0; row < 3; row++)
      {
        for (int col = 0; col < 3; col++)
          {
            board[row, col] = '-';
          }
      }
  }

  private void
  DrawBoard ()
  {
    Console.WriteLine ();
    Console.WriteLine ("   1 2 3");
    for (int row = 0; row < 3; row++)
      {
        Console.Write ($"{row + 1}  ");
        for (int col = 0; col < 3; col++)
          {
            Console.Write (board[row, col] + " ");
          }
        Console.WriteLine ();
      }
    Console.WriteLine ();
  }

  private bool
  CheckWin ()
  {
    // Check rows
    for (int row = 0; row < 3; row++)
      {
        if ((board[row, 0] == 'X' && board[row, 1] == 'X'
             && board[row, 2] == 'X')
            || (board[row, 0] == 'O' && board[row, 1] == 'O'
                && board[row, 2] == 'O'))
          {
            return true;
          }
      }
    // Check columns
    for (int col = 0; col < 3; col++)
      {
        if ((board[0, col] == 'X' && board[1, col] == 'X'
             && board[2, col] == 'X')
            || (board[0, col] == 'O' && board[1, col] == 'O'
                && board[2, col] == 'O'))
          {
            return true;
          }
      }
    // Check diagonals
    if ((board[0, 0] == 'X' && board[1, 1] == 'X' && board[2, 2] == 'X')
        || (board[0, 0] == 'O' && board[1, 1] == 'O' && board[2, 2] == 'O'))
      {
        return true;
      }
    if ((board[0, 2] == 'X' && board[1, 1] == 'X' && board[2, 0] == 'X')
        || (board[0, 2] == 'O' && board[1, 1] == 'O' && board[2, 0] == 'O'))
      {
        return true;
      }
    return false;
  }

  private static bool
  IsBoardFull ()
  {
    for (int row = 0; row < 3; row++)
      {
        for (int col = 0; col < 3; col++)
          {
            if (board[row, col] == '-')
              {
                return false;
              }
          }
      }
    return true;
  }

  private static void
  GetNextMove ()
  {
    int row, col;
    char piece;
    do
      {
        Console.Write ($"Player {currentPlayer}, enter row (1-3): ");
        row = GetIntInput ();
        Console.Write ($"Player {currentPlayer}, enter column (1-3): ");
        col = GetIntInput ();
        Console.Write ($"Player {currentPlayer}, enter piece (X or O): ");
        piece = Console.ReadLine ().ToUpper ()[0];
      }
    while (!IsValidMove (row - 1, col - 1, piece));
    board[row - 1, col - 1] = piece;
    boardStates.Push((char[,])board.Clone());
  }

  private static int
  GetIntInput ()
  {
    int value;
    while (!int.TryParse (Console.ReadLine (), out value) || value < 1
           || value > 3)
      {
        Console.WriteLine (
            "Invalid input. Please enter a number between 1 and 3.");
      }
    return value;
  }
 
  private void Undo()
{
    if (boardStates.Count > 1)
    {
        boardStates.Pop(); // Remove current state
        board = (char[,])boardStates.Peek().Clone(); // Set board to previous state
       
    }
    else
    {
        Console.WriteLine("Cannot undo further.");
    }
}

 

  private static bool
  IsValidMove (int row, int col, char piece)
  {
    if (board[row, col] != '-')
      {
        Console.WriteLine ("That space is already occupied. Please try again.");
        return false;
      }
    if (piece != 'X' && piece != 'O')
      {
        Console.WriteLine ("Invalid piece. Please enter X or O.");
        return false;
      }
    return true;
  }
}

class ComputerNumeric
{
  private int[,] board;
  private int currentPlayer;
  private int turns;
  private bool[] used;
  private Stack<int[,]> undoStack = new Stack<int[,]> ();
  private Stack<int[,]> redoStack = new Stack<int[,]> ();

  public ComputerNumeric ()
  {
    board = new int[3, 3];
    currentPlayer = 1;
    turns = 0;
    used = new bool[10];
  }
  public void SaveGame(string filename)
{
    using (StreamWriter writer = new StreamWriter(filename))
    {
        writer.WriteLine(currentPlayer);

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                writer.Write(board[row, col]);
            }
            writer.WriteLine();
        }

        for (int i = 1; i <= 9; i++)
        {
            writer.WriteLine(used[i] ? "1" : "0");
        }
    }
}

public void LoadGame(string filename)
{
    if (File.Exists(filename))
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            currentPlayer = int.Parse(reader.ReadLine());

            for (int row = 0; row < 3; row++)
            {
                string line = reader.ReadLine();
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = int.Parse(line[col].ToString());
                }
            }

            for (int i = 1; i <= 9; i++)
            {
                used[i] = reader.ReadLine() == "1";
            }
        }
        Console.WriteLine("Game loaded.");
    }
}

  public void
  Run ()
  {
      LoadGame("savedgame3.txt");
    PrintBoard ();
    while (true)
      {
         SaveGame("savedgame3.txt");
        GetInput ();
        turns++;
        PrintBoard ();
        if (CheckWin (board))
          {
            if (currentPlayer == 1)
              {
                Console.WriteLine ("Player 1 wins!");
              }
            else
              {
                Console.WriteLine ("Computer wins!");
              }
            break;
          }
        else if (turns == 9)
          {
            Console.WriteLine ("It's a tie!");
            break;
          }

        // Switch to the other player
        currentPlayer = (currentPlayer == 1) ? 2 : 1;
      }
  }

  private void
  PrintBoard ()
  {
    Console.WriteLine ("  1 2 3");
    Console.WriteLine (" +-+-+-+");
    for (int i = 0; i < 3; i++)
      {
        Console.Write ((i + 1) + "|");
        for (int j = 0; j < 3; j++)
          {
            if (board[i, j] == 0)
              {
                Console.Write (" |");
              }
            else
              {
                Console.Write ("{0}|", board[i, j]);
              }
          }
        Console.WriteLine ();
        Console.WriteLine (" +-+-+-+");
      }
  }

  public void
  Undo (int num)
  {
    if (undoStack.Count > 0)
      {
        // Save current state on redo stack
        int[,] currentState = new int[3, 3];
        Array.Copy (board, currentState, board.Length);
        redoStack.Push (currentState);

        // Restore previous state from undo stack
        int[,] prevState = undoStack.Pop ();
        Array.Copy (prevState, board, board.Length);
        currentPlayer = (currentPlayer == 2) ? 1 : 2;
        turns--;
        PrintBoard ();
        used[num] = false;
        Console.WriteLine (
            "Press 1 to Redo or Press any other key to continue: ");
        string input = Console.ReadLine ();

        if (input == "1")
          {
            Redo (num);
          }
        else
          {
            Run ();
          }
      }
  }

  public void
  Redo (int num)
  {
    Console.WriteLine ("Redo Called ");
    if (redoStack.Count > 0)
      {
        // Save current state on undo stack
        int[,] currentState = new int[3, 3];
        Array.Copy (board, currentState, board.Length);
        undoStack.Push (currentState);

        // Restore next state from redo stack
        int[,] nextState = redoStack.Pop ();
        Array.Copy (nextState, board, board.Length);
        currentPlayer = (currentPlayer == 1) ? 2 : 1;
        turns++;
        used[num] = true;
        PrintBoard ();
      }
  }

  private void
  GetInput ()
  {
    int row = 0;
    int col = 0;
    int num = 0;
    int[,] currentState = new int[3, 3];
    Array.Copy (board, currentState, board.Length);
    undoStack.Push (currentState);
    if (currentPlayer == 1)
      {
        Console.Write ("Player 1, enter your position (row SPACE column): ");
        string[] input = Console.ReadLine ().Split ();
        row = int.Parse (input[0]) - 1;
        col = int.Parse (input[1]) - 1;

        if (row < 0 || row > 2 || col < 0 || col > 2)
          {
            Console.WriteLine ("Invalid position, try again.");
            GetInput ();
            return;
          }

        if (board[row, col] != 0)
          {
            Console.WriteLine ("Position already taken, try again.");
            GetInput ();
            return;
          }

        Console.Write ("Enter an odd number (1-9): ");
        num = int.Parse (Console.ReadLine ());
        if (num % 2 == 0)
          {
            Console.WriteLine ("Invalid number, try again.");
            GetInput ();
            return;
          }
        if (used[num])
          {
            Console.WriteLine ("Number already used, try again.");
            GetInput ();
            return;
          }
        else
          {
            used[num] = true;
          }
        Console.WriteLine (
            "Press 1 to Undo or Press any other key to continue: ");
        string input2 = Console.ReadLine ();

        if (input2 == "1")
          {
            Undo (num);
          }
      }
    else
      {
        // Generate random position and number for computer
        Random rand = new Random ();
        do
          {
            row = rand.Next (0, 3);
            col = rand.Next (0, 3);
          }
        while (board[row, col] != 0);

        num = rand.Next (1, 8);
        if (num % 2 == 1)
          {
            num++;
          }
        if (used[num])
          {
            GetInput ();
            return;
          }
        else
          {
            used[num] = true;
          }
        Console.WriteLine (
            "Computer chooses position ({0}, {1}) and number {2}", row, col,
            num);
      }
    // Player made a valid move, clear redo stack
    redoStack.Clear ();
    board[row, col] = num;
  }

  private bool
  CheckWin (int[,] board)
  {
    // Check rows
    for (int i = 0; i < 3; i++)
      {
        if (board[i, 0] + board[i, 1] + board[i, 2] >= 15)
          {
            if (board[i, 0] != 0 && board[i, 1] != 0 && board[i, 2] != 0)
              {
                return true;
              }
          }
      }

    // Check columns
    for (int j = 0; j < 3; j++)
      {
        if (board[0, j] + board[1, j] + board[2, j] >= 15)
          {
            if (board[0, j] != 0 && board[1, j] != 0 && board[2, j] != 0)
              {
                return true;
              }
          }
      }

    // Check diagonals
    if (board[0, 0] + board[1, 1] + board[2, 2] >= 15)
      {
        if (board[0, 0] != 0 && board[1, 1] != 0 && board[2, 2] != 0)
          {
            return true;
          }
      }
    if (board[0, 2] + board[1, 1] + board[2, 0] >= 15)
      {
        if (board[0, 2] != 0 && board[1, 1] != 0 && board[2, 0] != 0)
          {
            return true;
          }
      }

    return false;
  }
}

class HumanNumeric
{
  private int[,] board;
  private int currentPlayer;
  private int turns;
  private bool[] used;
  private static Stack<int[,]> boardStates = new Stack<int[,]>();

    public void SaveGame(string filename)
{
    using (StreamWriter writer = new StreamWriter(filename))
    {
        writer.WriteLine(currentPlayer);

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                writer.Write(board[row, col]);
            }
            writer.WriteLine();
        }

        for (int i = 1; i <= 9; i++)
        {
            writer.WriteLine(used[i] ? "1" : "0");
        }
    }
}

public void LoadGame(string filename)
{
    if (File.Exists(filename))
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            currentPlayer = int.Parse(reader.ReadLine());

            for (int row = 0; row < 3; row++)
            {
                string line = reader.ReadLine();
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = int.Parse(line[col].ToString());
                }
            }

            for (int i = 1; i <= 9; i++)
            {
                used[i] = reader.ReadLine() == "1";
            }
        }
        Console.WriteLine("Game loaded.");
    }
}

  public HumanNumeric ()
  {
    board = new int[3, 3];
    currentPlayer = 1;
    turns = 0;
    used = new bool[10];
  }

  public void
  Run ()
  {
      LoadGame("savedgame4.txt");
    while (true)
      {
        SaveGame("savedgame4.txt");
        PrintBoard ();
        boardStates.Push((int[,])board.Clone());
        Console.Write("Enter any key for inserting  or 'undo' to undo: ");
        string opt = Console.ReadLine();

        if (opt == "undo")
        {
            Undo();
            PrintBoard ();
        }
        int[] input = GetInput ();

       
        int row = input[0];
        int col = input[1];

        if (board[row, col] != 0)
          {
            Console.WriteLine ("Position already taken, try again.");
            continue;
          }

        int num = GetNumber ();
        if (num == -1)
          {
            continue;
          }

        board[row, col] = num;
        turns++;

        if (CheckWin ())
          {
            Console.WriteLine ("Player {0} wins!", currentPlayer);
            return;
          }

        if (turns == 9)
          {
            Console.WriteLine ("Tie game!");
            return;
          }

        currentPlayer = currentPlayer == 1 ? 2 : 1;
      }
  }


  private void Undo()
{
        boardStates.Pop(); // Remove current state
        board = (int[,])boardStates.Peek().Clone(); // Set board to previous state
   
}


  private void
  PrintBoard ()
  {
    Console.WriteLine ("  1 2 3");
    Console.WriteLine (" +-+-+-+");
    for (int i = 0; i < 3; i++)
      {
        Console.Write ((i + 1) + "|");
        for (int j = 0; j < 3; j++)
          {
            if (board[i, j] == 0)
              {
                Console.Write (" |");
              }
            else
              {
                Console.Write ("{0}|", board[i, j]);
              }
          }
        Console.WriteLine ();
        Console.WriteLine (" +-+-+-+");
      }
  }

  private int[] GetInput ()
  {
    while (true)
      {
        Console.Write ("Player {0}, enter your position (row SPACE column): ",
                       currentPlayer);
        string[] input = Console.ReadLine ().Split ();
        int row = int.Parse (input[0]) - 1;
        int col = int.Parse (input[1]) - 1;

        if (row < 0 || row > 2 || col < 0 || col > 2)
          {
            Console.WriteLine ("Invalid position, try again.");
            continue;
          }

        return new int[] { row, col };
      }
  }

  private int
  GetNumber ()
  {
    while (true)
      {
        int num = 0;
        if (currentPlayer == 1)
          {
            Console.Write ("Enter an odd number (1-9): ");
            num = int.Parse (Console.ReadLine ());
            if (num % 2 == 0)
              {
                Console.WriteLine ("Invalid number, try again.");
                continue;
              }
          }
        else
          {
            Console.Write ("Enter an even number (1-9): ");
            num = int.Parse (Console.ReadLine ());
            if (num % 2 == 1)
              {
                Console.WriteLine ("Invalid number, try again.");
                continue;
              }
          }

        if (num < 1 || num > 9)
          {
            Console.WriteLine ("Invalid number, try again.");
            continue;
          }
        if (used[num])
          {
            Console.WriteLine ("Number already used, try again.");
            continue;
          }
        else
          {
            used[num] = true;
            return num;
          }
      }
  }

  private bool
  CheckWin ()
  {
    for (int i = 0; i < 3; i++)
      {
        // Check rows
        if (board[i, 0] + board[i, 1] + board[i, 2] >= 15)
          {
            if (board[i, 0] != 0 && board[i, 1] != 0 && board[i, 2] != 0)
              {
                return true;
              }
          }
        // Check columns
        if (board[0, i] + board[1, i] + board[2, i] >= 15)
          {
            if (board[0, i] != 0 && board[1, i] != 0 && board[2, i] != 0)
              {
                return true;
              }
          }
      }
    // Check diagonals
    if (board[0, 0] + board[1, 1] + board[2, 2] >= 15
        || board[0, 2] + board[1, 1] + board[2, 0] >= 15)
      {
        if (board[0, 0] != 0 && board[1, 1] != 0 && board[2, 2] != 0
            || board[0, 2] != 0 && board[1, 1] != 0 && board[2, 0] != 0)

          {
            return true;
          }
      }

    return false;
  }
}

class Program
{
  static void Main (string[] args)
  {
    Console.WriteLine (
        "Press 1 for:  WildTicTacToe   \nPress 2 for:  NumericTicTacToe ");
    string main_input = Console.ReadLine ();

    if (main_input == "1")
      {
        Console.WriteLine (
            "Press 1 for:  playing against another player   \nPress 2 for:  playing against computer ");
        string input = Console.ReadLine ();

        if (input == "1")
          {
            HumanWildTicTacToe game = new HumanWildTicTacToe ();
            game.Play ();
          }
        else if (input == "2")
          {
            ComputerWildTicTacToe game2 = new ComputerWildTicTacToe ();
            game2.Play();
          }
        else
          {
            Console.WriteLine ("Invalid input. Please press 1 or 2 to play.");
          }
      }

    else if (main_input == "2")
      {
        Console.WriteLine (
            "Press 1 for:  playing against another player   \nPress 2 for:  playing against computer ");

        string input = Console.ReadLine ();

        if (input == "1")
          {
            HumanNumeric game2 = new HumanNumeric ();
            game2.Run ();
          }
        else if (input == "2")
          {
            ComputerNumeric game = new ComputerNumeric ();
            game.Run ();
          }
        else
          {
            Console.WriteLine ("Invalid input. Please press 1 or 2 to play.");
          }
      }
  }
}