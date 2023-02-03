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
            var bellmanDistA = MathGraph<int>.manageFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);
        }
        [TestMethod]
        public void TestInBellmanFord1()
        {
            string sourceFile = "../../../BellmanFord1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);
        }
        [TestMethod]
        public void TestInBellmanFord2()
        {
            string sourceFile = "../../../BellmanFord2.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);
        }
        [TestMethod]
        public void TestInBellmanFord3()
        {
            string sourceFile = "../../../BellmanFord3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> graph = new MathGraph<int>();
            var bellmanDistA = MathGraph<int>.manageFord(graph, lines);
            var bellmanDistASum = bellmanDistA.Sum(x => x.Value);
            var bellmanDistB = Lib.Graphs.v2.Graph.manageFord(lines);
            var bellmanDistBSum = bellmanDistB.Sum(x => x);
            // Assert
            Assert.AreEqual(bellmanDistASum, bellmanDistBSum);
        }
    }
}