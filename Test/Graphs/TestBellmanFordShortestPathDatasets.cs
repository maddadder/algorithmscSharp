using System.Diagnostics;
using Lib.Graphs.v2;
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
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageBellmanFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var DijkstraDistA = graphv1.Dijkstra(1);
            var DijkstraDistSum = DijkstraDistA.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, DijkstraDistSum);

            var bellmanDistC = graphv1.BellmanFord(1);
            var bellmanDistCSum = bellmanDistC.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, bellmanDistCSum);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageBellmanFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var DijkstraDistA = graphv1.Dijkstra(1);
            var DijkstraDistSum = DijkstraDistA.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, DijkstraDistSum);

            var bellmanDistC = graphv1.BellmanFord(1);
            var bellmanDistCSum = bellmanDistC.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, bellmanDistCSum);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageBellmanFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var DijkstraDistA = graphv1.Dijkstra(1);
            var DijkstraDistSum = DijkstraDistA.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, DijkstraDistSum);

            var bellmanDistC = graphv1.BellmanFord(1);
            var bellmanDistCSum = bellmanDistC.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, bellmanDistCSum);
        }
        [TestMethod]
        public void TestInBellmanFord1()
        {
            string sourceFile = "../../../BellmanFord1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageBellmanFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var bellmanDistC = graphv1.BellmanFord(1);
            var bellmanDistCSum = bellmanDistC.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, bellmanDistCSum);
        }
        [TestMethod]
        public void TestInBellmanFord2()
        {
            string sourceFile = "../../../BellmanFord2.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            // Assert
            Assert.AreEqual(null, bellmanDistA);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var bellmanDistC = graphv1.BellmanFord(1);
            Assert.AreEqual(null, bellmanDistC);
        }
        [TestMethod]
        public void TestInBellmanFord3()
        {
            string sourceFile = "../../../BellmanFord3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            // Assert
            Assert.AreEqual(null, bellmanDistA);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var bellmanDistC = graphv1.BellmanFord(1);
            Assert.AreEqual(null, bellmanDistC);
        }
        [TestMethod]
        public void TestInBellmanFord4()
        {
            string sourceFile = "../../../BellmanFord4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            // Assert
            Assert.AreEqual(null, bellmanDistA);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var bellmanDistC = graphv1.BellmanFord(1);
            Assert.AreEqual(null, bellmanDistC);
        }
        [TestMethod]
        public void TestInBellmanFord5()
        {
            string sourceFile = "../../../BellmanFord5.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageBellmanFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var bellmanDistC = graphv1.BellmanFord(1);
            var bellmanDistCSum = bellmanDistC.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, bellmanDistCSum);
        }
        [TestMethod]
        public void TestInBellmanFord6()
        {
            string sourceFile = "../../../BellmanFord6.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageBellmanFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageBellmanFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);

            Lib.Graphs.MathGraph<int> graphv1 = new Lib.Graphs.MathGraph<int>();
            Lib.Graphs.MathGraph<int>.LoadGraph(graphv1, lines, isUndirectedGraph: false);
            var bellmanDistC = graphv1.BellmanFord(1);
            var bellmanDistCSum = bellmanDistC.Sum(x => x.Value);
            Assert.AreEqual(bellmanDistASum, bellmanDistCSum);
        }
    }
}