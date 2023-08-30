using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.TicTacToe;

public class MonteCarloTreeSearchAI : IGameAI
{
    private int simulations;
    private int depth;
    public MonteCarloTreeSearchAI(int simulations, int depth){
        this.simulations = simulations;
        this.depth = depth;
    }
    public (int Row, int Col) GetBestMove(TicTacToeGame game)
    {
        Node rootNode = new Node(null, (-1, -1), game, 0);
        rootNode.Visits = 0;
        List<(int Row, int Col)> legalMoves = game.GetEmptyCells();

        foreach ((int Row, int Col) move in legalMoves)
        {
            TicTacToeGame nextState = (TicTacToeGame)game.Clone();
            nextState.MakeMove(move.Row, move.Col);
            Node childNode = new Node(rootNode, move, nextState, rootNode.Depth + 1);
            rootNode.Children.Add(childNode);
        }

        for (int i = 0; i < simulations; i++)
        {
            Node selectedNode = Select(rootNode);
            Node expandedNode = Expand(selectedNode, depth);
            
            // Simulate alternating player turns in the simulation
            expandedNode = SimulateToEnd(expandedNode);
            
            Backpropagate(expandedNode, expandedNode.State.GetWinner());
        }
        Node bestChild = rootNode.Children.OrderByDescending(child => child.Wins / child.Visits)
                                          .ThenByDescending(child => game.random.Next())
                                          .FirstOrDefault();
        return bestChild.Move;
    }

    public Node Select(Node node)
    {
        while (node.Children.Count > 0)
        {
            if (node.Children.Any(child => child.Visits == 0))
            {
                // If there are unvisited children, select the first one
                return node.Children.First(child => child.Visits == 0);
            }
            else
            {
                // Otherwise, select the best child using UCT
                node = BestChild(node);
            }
        }

        return node;
    }


    public Node Expand(Node node, int depth)
    {
        if (!node.State.IsGameEnd() && depth > 0)
        {
            List<(int Row, int Col)> legalMoves = node.State.GetEmptyCells();

            Node bestChild = null;
            double bestUCTValue = double.MinValue;

            foreach ((int Row, int Col) move in legalMoves)
            {
                if (!node.Children.Any(child => child.Move == move))
                {
                    TicTacToeGame nextState = (TicTacToeGame)node.State.Clone();
                    nextState.MakeMove(move.Row, move.Col);
                    Node child = new Node(node, move, nextState, node.Depth + 1);

                    node.Children.Add(child);

                    double uctValue = CalculateUCTValue(child, node.Visits); // Pass the parent's visits count
                    if (uctValue > bestUCTValue)
                    {
                        bestUCTValue = uctValue;
                        bestChild = child;
                    }
                }
            }

            if (bestChild != null)
            {
                return Expand(bestChild, depth - 1);
            }
            else
            {
                // If no unexpanded child was found, return the first unexpanded child
                return node.Children.FirstOrDefault(child => child.Visits == 0);
            }
        }

        return node;
    }



    public void Backpropagate(Node node, Player winner)
    {
        while (node != null)
        {
            // Update the visit count
            node.Visits++;

            //node.State.PrintBoard();
            // remember the the CurrentPlayer just got swapped out
            
            // Update the total reward based on the winner
            if (winner == node.State.GetOpponent(node.State.CurrentPlayer))
            {
                // If the node's player won, add 1 to the reward and wins
                node.Wins+=2;
            }
            else if (winner == node.State.CurrentPlayer)
            {
                // If the opponent won, subtract 1 from the reward and wins
                node.Wins-=2;
            }
            else if (winner == Player.N)
            {
                // For a draw, you can add a smaller reward or bias to encourage quicker wins
                //node.Draws+=1;
            }
            // Move up to the parent node for the next iteration
            node = node.Parent;
        }
    }



    public Node BestChild(Node node)
    {
        Node bestChild = null;
        double bestUCTValue = double.MinValue;

        foreach (Node child in node.Children)
        {
            double uctValue = CalculateUCTValue(child, node.Visits); // Pass the parent's visits count

            if (uctValue > bestUCTValue)
            {
                bestUCTValue = uctValue;
                bestChild = child;
            }
        }

        return bestChild;
    }

    public double CalculateUCTValue(Node node, int parentVisits)
    {
        if (node.Visits == 0)
            return double.PositiveInfinity;

        double explorationFactor = Math.Sqrt(2.0); // Adjust based on your exploration strategy

        double exploitationValue = (node.Wins) / node.Visits;
        double explorationValue = explorationFactor * Math.Sqrt(Math.Log(parentVisits) / node.Visits);
        return exploitationValue + explorationValue;
        
        // Factor in AI's estimation of potential future wins
        /*double potentialWinsEstimation = EstimatePotentialWins(node);
        double potentialWinsValue = potentialWinsEstimation / node.Visits;

        return exploitationValue + explorationValue + potentialWinsValue;*/
    }
    /*
    public double EstimatePotentialWins(Node node)
    {
        Player currentPlayer = node.State.CurrentPlayer;
        Player opponentPlayer = node.State.GetOpponent(currentPlayer);

        int currentPlayerPotentialWins = GetPlayerPotentialWins(node.State, currentPlayer);
        int opponentPotentialWins = GetPlayerPotentialWins(node.State, opponentPlayer);

        // Estimate the potential future wins for the AI
        // You can use your own heuristic or evaluation function here
        // For example, counting the number of potential wins based on the current state
        double estimatedWins = 0;
        if(currentPlayer == Player.X){
            estimatedWins = currentPlayerPotentialWins - opponentPotentialWins;
        }
        else if(currentPlayer == Player.O)
        {
            estimatedWins = opponentPotentialWins - currentPlayerPotentialWins;
        }
        return estimatedWins;
    }

    public int GetPlayerPotentialWins(TicTacToeGame game, Player player)
    {
        int potentialWins = 0;

        for (int row = 0; row < game.Board.GetLength(0); row++)
        {
            for (int col = 0; col < game.Board.GetLength(1); col++)
            {
                if (game.Board[row, col] == Player.N)
                {
                    TicTacToeGame copyGame = (TicTacToeGame)game.Clone();
                    copyGame.MakeMove(row, col);

                    if (copyGame.HasWin(player))
                    {
                        potentialWins+=1;
                    }
                }
            }
        }

        return potentialWins;
    }
    
    */
    public Node SimulateToEnd(Node node)
    {
        if (node.State.IsGameEnd())
        {
            return node;
        }

        List<(int Row, int Col)> legalMoves = node.State.GetEmptyCells();
        (int Row, int Col) randomMove = legalMoves[node.State.random.Next(legalMoves.Count)];

        node.State.MakeMove(randomMove.Row, randomMove.Col);
        return SimulateToEnd(node); // Recursively continue simulating
    }

}

public class Node
{
    public Node Parent { get; }
    public (int Row, int Col) Move { get; }
    public TicTacToeGame State { get; }
    public List<Node> Children { get; } = new List<Node>();
    public int Visits { get; set; } = 0;
    public int Wins { get; set; } = 0;
    public int Draws { get; set; } = 0;
    public int Depth { get; }
    public Node(Node parent, (int Row, int Col) move, TicTacToeGame state, int depth)
    {
        Parent = parent;
        Move = move;
        State = state;
        Children = new List<Node>();
        Visits = 0;
        Wins = 0;
        Draws = 0;
        Depth = depth;
    }
}