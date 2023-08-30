using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.TicTacToe; 

namespace Test
{
    [TestClass]
    public class TicTacToeGameTest
    {
        
        [TestMethod]
        public void TestGetMainDiagonal1()
        {
            // Create a TicTacToeGame instance with a specific board size
            const int boardSize = 3;
            TicTacToeGame game = new TicTacToeGame(boardSize, Player.X);

            // Set up a test game board with some values
            Player[,] testBoard = new Player[boardSize, boardSize]
            {
                { Player.X, Player.O, Player.N },
                { Player.N, Player.X, Player.O },
                { Player.O, Player.N, Player.X }
            };

            game.SetBoard(testBoard);
            Assert.IsTrue(game.HasWin(Player.X));
            // Call the GetDiagonal method with isMainDiagonal = true
            Player[] mainDiagonal = game.GetDiagonal(true);

            // Verify the expected main diagonal values
            Assert.AreEqual(Player.X, mainDiagonal[0]);
            Assert.AreEqual(Player.X, mainDiagonal[1]);
            Assert.AreEqual(Player.X, mainDiagonal[2]);
        }
        [TestMethod]
        public void TestGetMainDiagonal2()
        {
            // Create a TicTacToeGame instance with a specific board size
            const int boardSize = 3;
            TicTacToeGame game = new TicTacToeGame(boardSize, Player.X);

            // Set up a test game board with some values
            Player[,] testBoard = new Player[boardSize, boardSize]
            {
                { Player.N, Player.O, Player.X },
                { Player.N, Player.X, Player.O },
                { Player.X, Player.N, Player.N }
            };

            game.SetBoard(testBoard);
            Assert.IsTrue(game.HasWin(Player.X));
            // Call the GetDiagonal method with isMainDiagonal = true
            Player[] mainDiagonal = game.GetDiagonal(false);

            // Verify the expected main diagonal values
            Assert.AreEqual(Player.X, mainDiagonal[0]);
            Assert.AreEqual(Player.X, mainDiagonal[1]);
            Assert.AreEqual(Player.X, mainDiagonal[2]);
        }


        [TestMethod]
        public void TestGetWinner_NoWinner()
        {
            const int boardSize = 3;
            // Set up a game state with no winner
            TicTacToeGame game = new TicTacToeGame(3, Player.X);
            Player[,] testBoard = new Player[boardSize, boardSize]
            {
                { Player.X, Player.X, Player.O },
                { Player.X, Player.X, Player.O },
                { Player.O, Player.N, Player.N }
            };

            game.SetBoard(testBoard);

            Player actualWinner = game.GetWinner();

            Assert.AreEqual(Player.N, actualWinner);
        }

        [TestMethod]
        public void TestGetWinner_WinForPlayerX()
        {
            const int boardSize = 3;
            // Set up a game state with no winner
            TicTacToeGame game = new TicTacToeGame(3, Player.X);
            Player[,] testBoard = new Player[boardSize, boardSize]
            {
                { Player.X, Player.X, Player.O },
                { Player.X, Player.X, Player.O },
                { Player.O, Player.N, Player.X }
            };

            game.SetBoard(testBoard);

            Player actualWinner = game.GetWinner();

            Assert.AreEqual(Player.X, actualWinner);
        }
        [TestMethod]
        public void TestGetWinner_WinForPlayerO()
        {
            const int boardSize = 3;
            // Set up a game state with no winner
            TicTacToeGame game = new TicTacToeGame(3, Player.X);
            Player[,] testBoard = new Player[boardSize, boardSize]
            {
                { Player.X, Player.X, Player.O },
                { Player.X, Player.O, Player.O },
                { Player.O, Player.N, Player.N }
            };

            game.SetBoard(testBoard);

            Player actualWinner = game.GetWinner();

            Assert.AreEqual(Player.O, actualWinner);
        }
        [TestMethod]
        public void MakeMove_PlayerX_Success()
        {
            // Arrange
            var game = new TicTacToeGame(3, Player.X);

            // Act
            game.MakeMove(0, 0);

            // Assert
            Assert.AreEqual(Player.X, game.Board[0, 0]);
            Assert.AreEqual(Player.O, game.CurrentPlayer);
        }

    }
}