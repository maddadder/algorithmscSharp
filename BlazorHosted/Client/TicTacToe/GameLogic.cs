public enum Player { None, X, O }

public class TicTacToeGame
{
    public Player[,] Board { get; private set; }
    public Player CurrentPlayer { get; private set; }

    // Constructor to initialize the board and start the game
    public TicTacToeGame()
    {
        Board = new Player[3, 3];
        CurrentPlayer = Player.X;
    }

    public void MakeMove(int row, int col)
    {
        if (Board[row, col] == Player.None)
        {
            Board[row, col] = CurrentPlayer;
            CurrentPlayer = CurrentPlayer == Player.X ? Player.O : Player.X;
        }
    }
    public void MakeMoveAI(int row, int col)
    {
        if (Board[row, col] == Player.None)
        {
            Board[row, col] = Player.O;
            CurrentPlayer = Player.X;
        }
    }

    public bool HasWin(Player player)
    {
        // Check horizontal lines
        for (int row = 0; row < 3; row++)
        {
            if (Board[row, 0] == player && Board[row, 1] == player && Board[row, 2] == player)
            {
                return true;
            }
        }

        // Check vertical lines
        for (int col = 0; col < 3; col++)
        {
            if (Board[0, col] == player && Board[1, col] == player && Board[2, col] == player)
            {
                return true;
            }
        }

        // Check diagonals
        if (Board[0, 0] == player && Board[1, 1] == player && Board[2, 2] == player)
        {
            return true;
        }
        
        if (Board[0, 2] == player && Board[1, 1] == player && Board[2, 0] == player)
        {
            return true;
        }

        return false;
    }

    public bool IsDraw()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (Board[row, col] == Player.None)
                {
                    return false; // There's an empty cell, game is not a draw
                }
            }
        }

        return !HasWin(Player.X) && !HasWin(Player.O);
    }

}
