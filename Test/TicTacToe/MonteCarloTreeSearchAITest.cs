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
            for(var i = 0;i<100;i++)
            {
                Debug.WriteLine("NEW_GAME");
                // Create a new Tic-Tac-Toe game instance
                TicTacToeGame game = new TicTacToeGame(3, Player.X); // Adjust board size as needed

                // Create an instance of the MonteCarloTreeSearchAI
                MonteCarloTreeSearchAI aiPlayer = new MonteCarloTreeSearchAI(1500);

                // Perform game simulation
                while (!game.IsGameEnd())
                {
                    //game.PrintBoard();
                    (int row, int col) aiMove = aiPlayer.GetBestMove(game);
                    game.MakeMove(aiMove.row, aiMove.col);
                    if (game.IsGameEnd())
                    {
                        // Verify the winner
                        Player actualWinner = game.GetWinner();
                        Assert.IsTrue(actualWinner == Player.N);
                    }
                }
                
            }  
            
        }
        
        [TestMethod]
        public void TestSelectMethod()
        {
            // Create a Tic-Tac-Toe game instance
            TicTacToeGame game = new TicTacToeGame(3, Player.X);

            // Set up the initial game state
            // (You should set up the initial state based on your game mechanics)

            // Create an instance of the MonteCarloTreeSearchAI
            MonteCarloTreeSearchAI aiPlayer = new MonteCarloTreeSearchAI(1000);

            // Create a root node
            Node rootNode = new Node(null, (-1,-1), (TicTacToeGame)game.Clone());
            rootNode.Visits = 1;

            // Manually add child nodes with different statistics
            Node child1 = new Node(rootNode, (0, 0), (TicTacToeGame)game.Clone());
            Node child2 = new Node(rootNode, (0, 1), (TicTacToeGame)game.Clone());
            Node child3 = new Node(rootNode, (0, 2), (TicTacToeGame)game.Clone());

            child1.Visits = 10;
            child1.TotalReward = 5;

            child2.Visits = 20;
            child2.TotalReward = 10;

            child3.Visits = 15;
            child3.TotalReward = 8;

            rootNode.Children.Add(child1);
            rootNode.Children.Add(child2);
            rootNode.Children.Add(child3);

            // Call the Select method
            Node selectedNode = aiPlayer.Select(rootNode);

            // Assert that the selected node is the one with the highest UCT value
            Assert.AreEqual(child3, selectedNode);
        }

        [TestMethod]
        public void TestBestChildMethod()
        {
            // Create a Tic-Tac-Toe game instance
            TicTacToeGame game = new TicTacToeGame(3, Player.X);

            // Set up the initial game state
            // (You should set up the initial state based on your game mechanics)

            // Create an instance of the MonteCarloTreeSearchAI
            MonteCarloTreeSearchAI aiPlayer = new MonteCarloTreeSearchAI(1000);

            // Create a root node
            Node rootNode = new Node(null, (-1,-1), (TicTacToeGame)game.Clone());
            rootNode.Visits = 1;

            // Manually add child nodes with different statistics
            Node child1 = new Node(rootNode, (0, 0),(TicTacToeGame) game.Clone());
            Node child2 = new Node(rootNode, (0, 1), (TicTacToeGame)game.Clone());
            Node child3 = new Node(rootNode, (0, 2), (TicTacToeGame)game.Clone());

            child1.Visits = 10;
            child1.TotalReward = 5;

            child2.Visits = 20;
            child2.TotalReward = 10;

            child3.Visits = 15;
            child3.TotalReward = 8;

            rootNode.Children.Add(child1);
            rootNode.Children.Add(child2);
            rootNode.Children.Add(child3);

            // Call the BestChild method
            Node bestChild = aiPlayer.BestChild(rootNode);

            // Assert that the best child is the one with the highest UCT value
            // This assertion might depend on your UCT calculation method
            // You can adjust this assertion based on your UCT formula
            Assert.AreEqual(child3, bestChild);

            double explorationFactor = 2;

            double uctChild1 = (child1.TotalReward / child1.Visits) +
                            explorationFactor * Math.Sqrt(Math.Log(rootNode.Visits) / child1.Visits);

            double uctChild2 = (child2.TotalReward / child2.Visits) +
                            explorationFactor * Math.Sqrt(Math.Log(rootNode.Visits) / child2.Visits);

            double uctChild3 = (child3.TotalReward / child3.Visits) +
                            explorationFactor * Math.Sqrt(Math.Log(rootNode.Visits) / child3.Visits);

            // Check the order of UCT values and compare to the best child
            if (uctChild3 > uctChild1 && uctChild3 > uctChild2)
            {
                // child3 should be the best child
                Assert.AreEqual(child3, bestChild);
            }
            else{
                Assert.Fail();
            }

        }
        
        [TestMethod]
        public void TestCalculateUCTValueMethod()
        {
            // Create a sample node for testing
            Node node = new Node(null, (-1,-1), new TicTacToeGame(3, Player.X));

            // Set up node statistics for testing
            node.Visits = 10;
            node.TotalReward = 5;

            // Create an instance of the MonteCarloTreeSearchAI
            MonteCarloTreeSearchAI aiPlayer = new MonteCarloTreeSearchAI(1000);

            // Call the CalculateUCTValue method
            double uctValue = aiPlayer.CalculateUCTValue(node, 100); // Pass parent's visits count

            // Calculate the expected UCT value manually based on your formula
            double expectedExploitationValue = node.TotalReward / node.Visits;
            double expectedExplorationValue = 1.1 * Math.Sqrt(Math.Log(100) / node.Visits);
            double expectedUCTValue = expectedExploitationValue + expectedExplorationValue;

            // Assert that the calculated UCT value matches the expected value
            Assert.AreEqual(expectedUCTValue, uctValue, 1e-6); // Using a tolerance of 1e-6 for double comparison
        }

        [TestMethod]
        public void TestBackpropagate()
        {
            // Create a root node and child node
            Node rootNode = new Node(null, (0, 0), new TicTacToeGame(3, Player.X));
            Node childNode = new Node(rootNode, (0, 0), new TicTacToeGame(3, Player.X));
            
            // Simulate an outcome (assuming currentPlayer wins)
            Player winner = Player.X;
            
            // Call the Backpropagate method
            MonteCarloTreeSearchAI aiPlayer = new MonteCarloTreeSearchAI(1000);
            aiPlayer.Backpropagate(childNode, winner);

            // Assert that visit counts and total rewards are updated
            Assert.AreEqual(1, childNode.Visits); // Should be incremented to 1
            Assert.AreEqual(1, childNode.TotalReward); // Assuming winRewardValue is used for a win
            //Assert.AreEqual(0, rootNode.Visits); // Should remain 0 for the root node
            //Assert.AreEqual(0, rootNode.TotalReward); // Should remain 0 for the root node
        }

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
        public void TestSimulateToEnd()
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

            // Create an instance of the MonteCarloTreeSearchAI
            MonteCarloTreeSearchAI aiPlayer = new MonteCarloTreeSearchAI(1000); // Set the number of simulations

            Player winner = aiPlayer.SimulateToEnd(game);

            // Verify the expected winner (for the specific test scenario)
            Assert.AreEqual(Player.X, winner);
        }
        [TestMethod]
        public void TestSimulateToEnd_WinForCurrentPlayer()
        {
            const int boardSize = 3;
            // Set up a game state where current player can win
            TicTacToeGame game = new TicTacToeGame(3, Player.X);
            Player[,] testBoard = new Player[boardSize, boardSize]
            {
                { Player.X, Player.X, Player.O },
                { Player.X, Player.X, Player.O },
                { Player.O, Player.O, Player.X }
            };

            game.SetBoard(testBoard);

            MonteCarloTreeSearchAI aiPlayer = new MonteCarloTreeSearchAI(1000);
            Player winner = aiPlayer.SimulateToEnd(game);

            Assert.AreEqual(game.CurrentPlayer, winner);
        }

        [TestMethod]
        public void TestSimulateToEnd_BlockOpponentWin()
        {
            const int boardSize = 3;
            // Set up a game state where current player can win
            TicTacToeGame game = new TicTacToeGame(3, Player.O);
            Player[,] testBoard = new Player[boardSize, boardSize]
            {
                { Player.X, Player.X, Player.O },
                { Player.X, Player.X, Player.O },
                { Player.O, Player.N, Player.O }
            };

            game.SetBoard(testBoard);
            MonteCarloTreeSearchAI aiPlayer = new MonteCarloTreeSearchAI(1000);

            Player winner = aiPlayer.SimulateToEnd(game);

            Assert.AreEqual(Player.O, winner);
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

        [TestMethod]
        public void GetRandomMove_ReturnsValidMove()
        {
            // Arrange
            var game = new TicTacToeGame(3, Player.X);
            game.MakeMove(0, 0); // Make a move to ensure the cell (0, 0) is not empty
            var aiPlayer = new MonteCarloTreeSearchAI(1000);

            // Act
            var randomMove = game.GetRandomMove(game);

            // Assert
            Assert.IsTrue(randomMove.Row >= 0 && randomMove.Row <= 2);
            Assert.IsTrue(randomMove.Col >= 0 && randomMove.Col <= 2);
            Assert.AreEqual(Player.N, game.Board[randomMove.Row, randomMove.Col]);
        }
    }
}