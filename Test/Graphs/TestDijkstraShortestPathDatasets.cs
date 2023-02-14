using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestDijkstraShortestPathDatasets
    {
        [TestMethod]
        public void TestRandomGraph()
        {
            var inputGraph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.GenerateGraph(inputGraph, 5, 8, 0);
            var bf = inputGraph.BellmanFord(1);
            var bfSum = bf.Item1.Select(x => x.Value).Sum();
            var dijkstra = inputGraph.Dijkstra(1);
            var dijkstraSum = dijkstra.Item1.Select(x => x.Value).Sum();
            Assert.AreEqual(bfSum, dijkstraSum);
        }
        
        [TestMethod]
        public void TestIn1()
        {
            string sourceFile = "../../../../Data/MST1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);

            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var dijkstra = graph.Dijkstra(1);
            var dijkstraDist = dijkstra.Item1;
            var dijkstraDistSum = dijkstraDist.Sum(x => x.Value);
            Assert.AreEqual(6, dijkstraDistSum);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../../Data/MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var dijkstra = graph.Dijkstra(1);
            var dijkstraDist = dijkstra.Item1;
            var dijkstraDistSum = dijkstraDist.Sum(x => x.Value);
            Assert.AreEqual(34, dijkstraDistSum);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../../Data/MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var dijkstra = graph.Dijkstra(1);
            var dijkstraDist = dijkstra.Item1;
            var dijkstraDistSum = dijkstraDist.Sum(x => x.Value);
            Assert.AreEqual(6, dijkstraDistSum);
        }

        
    }
}