
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.TicTacToe;

public enum Player { N, X, O }

public class TicTacToeGame : ICloneable
{
    public Random random = new Random();
    public readonly int boardSize;
    public Player[,] Board { get; private set; }
    public Player CurrentPlayer { get; set; }

    // Constructor to initialize the board and start the game
    public TicTacToeGame(int boardSize, Player player1)
    {
        this.boardSize = boardSize;
        Board = new Player[boardSize, boardSize];
        if(player1 == Player.N){
            CurrentPlayer = GetRandomPlayer();
        }
        else
        {
            CurrentPlayer = player1;
        }
    }
    public Player GetRandomPlayer()
    {
        int randomIndex = random.Next(2);
        return randomIndex == 0 ? Player.X : Player.O;
    }
    public object Clone()
    {
        TicTacToeGame clone = new TicTacToeGame(boardSize, this.CurrentPlayer);
        
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                clone.Board[row, col] = Board[row, col];
            }
        }
        
        return clone;
    }
    public (int Row, int Col) GetRandomMove(TicTacToeGame game)
    {
        List<(int Row, int Col)> availableMoves = new List<(int, int)>();

        for (int row = 0; row < Board.GetLength(0); row++)
        {
            for (int col = 0; col < Board.GetLength(1); col++)
            {
                if (Board[row, col] == Player.N)
                {
                    availableMoves.Add((row, col));
                }
            }
        }

        if (availableMoves.Count > 0)
        {
            int randomIndex = random.Next(availableMoves.Count);
            return availableMoves[randomIndex];
        }
        else
        {
            throw new Exception("No available moves.");
        }
    }
    public void MakeMove(int row, int col)
    {
        if (Board[row, col] == Player.N)
        {
            Board[row, col] = CurrentPlayer;
            CurrentPlayer = GetOpponent(CurrentPlayer); // Switch players
        }
        //PrintBoard();
    }

    public bool HasWin(Player player)
    {
        // Check rows and columns for a win
        for (int i = 0; i < boardSize; i++)
        {
            if (CheckLineForWin(GetRow(i), player) || CheckLineForWin(GetColumn(i), player))
            {
                return true;
            }
        }

        // Check main diagonal and anti-diagonal for a win
        Player[] mainDiagonal = GetDiagonal(true);
        Player[] antiDiagonal = GetDiagonal(false);

        return CheckLineForWin(mainDiagonal, player) || CheckLineForWin(antiDiagonal, player);
    }

    private bool CheckLineForWin(Player[] line, Player player)
    {
        return line.All(cell => cell == player);
    }

    public bool IsDraw()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (Board[row, col] == Player.N)
                {
                    return false; // There's an empty cell, game is not a draw
                }
            }
        }

        return !HasWin(Player.X) && !HasWin(Player.O);
    }


    

    public List<(int Row, int Col)> GetEmptyCells()
    {
        List<(int Row, int Col)> emptyCells = new List<(int Row, int Col)>();

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (Board[row, col] == Player.N)
                {
                    emptyCells.Add((row, col));
                }
            }
        }

        return emptyCells;
    }
    public bool IsGameEnd()
    {
        return this.HasWin(Player.O) || this.HasWin(Player.X) || this.IsDraw();
    }

    internal Player GetOpponent(Player currentPlayer)
    {
        if(currentPlayer == Player.X)
            return Player.O;
        else if (currentPlayer == Player.O)
            return Player.X;
        else
        {
            throw new Exception("Cannot swap player");
        }
    }
    public Player GetWinner()
    {
        // Check rows, columns, and diagonals for a winning sequence
        for (int i = 0; i < boardSize; i++)
        {
            // Check row winner
            if (AreAllEqual(Board[i, 0], GetRow(i)))
            {
                return Board[i, 0];
            }

            // Check column winner
            if (AreAllEqual(Board[0, i], GetColumn(i)))
            {
                return Board[0, i];
            }
        }

        // Check diagonals for a winning sequence
        if (AreAllEqual(Board[0, 0], GetDiagonal(true)))
        {
            return Board[0, 0];
        }
        // Check diagonals for a winning sequence
        if(AreAllEqual(Board[0, boardSize - 1], GetDiagonal(false)))
        {
            return Board[0, boardSize - 1];
        }

        return Player.N; // No winner (draw or ongoing game)
    }

    public bool AreAllEqual(Player player, Player[] cells)
    {
        return cells.All(cell => cell == player);
    }

    public Player[] GetRow(int row)
    {
        return Enumerable.Range(0, boardSize).Select(col => Board[row, col]).ToArray();
    }

    public Player[] GetColumn(int col)
    {
        return Enumerable.Range(0, boardSize).Select(row => Board[row, col]).ToArray();
    }

    public Player[] GetDiagonal(bool isMainDiagonal)
    {
        if(isMainDiagonal)
        {
            Player[] diagonal = new Player[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                diagonal[i] = Board[i, i];
            }
            return diagonal;
        }
        else
        {
            Player[] diagonal = new Player[boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                diagonal[i] = Board[i, boardSize - 1 - i];
            }
            return diagonal;
        }
        
    }

    
    public void PrintBoard()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                //Debug.Write($"{row},{col},{Board[row, col]}");
                Debug.Write($"{Board[row, col]}");

                if (col < boardSize - 1)
                {
                    Debug.Write(" | ");
                }
            }

            Debug.WriteLine("");

            if (row < boardSize - 1)
            {
                Debug.WriteLine(new string('-', boardSize * 4 - 1));
            }
        }

        Debug.WriteLine("");
    }

    public void SetBoard(Player[,] newBoard)
    {
        if (newBoard.GetLength(0) != boardSize || newBoard.GetLength(1) != boardSize)
        {
            throw new ArgumentException("Invalid board size");
        }

        Board = newBoard;
    }
}
