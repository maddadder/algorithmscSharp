using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
namespace Test
{
    [TestClass]
    public class TestFindShortestPathRandom
    {
        public static int[] RandomList(int length)
        {
            Random rand = new Random();
            return Enumerable.Range(1, length)
                    .Select(i => new Tuple<int, int>(rand.Next(1, length), i))
                    .OrderBy(i => i.Item1)
                    .Select(i => i.Item2).ToArray();
        }

        [TestMethod]
        public void Test_FindShortestPathRandom()
        {
            var inputX = RandomList((int)Math.Pow(10,1)).OrderBy(x => x).ToArray();
            Random rand = new Random();
            MathGraph<int> graph = new MathGraph<int>();

            for (int i = 0; i < inputX.Length-1; i++)
            {
                var nodeA = inputX[i];
                var nodeB = inputX[i+1];

                if(!graph.ContainsVertex(nodeA))
                {
                    graph.AddVertex(nodeA);
                }

                if(!graph.ContainsVertex(nodeB))
                {
                    graph.AddVertex(nodeB);
                }
                graph.AddEdge(nodeA, nodeB, 1);
            }
            
            var components = graph.CountComponents();
            Debug.WriteLine($"Components: {components}");

            var node1 = inputX[0];
            var node2 = inputX[inputX.Length-1];

            if(!graph.ContainsVertex(node1)) // check actor 1 exists
            {
                Debug.WriteLine($"node '{node1}' not found.");
                return;
            }

            if (!graph.ContainsVertex(node2)) // check actor 2 exists
            {
                Debug.WriteLine($"node '{node2}' not found.");
                return;
            }

            //5. Find the shortest path from one actor/ actress to the other
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
            var pathDistances = graph.Dijkstra(5);
            var distance = pathDistances.Where(x => x.Key == node2).First().Value;
            Debug.WriteLine($"The distance between {5} and {node2} is {distance}.");
            graph.printComponentWeights(5);


            var source = 5;

            float expectedCost = 9;

            // Act
            graph.prims_mst(source);
            var graph_mst = graph.GetVertices();
            // Assert
            Assert.AreEqual(expectedCost, graph_mst.Sum(x => graph.GetComponentWeights()[x.Key]));

            var t = graph.CalculateMWISFrom(1);
            Debug.WriteLine(t);
            Assert.AreEqual(30, t);
        }
    }
}