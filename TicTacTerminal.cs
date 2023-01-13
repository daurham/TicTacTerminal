public class TicTacTerminal
{
  // constructor
  public TicTacTerminal()
  {
    SetUpBoard();

    System.Console.WriteLine("Board created");
  }

  // properties
  private string[,] Board { get; set; }
  private int Moves { get; set; }
  private bool HasWinner { get; set; }


  // methods
  public void Run()
  {
    Console.Clear();
    bool wantsToPlay = false;
    DisplayBoard();
    System.Console.WriteLine("_______________________");
    System.Console.WriteLine("* * ** TICTACTOE ** * *");
    System.Console.WriteLine($"Ready to play? (y)");
    string response = Console.ReadLine();
    response.ToLower();
    if (response == "" || response == "y")
    {
      wantsToPlay = true;
    }
    else
    {
      Console.Clear();
      DisplayBoard();
      System.Console.WriteLine("_______________________");
      System.Console.WriteLine("* * ** TICTACTOE ** * *");
      System.Console.WriteLine("  Maybe later then!");
      System.Console.WriteLine("Peace, nerd");
    }

    while (wantsToPlay == true)
    {
      wantsToPlay = GameLogic();
      SetUpBoard();
    }
  }

  private bool GameLogic()
  {

    string currentPlayer;
    bool priorError = false;
    do
    {

      // Game Logic:
      Console.Clear();

      DisplayBoard();
      System.Console.WriteLine();
      if (priorError == true)
      {
        System.Console.WriteLine("ERROR: Please input a valid value!");
        priorError = false;
      }
      else System.Console.WriteLine();

      if (Moves % 2 == 0)
      {
        System.Console.WriteLine("Player 1: Choose your field!");
        currentPlayer = "X";
      }
      else
      {
        System.Console.WriteLine("Player 2: Choose your field!");
        currentPlayer = "O";
      }

      bool isValidInput = false;
      int inputInt;
      bool isValidInt = false;

      string play = Console.ReadLine();
      isValidInt = int.TryParse(play, out inputInt);
      if (isValidInt == true && inputInt > 0 && inputInt < 10)
      {
        isValidInput = MakeMove(Board, play, currentPlayer);
        if (isValidInput) Moves += 1;
      }

      if (isValidInput == false) priorError = true;

    } while (MatchIsOver(Board) == false);

    if (Moves == 9 && HasWinner == false)
    {
      Console.Clear();
      DisplayBoard();
      System.Console.WriteLine();
      System.Console.WriteLine();
      System.Console.WriteLine("FULL BOARD - EVERYONE LOSES!");
    }

    System.Console.WriteLine("Play Again? (y)");
    string response = Console.ReadLine();
    response = response.ToLower();
    return (response == "" || response == "y");
  }

  private void SetUpBoard()
  {
    Board = new string[3, 3]
{
    {"1","2","3"},
    {"4","5","6"},
    {"7","8","9"}
};
    Moves = 0;
    HasWinner = false;
  }

  private void DisplayBoard()
  {
    for (int i = 0; i < 3; i++)
    {
      if (i == 0)
      {
        System.Console.WriteLine("       |       |       ");
      }
      else if (i == 1)
      {
        System.Console.WriteLine("_______|_______|_______");
        System.Console.WriteLine("       |       |       ");
      }
      else
      {
        System.Console.WriteLine("_______|_______|_______");
        System.Console.WriteLine("       |       |       ");

      }
      for (int y = 0; y < 3; y++)
      {
        if (y < 2)
        {
          System.Console.Write($"   {Board[i, y]}   |");
        }
        else
        {
          System.Console.Write($"   {Board[i, y]}   \n");
        }
        if (i == 2 && y == 2)
        {
          System.Console.WriteLine("       |       |       ");
        }
      }
    }
  }
  private bool MatchIsOver(string[,] board)
  {
    string diagonalPlay1 = "";
    string diagonalPlay2 = "";
    string horizonalPlay1 = "";
    string horizonalPlay2 = "";
    string horizonalPlay3 = "";
    string verticalPlay1 = "";
    string verticalPlay2 = "";
    string verticalPlay3 = "";
    for (int i = 0, y = 2; i < board.GetLength(0); i++, y--)
    {
      diagonalPlay1 += board[i, i];
      diagonalPlay2 += board[y, i];
      horizonalPlay1 += board[0, i];
      horizonalPlay2 += board[1, i];
      horizonalPlay3 += board[2, i];
      verticalPlay1 += board[i, 0];
      verticalPlay2 += board[i, 1];
      verticalPlay3 += board[i, 2];
    }
    return (CheckPlay(diagonalPlay1) == true
            || CheckPlay(diagonalPlay2) == true
            || CheckPlay(horizonalPlay1) == true
            || CheckPlay(horizonalPlay2) == true
            || CheckPlay(horizonalPlay3) == true
            || CheckPlay(verticalPlay1) == true
            || CheckPlay(verticalPlay2) == true
            || CheckPlay(verticalPlay3) == true
            || Moves == 9);
  }
  private bool CheckPlay(string play)
  {
    if (play == "XXX")
    {
      HasWinner = true;
      Console.Clear();
      DisplayBoard();
      System.Console.WriteLine();
      System.Console.WriteLine("Player 1 Wins!!");
      return true;
    }
    else if (play == "OOO")
    {
      HasWinner = true;
      Console.Clear();
      DisplayBoard();
      System.Console.WriteLine();
      System.Console.WriteLine("Player 2 Wins!!");
      return true;
    }
    return false;
  }
  private bool MakeMove(string[,] board, string play, string playerChar)
  {
    for (int i = 0; i < board.GetLength(0); i++)
    {
      for (int j = 0; j < board.GetLength(0); j++)
      {
        if (board[i, j] == play)
        {
          board[i, j] = playerChar;
          return true;
        }
      }
    }
    return false;
  }
}