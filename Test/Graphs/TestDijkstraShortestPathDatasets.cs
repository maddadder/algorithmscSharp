using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestDijkstraShortestPathDatasets
    {
        
        [TestMethod]
        public void TestIn1()
        {
            string sourceFile = "../../../../Data/MST1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);

            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var dijkstraDist = graph.Dijkstra(1);
            var dijkstraDistSum = dijkstraDist.Sum(x => x.Value);
            Assert.AreEqual(6, dijkstraDistSum);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../../Data/MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var dijkstraDist = graph.Dijkstra(1);
            var dijkstraDistSum = dijkstraDist.Sum(x => x.Value);
            Assert.AreEqual(34, dijkstraDistSum);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../../Data/MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var dijkstraDist = graph.Dijkstra(1);
            var dijkstraDistSum = dijkstraDist.Sum(x => x.Value);
            Assert.AreEqual(6, dijkstraDistSum);
        }
        
    }
}