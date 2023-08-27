using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.TicTacToe; // Replace with your namespace

namespace Test
{
    [TestClass]
    public class TicTacToeGameTests
    {
        [TestMethod]
        public void AIPlaysGameToEnd()
        {
            // Arrange
            var game = new TicTacToeGame(4);
            var aiPlayer = new AIPlayer();

            // Act
            while (!game.HasWin(Player.X) && !game.HasWin(Player.O) && !game.IsDraw())
            {
                var (aiRow, aiCol) = aiPlayer.GetBestMove(game, 7);
                game.MakeMoveAI(aiRow, aiCol);
            }

            // Assert
            Assert.IsTrue(game.HasWin(Player.X) || game.HasWin(Player.O) || game.IsDraw());
        }
        [TestMethod]
        public void MakeMove_PlayerX_Success()
        {
            // Arrange
            var game = new TicTacToeGame(4);

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
            var game = new TicTacToeGame(4);
            game.MakeMove(0, 0); // Make a move to ensure the cell (0, 0) is not empty
            var aiPlayer = new AIPlayer();

            // Act
            var randomMove = aiPlayer.GetRandomMove(game);

            // Assert
            Assert.IsTrue(randomMove.Row >= 0 && randomMove.Row <= 2);
            Assert.IsTrue(randomMove.Col >= 0 && randomMove.Col <= 2);
            Assert.AreEqual(Player.None, game.Board[randomMove.Row, randomMove.Col]);
        }
    }
}