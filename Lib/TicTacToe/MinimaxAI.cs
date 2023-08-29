using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.TicTacToe;
public class MinimaxAI : IGameAI
{
    Random random = new Random();
    int maxDepth;

    public MinimaxAI(int maxDepth)
    {
        this.maxDepth = maxDepth;
    }

    public (int Row, int Col) GetBestMove(TicTacToeGame game)
    {
        int bestScore = int.MinValue;
        (int Row, int Col) bestMove = (-1, -1);

        for (int row = 0; row < game.Board.GetLength(0); row++)
        {
            for (int col = 0; col < game.Board.GetLength(1); col++)
            {
                if (game.Board[row, col] == Player.N)
                {
                    game.Board[row, col] = Player.O;
                    int score = Minimax(game, 0, false);
                    game.Board[row, col] = Player.N;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = (row, col);
                    }
                }
            }
        }

        return bestMove;
    }


    private int Minimax(TicTacToeGame game, int depth, bool isMaximizing)
    {
        Player currentPlayer = isMaximizing ? Player.O : Player.X;

        if (game.HasWin(Player.O)) // AI wins
        {
            return 100 - depth; // Adjust the scoring as needed
        }
        if (game.HasWin(Player.X)) // Human wins
        {
            return depth - 100; // Adjust the scoring as needed
        }
        if (game.IsDraw()) // Draw
        {
            return 0;
        }

        if (depth >= maxDepth) // Depth limit reached, return evaluation score
        {
            return EvaluateBoard(game, Player.O); // Use AI as the currentPlayer
        }

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

        for (int row = 0; row < game.Board.GetLength(0); row++)
        {
            for (int col = 0; col < game.Board.GetLength(1); col++)
            {
                if (game.Board[row, col] == Player.N)
                {
                    game.Board[row, col] = currentPlayer;
                    int score = Minimax(game, depth + 1, !isMaximizing);
                    game.Board[row, col] = Player.N;

                    if (isMaximizing)
                    {
                        bestScore = Math.Max(bestScore, score);
                    }
                    else
                    {
                        bestScore = Math.Min(bestScore, score);
                    }
                }
            }
        }

        return bestScore;
    }

    private int EvaluateBoard(TicTacToeGame game, Player currentPlayer)
    {
        int score = 0;

        // Evaluate rows, columns, and diagonals
        for (int i = 0; i < game.boardSize; i++)
        {
            score += EvaluateLine(game.GetRow(i), currentPlayer);
            score += EvaluateLine(game.GetColumn(i), currentPlayer);
        }
        score += EvaluateLine(game.GetDiagonal(true), currentPlayer);
        score += EvaluateLine(game.GetDiagonal(false), currentPlayer);

        return score;
    }

    private int EvaluateLine(Player[] line, Player currentPlayer)
    {
        int aiCount = line.Count(cell => cell == Player.O);
        int opponentCount = line.Count(cell => cell == Player.X);
        int emptyCount = line.Count(cell => cell == Player.N);

        int lineScore = 0;

        if (aiCount == 2 && emptyCount == 1)
        {
            lineScore += 10; // Favor AI's winning positions
        }
        else if (opponentCount == 2 && emptyCount == 1)
        {
            lineScore -= 10; // Favor blocking opponent's winning positions
        }

        // Add more pattern scoring as needed

        return lineScore;
    }


}
