using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
namespace Test
{
    [TestClass]
    public class TestFindShortestPath
    {
        [TestMethod]
        public void Test_FindShortestPath()
        {
            //1. Load the movie database from the IMDB file into a graph object
            string sourceFile = "../../../imdb.top250.txt";
            if(!File.Exists(sourceFile))
            {
                Debug.WriteLine($"'{sourceFile}' cannot be found.");
                return;
            }

            string[] lines = File.ReadAllLines(sourceFile);
            var random = new Random(); 
            MathGraph<string> graph = new MathGraph<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] vertices = lines[i].Split('|');
                string names = vertices[0];
                string movies = vertices[1];

                if(names.Contains(" ("))
                {
                    string[] newName = names.Split(' ');
                    names = newName[0] + ' ' + newName[1];
                }

                if(!graph.ContainsVertex(names))
                {
                    graph.AddVertex(names);
                }

                if(!graph.ContainsVertex(movies))
                {
                    graph.AddVertex(movies);
                }
                graph.AddEdge(names, movies, 1);
            }

            string actor1 = "Kevin Bacon";
            string actor2 = "Harrison Ford";

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
            List<string> results = graph.FindShortestPath(actor1, actor2);

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
            var test = graph.Dijkstra(actor1);
            foreach(var t in test.Where(x => x.Key == actor2)){
                Debug.WriteLine($"{t.Key},{t.Value/2}");
            }

            var source = "Kevin Spacey";

            float expectedCost = 68212000;

            // Act
            var actualCost = graph.prims_mst(source);
            
            // Assert
            Assert.AreEqual(expectedCost, actualCost);

            var components = graph.CountConnectedTo(source);
            Debug.WriteLine($"Components: {components}");
        }
    }
}
