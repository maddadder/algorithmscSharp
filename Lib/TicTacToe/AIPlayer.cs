using System;
using System.Collections.Generic;

namespace Lib.TicTacToe;
public class AIPlayer
{
    Random random = new Random();

    public (int Row, int Col) GetRandomMove(TicTacToeGame game)
    {
        List<(int Row, int Col)> availableMoves = new List<(int, int)>();

        for (int row = 0; row < game.Board.GetLength(0); row++)
        {
            for (int col = 0; col < game.Board.GetLength(1); col++)
            {
                if (game.Board[row, col] == Player.None)
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

    public (int Row, int Col) GetBestMove(TicTacToeGame game, int maxDepth)
    {
        int bestScore = int.MinValue;
        (int Row, int Col) bestMove = (-1, -1);

        for (int depth = 1; depth <= maxDepth; depth++)
        {
            for (int row = 0; row < game.Board.GetLength(0); row++)
            {
                for (int col = 0; col < game.Board.GetLength(1); col++)
                {
                    if (game.Board[row, col] == Player.None)
                    {
                        game.MakeMove(row, col);
                        int score = Minimax(game, 0, false, int.MinValue, int.MaxValue, depth);
                        game.Board[row, col] = Player.None; // Undo the move

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (row, col);
                        }
                    }
                }
            }
        }

        if (bestMove == (-1, -1))
        {
            bestMove = GetRandomMove(game);
        }
        return bestMove;
    }

    private int Minimax(TicTacToeGame game, int depth, bool isMaximizing, int alpha, int beta, int maxDepth)
    {
        // Terminal node evaluation
        if (depth == maxDepth || game.HasWin(Player.X) || game.HasWin(Player.O) || game.IsDraw())
        {
            return EvaluateBoard(game);
        }

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int row = 0; row < game.Board.GetLength(0); row++)
            {
                for (int col = 0; col < game.Board.GetLength(1); col++)
                {
                    if (game.Board[row, col] == Player.None)
                    {
                        game.MakeMove(row, col);
                        int score = Minimax(game, depth + 1, true, alpha, beta, maxDepth);
                        game.Board[row, col] = Player.None; // Undo the move
                        bestScore = Math.Max(bestScore, score);
                        alpha = Math.Max(alpha, bestScore);
                        if (beta <= alpha)
                            break; // Beta pruning
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int row = 0; row < game.Board.GetLength(0); row++)
            {
                for (int col = 0; col < game.Board.GetLength(1); col++)
                {
                    if (game.Board[row, col] == Player.None)
                    {
                        game.MakeMove(row, col);
                        int score = Minimax(game, depth + 1, true, alpha, beta, maxDepth);
                        game.Board[row, col] = Player.None; // Undo the move
                        bestScore = Math.Min(bestScore, score);
                        beta = Math.Min(beta, bestScore);
                        if (beta <= alpha)
                            break; // Alpha pruning
                    }
                }
            }
            return bestScore;
        }
    }

    private int EvaluateBoard(TicTacToeGame game)
    {
        if (game.HasWin(Player.O))
        {
            return 10; // AI wins
        }
        else if (game.HasWin(Player.X))
        {
            return -10; // Player wins
        }
        else
        {
            return 0; // Draw or neutral position
        }
    }


}
