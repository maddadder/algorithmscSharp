using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lib.TicTacToe;

public class MonteCarloTreeSearchAI : IGameAI
{
    private int simulations;
    public MonteCarloTreeSearchAI(int simulations){
        this.simulations = simulations;
    }
    public (int Row, int Col) GetBestMove(TicTacToeGame game)
    {
        Node rootNode = new Node(null, (-1, -1), game);
        rootNode.Visits = 0;
        List<(int Row, int Col)> legalMoves = game.GetEmptyCells();

        foreach ((int Row, int Col) move in legalMoves)
        {
            TicTacToeGame nextState = (TicTacToeGame)game.Clone();
            nextState.MakeMove(move.Row, move.Col);
            Node childNode = new Node(rootNode, move, nextState);
            rootNode.Children.Add(childNode);
        }

        for (int i = 0; i < simulations; i++)
        {
            Node selectedNode = Select(rootNode);
            Node expandedNode = Expand(selectedNode);
            
            // Simulate alternating player turns in the simulation
            Player winner = SimulateToEnd(expandedNode.State);
            
            Backpropagate(expandedNode, winner);
        }
        Node bestChild = rootNode.Children.OrderByDescending(child => child.TotalReward / child.Visits)
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


    public Node Expand(Node node)
    {
        if (!node.State.IsGameEnd() && node.Children.Count < node.State.GetEmptyCells().Count)
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
                    Node child = new Node(node, move, nextState);

                    node.Children.Add(child);

                    double uctValue = CalculateUCTValue(child, node.Visits);
                    if (uctValue > bestUCTValue)
                    {
                        bestUCTValue = uctValue;
                        bestChild = child;
                    }
                }
            }

            if (bestChild != null)
            {
                return bestChild;
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

            // remember the the CurrentPlayer just got swapped out

            // Update the total reward based on the winner
            if (winner == node.State.GetOpponent(node.State.CurrentPlayer) && winner != Player.N)
            {
                // If the node's player won, add 1 to the reward and wins
                node.TotalReward += 1;
                node.Wins++;
            }
            else if (winner == node.State.CurrentPlayer && winner != Player.N)
            {
                // If the opponent won, subtract 1 from the reward and wins
                node.TotalReward -= 1;
            }
            else if (winner == Player.N)
            {
                // For a draw, you can add a smaller reward or bias to encourage quicker wins
                node.TotalReward += 0;
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
        double explorationFactor = 2; // Adjust based on your exploration strategy

        double exploitationValue = node.TotalReward / node.Visits;
        double explorationValue = explorationFactor * Math.Sqrt(Math.Log(parentVisits) / node.Visits);

        return exploitationValue + explorationValue;
    }

    public Player SimulateToEnd(TicTacToeGame game)
    {
        if (game.IsGameEnd())
        {
            return game.GetWinner();
        }

        List<(int Row, int Col)> legalMoves = game.GetEmptyCells();
        (int Row, int Col) randomMove = legalMoves[game.random.Next(legalMoves.Count)];

        game.MakeMove(randomMove.Row, randomMove.Col);
        return SimulateToEnd(game); // Recursively continue simulating
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
    public double TotalReward { get; set; } // Total rewards accumulated during simulations

    public Node(Node parent, (int Row, int Col) move, TicTacToeGame state)
    {
        Parent = parent;
        Move = move;
        State = state;
        Children = new List<Node>();
        Visits = 0;
        TotalReward = 0;
        Wins = 0;
    }
}