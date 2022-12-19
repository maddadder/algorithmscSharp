using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
namespace Test
{
    [TestClass]
    public class TestFindShortestPath2
    {
        public static int[] RandomList(int length)
        {
            Random rand = new Random();
            return Enumerable.Range(0, length)
                    .Select(i => new Tuple<int, int>(rand.Next(length), i))
                    .OrderBy(i => i.Item1)
                    .Select(i => i.Item2).ToArray();
        }

        [TestMethod]
        public void Test_FindShortestPath2()
        {
            var inputX = RandomList((int)Math.Pow(10,1));
           
            MathGraph<int> graph = new MathGraph<int>();

            for (int i = 0; i < inputX.Length-1; i++)
            {
                var names = inputX[i];
                var movies = inputX[i+1];

                if(!graph.ContainsVertex(names))
                {
                    graph.AddVertex(names);
                }

                if(!graph.ContainsVertex(movies))
                {
                    graph.AddVertex(movies);
                }
                graph.AddEdge(names, movies);
            }

            var actor1 = inputX[0];
            var actor2 = inputX[inputX.Length-1];

            if(!graph.ContainsVertex(actor1)) // check actor 1 exists
            {
                Debug.WriteLine($"Actor '{actor1}' not found.");
                return;
            }

            if (!graph.ContainsVertex(actor2)) // check actor 2 exists
            {
                Debug.WriteLine($"Actor '{actor2}' not found.");
                return;
            }

            //5. Find the shortest path from one actor/ actress to the other
            var results = graph.FindShortestPath(actor1, actor2);

            //6. Calculate the degrees of separation score
            int degree = (results.Count - 1) / 2;
            Debug.WriteLine($"{actor1} has been in {graph.CountAdjacent(actor1)} movie(s) and {actor2} has been in {graph.CountAdjacent(actor2)} movie(s).");
            Debug.WriteLine($"The degree of separation between {actor1} and {actor2} is {degree}.");
            Debug.WriteLine("SHORTEST PATH:");

            //7. Display the path from one person to the other
            for (int j = 0; j < results.Count - 2; j += 2)
            {
                Debug.WriteLine($"{results[j]} was in {results[j + 1]} with {results[j + 2]}.");
            }

        }
    }
}
