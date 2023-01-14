using System.Diagnostics;
using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestFindShortestPathDatasets
    {
        [TestMethod]
        public void TestIn5()
        {
            //https://www.cs.utah.edu/~lifeifei/SpatialDataset.htm
            //California Road Network's Edges (Edge ID, Start Node ID, End Node ID, L2 Distance)
            //https://www.cs.utah.edu/~lifeifei/research/tpq/cal.cedge
            string sourceFile = "../../../MST5.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();

            float expectedCost = 307.6319f;

            // Act
            var graph_mst = MathGraph<int>.managePrimsMST(graph, lines);
        
            // Assert
            var sum = graph_mst.Sum(x => graph.GetComponentWeights()[x.Key]);
            Assert.AreEqual(expectedCost, sum);
            Debug.WriteLine($"Components: {graph.CountComponents()}");

            var node1 = 0;
            var node2 = 1164;
            var results = graph.FindShortestPath(node1, node2);

            //6. Calculate the degrees of separation score
            int degree = (results.Count - 1) / 2;
            Debug.WriteLine($"{node1} has been in {graph.CountAdjacent(node1)} node(s) and {node2} has been in {graph.CountAdjacent(node2)} node(s).");
            Debug.WriteLine($"The degree of separation between {node1} and {node2} is {degree}.");
            Debug.WriteLine("SHORTEST PATH:");

            //7. Display the path from one person to the other
            for (int j = 0; j < results.Count - 2; j += 2)
            {
                Debug.WriteLine($"{results[j]} was in {results[j + 1]} with {results[j + 2]}.");
            }
            var pathDistances = graph.Dijkstra(node1);
            var distance = pathDistances.Where(x => x.Key == node2).First().Value;
            Debug.WriteLine($"The distance between {5} and {node2} is {distance}.");
        }
        [TestMethod]
        public void TestIn6()
        {
            //https://snap.stanford.edu/data/soc-sign-bitcoin-otc.html
            string sourceFile = "../../../MST6.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();

            float expectedCost = 593805;

            // Act
            var graph_mst = MathGraph<int>.managePrimsMST(graph, lines);
        
            // Assert
            var sum = graph_mst.Sum(x => graph.GetComponentWeights()[x.Key]);
            Assert.AreEqual(expectedCost, sum);
            Debug.WriteLine($"Components: {graph.CountComponents()}");
            var components = graph.GetComponents();
            var node1 = 6;
            var node2 = 3219;

            //use mst to get distance
            var node2_key = graph_mst.Where(x => x.Key == node2).First().Key;
            var distance2 = graph.GetComponentWeights()[node2_key];
            Debug.WriteLine($"The distance between {node1} and {node2} is {distance2}.");
            
            while(graph.GetParent(node2_key) != 0)
            {
                Debug.WriteLine($"The parent of {node2_key} is {graph.GetParent(node2_key)}"); 
                node2_key = graph.GetParent(node2_key);
            }

            var results = graph.FindShortestPath(node1, node2);

            //6. Calculate the degrees of separation score
            int degree = (results.Count - 1) / 2;
            Debug.WriteLine($"{node1} has been in {graph.CountAdjacent(node1)} node(s) and {node2} has been in {graph.CountAdjacent(node2)} node(s).");
            Debug.WriteLine($"The degree of separation between {node1} and {node2} is {degree}.");
            Debug.WriteLine("SHORTEST PATH:");

            //7. Display the path from one person to the other
            for (int j = 0; j < results.Count - 2; j += 2)
            {
                Debug.WriteLine($"{results[j]} was in {results[j + 1]} with {results[j + 2]}.");
            }
            //this doesn't work because weights have negative values
            var pathDistances = graph.Dijkstra(node1);
            var distance = pathDistances.Where(x => x.Key == node2).First();
            Debug.WriteLine($"The distance between {node1} and {node2} is {distance.Value}.");

            
        }
    }
}