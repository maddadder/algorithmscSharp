using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs
{
    [TestClass]
    public class TestBellmanFordShortestPathDatasets
    {
        [TestMethod]
        public void TestInRandom()
        {
            var connectedGraph = float.PositiveInfinity;
            Lib.Graphs.MathGraph<int> inputGraph = new Lib.Graphs.MathGraph<int>(true);
            while(connectedGraph == float.PositiveInfinity){
                inputGraph = new Lib.Graphs.MathGraph<int>(true);
                inputGraph.GenerateGraph(5, 8, inputGraph);
                var bellmanDist = inputGraph.BellmanFord(1);
                if(bellmanDist != null)
                    connectedGraph = bellmanDist.Item1.Sum(x => x.Value);
            }
            Assert.AreNotEqual(float.PositiveInfinity, connectedGraph);
            var data = inputGraph.BellmanFord(1);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadBellmanFordDistances(graph, data.Item2, data.Item1);
            var diagram = graph.GenerateDot();
        }
        
        [TestMethod]
        public void TestIn1()
        {
            string sourceFile = "../../../../Data/MST1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);

            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Item1.Sum(x => x.Value);
            Assert.AreEqual(6, bellmanDistSum);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../../Data/MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Item1.Sum(x => x.Value);
            Assert.AreEqual(34, bellmanDistSum);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../../Data/MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Item1.Sum(x => x.Value);
            Assert.AreEqual(6, bellmanDistSum);
        }
        [TestMethod]
        public void TestInBellmanFord1()
        {
            string sourceFile = "../../../../Data/BellmanFord1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> inputGraph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(inputGraph, lines);
            var bellmanDist = inputGraph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Item1.Sum(x => x.Value);
            Assert.AreEqual(5, bellmanDistSum);

            var graphviz = inputGraph.GenerateDot();
            Debug.WriteLine(graphviz);

            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadBellmanFordDistances(graph, bellmanDist.Item2, bellmanDist.Item1);
            var bellmanDist2 = graph.BellmanFord(1);
            var bellmanDist2Sum = bellmanDist2.Item1.Sum(x => x.Value);
            Assert.AreEqual(5, bellmanDist2Sum);
            var graphviz2 = graph.GenerateDot();
            Debug.WriteLine(graphviz2);

            var graphs = Lib.Graphs.MathGraph<int>.LoadBellmanFordPathsFromGraph(graph);
            foreach(var _graph in graphs.Values)
            {
                if(_graph.GetVertices().Any()){
                    graphviz2 = _graph.GenerateDot();
                    Debug.WriteLine(graphviz2);
                }
            }
            graphs = Lib.Graphs.MathGraph<int>.LoadJohnsonPathsFromGraph(inputGraph);
            foreach(var _graph in graphs.Values)
            {
                if(_graph.GetVertices().Any()){
                    graphviz2 = _graph.GenerateDot();
                    Debug.WriteLine(graphviz2);
                }
            }
            
        }
        [TestMethod]
        public void TestInBellmanFord2()
        {
            string sourceFile = "../../../../Data/BellmanFord2.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var bellmanDist = graph.BellmanFord(1);
            Assert.AreEqual(null, bellmanDist);
        }
        [TestMethod]
        public void TestInBellmanFord3()
        {
            string sourceFile = "../../../../Data/BellmanFord3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var bellmanDist = graph.BellmanFord(1);
            Assert.AreEqual(null, bellmanDist);
        }
        [TestMethod]
        public void TestInBellmanFord4()
        {
            string sourceFile = "../../../../Data/BellmanFord4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var bellmanDist = graph.BellmanFord(1);
            Assert.AreEqual(null, bellmanDist);
        }
        [TestMethod]
        public void TestInBellmanFord5()
        {
            string sourceFile = "../../../../Data/BellmanFord5.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Item1.Sum(x => x.Value);
            Assert.AreEqual(-1344, bellmanDistSum);
        }
        [TestMethod]
        public void TestInBellmanFord6()
        {
            string sourceFile = "../../../../Data/BellmanFord6.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            Lib.Graphs.MathGraph<int> graph = new Lib.Graphs.MathGraph<int>(true);
            Lib.Graphs.MathGraph<int>.LoadGraph(graph, lines);
            var bellmanDist = graph.BellmanFord(1);
            var bellmanDistSum = bellmanDist.Item1.Sum(x => x.Value);
            Assert.AreEqual(296435, bellmanDistSum);
        }
    }
}