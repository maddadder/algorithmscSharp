// AIPlayer.cs

public class AIPlayer
{
    public (int Row, int Col) GetBestMove(TicTacToeGame game)
    {
        // Minimax algorithm with alpha-beta pruning
        int bestScore = int.MinValue;
        (int Row, int Col) bestMove = (-1, -1);

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var localCol = col;
                var localRow = row;
                if (game.Board[localRow, localCol] == Player.None)
                {
                    game.MakeMove(localRow, localCol);
                    int score = Minimax(game, 0, false, int.MinValue, int.MaxValue);
                    game.Board[localRow, localCol] = Player.None; // Undo the move

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = (localRow, localCol);
                    }
                }
            }
        }

        return bestMove;
    }

    private int Minimax(TicTacToeGame game, int depth, bool isMaximizing, int alpha, int beta)
    {
        // Terminal node evaluation
        if (game.HasWin(Player.X)) return -10 + depth; // X wins
        if (game.HasWin(Player.O)) return 10 - depth;  // O wins
        if (game.IsDraw()) return 0;

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (game.Board[row, col] == Player.None)
                    {
                        game.MakeMove(row, col);
                        int score = Minimax(game, depth + 1, false, alpha, beta);
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
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (game.Board[row, col] == Player.None)
                    {
                        game.MakeMove(row, col);
                        int score = Minimax(game, depth + 1, true, alpha, beta);
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
}
