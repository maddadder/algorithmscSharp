using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.TicTacToe; // Replace with your namespace

namespace Test
{
    [TestClass]
    public class MiniMaxPlayerTests
    {
        [TestMethod]
        public void AIPlaysGameToEnd()
        {
            // Arrange
            var game = new TicTacToeGame(3, Player.X);
            var aiPlayer = new MinimaxAI(4);

            // Act
            while (!game.HasWin(Player.X) && !game.HasWin(Player.O) && !game.IsDraw())
            {
                var (aiRow, aiCol) = aiPlayer.GetBestMove(game);
                game.MakeMove(aiRow, aiCol);
            }

            // Assert
            Assert.IsTrue(game.HasWin(Player.X) || game.HasWin(Player.O) || game.IsDraw());
        }
        [TestMethod]
        public void MakeMove_PlayerX_Success()
        {
            // Arrange
            var game = new TicTacToeGame(4, Player.X);

            // Act
            game.MakeMove(0, 0);

            // Assert
            Assert.AreEqual(Player.X, game.Board[0, 0]);
            Assert.AreEqual(Player.O, game.CurrentPlayer);
        }

        [TestMethod]
        public void GetRandomMove_ReturnsValidMove()
        {
            // Arrange
            var game = new TicTacToeGame(4, Player.X);
            game.MakeMove(0, 0); // Make a move to ensure the cell (0, 0) is not empty
            var aiPlayer = new MinimaxAI(5);

            // Act
            var randomMove = game.GetRandomMove();

            // Assert
            Assert.IsTrue(randomMove.Row >= 0 && randomMove.Row <= 2);
            Assert.IsTrue(randomMove.Col >= 0 && randomMove.Col <= 2);
            Assert.AreEqual(Player.N, game.Board[randomMove.Row, randomMove.Col]);
        }
    }
}