using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
namespace Test
{
    [TestClass]
    public class TestFindShortestPathImdbActorMovie
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
                /*
                if(names.Contains(" ("))
                {
                    string[] newName = names.Split(' ');
                    names = newName[0] + ' ' + newName[1];
                }
                */
                if(!graph.ContainsVertex(names))
                {
                    graph.AddVertex(names);
                }

                if(!graph.ContainsVertex(movies))
                {
                    graph.AddVertex(movies);
                }
                graph.AddEdge(names, movies, 1, isUndirectedGraph:true);
            }
            var components = graph.CountComponents();
            Debug.WriteLine($"Components: {components}");

            string actor1 = "Carrie Fisher";
            string actor2 = "Michael Caine (I)";

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
            int degree = (results.Count - 1);
            Debug.WriteLine($"{actor1} has been in {graph.CountAdjacent(actor1)} movie(s) and {actor2} has been in {graph.CountAdjacent(actor2)} movie(s).");
            Debug.WriteLine($"The degree of separation between {actor1} and {actor2} is {degree}.");
            Debug.WriteLine("SHORTEST PATH:");

            //7. Display the path from one person to the other
            for (int j = 0; j < results.Count - 2; j += 2)
            {
                Debug.WriteLine($"{results[j]} was in {results[j + 1]} with {results[j + 2]}.");
            }
            var test = graph.Dijkstra(actor1);
            Debug.WriteLine($"Dijkstra's Distance from {actor1} to {actor2} is {test[actor2]}");
            
            var source = "Kevin Spacey";

            float expectedCost = float.PositiveInfinity;

            // Act
            graph.prims_mst(source); 
            var graph_mst = graph.GetVertices();
            
            // Assert
            Assert.AreEqual(expectedCost, graph_mst.Sum(x => graph.GetComponentWeights()[x.Key]));

        }
    }
}
