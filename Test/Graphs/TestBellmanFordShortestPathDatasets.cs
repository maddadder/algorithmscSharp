using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestBellmanFordShortestPathDatasets
    {
        
        [TestMethod]
        public void TestIn1()
        {
            string sourceFile = "../../../MST1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);

            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Sum(x => x.Value);
            Assert.AreEqual(6, bellmanDistSum);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Sum(x => x.Value);
            Assert.AreEqual(34, bellmanDistSum);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Sum(x => x.Value);
            Assert.AreEqual(6, bellmanDistSum);
        }
        [TestMethod]
        public void TestInBellmanFord1()
        {
            string sourceFile = "../../../BellmanFord1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Sum(x => x.Value);
            Assert.AreEqual(5, bellmanDistSum);
        }
        [TestMethod]
        public void TestInBellmanFord2()
        {
            string sourceFile = "../../../BellmanFord2.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            Assert.AreEqual(null, bellmanDist);
        }
        [TestMethod]
        public void TestInBellmanFord3()
        {
            string sourceFile = "../../../BellmanFord3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            Assert.AreEqual(null, bellmanDist);
        }
        [TestMethod]
        public void TestInBellmanFord4()
        {
            string sourceFile = "../../../BellmanFord4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            Assert.AreEqual(null, bellmanDist);
        }
        [TestMethod]
        public void TestInBellmanFord5()
        {
            string sourceFile = "../../../BellmanFord5.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Sum(x => x.Value);
            Assert.AreEqual(-1344, bellmanDistSum);
        }
        [TestMethod]
        public void TestInBellmanFord6()
        {
            string sourceFile = "../../../BellmanFord6.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines, isUndirectedGraph: false);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Sum(x => x.Value);
            Assert.AreEqual(296435, bellmanDistSum);
        }
    }
}