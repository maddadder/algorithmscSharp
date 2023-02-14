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
            string sourceFile = "../../../../Data/MST5.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>(false);

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
            var dijkstra = graph.Dijkstra(1);
            var pathDistances = dijkstra.Item1;
            var distance = pathDistances.Where(x => x.Key == node2).First().Value;
            Debug.WriteLine($"The distance between {5} and {node2} is {distance}.");
        }
        
    }
}