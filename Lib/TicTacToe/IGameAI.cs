namespace Lib.TicTacToe;

public interface IGameAI
{
    (int Row, int Col) GetBestMove(TicTacToeGame game);
    // Other common methods or properties needed for AI
}