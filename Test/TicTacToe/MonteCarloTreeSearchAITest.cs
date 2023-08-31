using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.TicTacToe; 

namespace Test
{
    [TestClass]
    public class MonteCarloTreeSearchAITest
    {
        
        [TestMethod]
        public void TestGameSimulation()
        {
            int winCount = 0;
            for(var i = 0;i<100;i++)
            {
                Debug.WriteLine("NEW_GAME");
                const int boardSize = 3;
                Player startingPlayer = Player.X;
                // Create a new Tic-Tac-Toe game instance
                TicTacToeGame game = new TicTacToeGame(boardSize, startingPlayer);

                // Create an instance of the MonteCarloTreeSearchAI
                MonteCarloTreeSearchAI OPlayer = new MonteCarloTreeSearchAI(1000, 6);
                MinimaxAI XPlayer = new MinimaxAI(4);
                var XPlayerRandomMove = game.GetRandomMove();
                game.MakeMove(XPlayerRandomMove.Row, XPlayerRandomMove.Col);
                
                // Perform game simulation
                while (!game.IsGameEnd())
                {
                    (int row, int col) OPlayerMove = OPlayer.GetBestMove(game);
                    game.MakeMove(OPlayerMove.row, OPlayerMove.col);
                    
                    if (!game.IsGameEnd())
                    {
                        (int row, int col) XPlayerMove = XPlayer.GetBestMove(game);
                        game.MakeMove(XPlayerMove.row, XPlayerMove.col);
                    }
                    if (game.IsGameEnd())
                    {
                        // Verify the winner
                        Player actualWinner = game.GetWinner();
                        Debug.WriteLine(actualWinner);
                        Debug.WriteLine(winCount);
                        Assert.IsTrue(actualWinner == Player.O || actualWinner == Player.N);
                    }
                }
                winCount+=1;
            }  
        }
        [TestMethod]
        public void TestAgainstHuman()
        {
            const int boardSize = 3;
            Player startingPlayer = Player.X;
            // Create a new Tic-Tac-Toe game instance
            TicTacToeGame game = new TicTacToeGame(boardSize, startingPlayer);
            MonteCarloTreeSearchAI OPlayer = new MonteCarloTreeSearchAI(2000, 6);
            game.MakeMove(2, 2);
            var OPlayerMove1 = OPlayer.GetBestMove(game);
            game.MakeMove(OPlayerMove1.Row, OPlayerMove1.Col);
            List<(int Row, int Col)> legalMoves = game.GetEmptyCells();
            if(legalMoves.Contains((0,0))){
                game.MakeMove(0, 0);
                var OPlayerMove2 = OPlayer.GetBestMove(game);
                game.MakeMove(OPlayerMove2.Row, OPlayerMove2.Col);
                game.PrintBoard();
                legalMoves = game.GetEmptyCells();
                if(legalMoves.Contains((2,0)))
                {
                    game.MakeMove(2,0);
                    var OPlayerMove3 = OPlayer.GetBestMove(game);
                    game.MakeMove(OPlayerMove3.Row, OPlayerMove3.Col);
                    game.PrintBoard();
                    if (!game.IsGameEnd())
                    {
                        legalMoves = game.GetEmptyCells();
                        if(legalMoves.Contains((1,0)))
                        {
                            game.MakeMove(1,0);
                            game.PrintBoard();
                            var winner = game.GetWinner();
                            Assert.AreNotEqual(Player.X, winner);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestBestChildMethod()
        {
            // Arrange
            TicTacToeGame game = new TicTacToeGame(3, Player.X);
            Node rootNode = new Node(null, (-1, -1), game, 6){
                Visits = 1
            };
            MonteCarloTreeSearchAI ai = new MonteCarloTreeSearchAI(1000, 6); // Adjust the parameters as needed

            // Create child nodes with different UCT values
            Node child1 = new Node(rootNode, (0, 0), new TicTacToeGame(3, Player.X), 6);
            child1.Wins = 10;
            child1.Visits = 50;

            Node child2 = new Node(rootNode, (0, 1), new TicTacToeGame(3, Player.X), 6);
            child2.Wins = 20;
            child2.Visits = 60;

            Node child3 = new Node(rootNode, (0, 2), new TicTacToeGame(3, Player.X), 6);
            child3.Wins = 15;
            child3.Visits = 55;

            rootNode.Children.Add(child1);
            rootNode.Children.Add(child2);
            rootNode.Children.Add(child3);

            // Act
            Node bestChild = ai.BestChild(rootNode);

            // Assert
            Assert.AreEqual(child2, bestChild); // Child2 has the highest UCT value
        }

        [TestMethod]
        public void GetRandomMove_ReturnsValidMove()
        {
            // Arrange
            var game = new TicTacToeGame(3, Player.X);
            game.MakeMove(0, 0); // Make a move to ensure the cell (0, 0) is not empty
            var aiPlayer = new MonteCarloTreeSearchAI(1000, 4);

            // Act
            var randomMove = game.GetRandomMove();

            // Assert
            Assert.IsTrue(randomMove.Row >= 0 && randomMove.Row <= 2);
            Assert.IsTrue(randomMove.Col >= 0 && randomMove.Col <= 2);
            Assert.AreEqual(Player.N, game.Board[randomMove.Row, randomMove.Col]);
        }
    }
}