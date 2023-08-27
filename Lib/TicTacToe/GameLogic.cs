
namespace Lib.TicTacToe;

public enum Player { None, X, O }

public class TicTacToeGame
{
    public readonly int boardSize;
    public Player[,] Board { get; private set; }
    public Player CurrentPlayer { get; private set; }

    // Constructor to initialize the board and start the game
    public TicTacToeGame(int boardSize)
    {
        this.boardSize = boardSize;
        Board = new Player[boardSize, boardSize];
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
        // Check rows
        for (int row = 0; row < boardSize; row++)
        {
            bool allMatch = true;
            for (int col = 0; col < boardSize; col++)
            {
                if (Board[row, col] != player)
                {
                    allMatch = false;
                    break;
                }
            }
            if (allMatch)
            {
                return true;
            }
        }

        // Check columns
        for (int col = 0; col < boardSize; col++)
        {
            bool allMatch = true;
            for (int row = 0; row < boardSize; row++)
            {
                if (Board[row, col] != player)
                {
                    allMatch = false;
                    break;
                }
            }
            if (allMatch)
            {
                return true;
            }
        }

        // Check diagonals
        bool diagonal1Match = true;
        bool diagonal2Match = true;
        for (int i = 0; i < boardSize; i++)
        {
            if (Board[i, i] != player)
            {
                diagonal1Match = false;
            }
            if (Board[i, boardSize - 1 - i] != player)
            {
                diagonal2Match = false;
            }
        }
        if (diagonal1Match || diagonal2Match)
        {
            return true;
        }

        return false;
    }

    public bool IsDraw()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (Board[row, col] == Player.None)
                {
                    return false; // There's an empty cell, game is not a draw
                }
            }
        }

        return !HasWin(Player.X) && !HasWin(Player.O);
    }

    public Player[] GetRow(int rowIndex)
    {
        Player[] row = new Player[boardSize];
        for (int col = 0; col < boardSize; col++)
        {
            row[col] = Board[rowIndex, col];
        }
        return row;
    }

    public Player[] GetColumn(int colIndex)
    {
        Player[] column = new Player[boardSize];
        for (int row = 0; row < boardSize; row++)
        {
            column[row] = Board[row, colIndex];
        }
        return column;
    }

    public Player[] GetDiagonal1()
    {
        Player[] diagonal = new Player[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            diagonal[i] = Board[i, i];
        }
        return diagonal;
    }

    public Player[] GetDiagonal2()
    {
        Player[] diagonal = new Player[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            diagonal[i] = Board[i, boardSize - 1 - i];
        }
        return diagonal;
    }
}
