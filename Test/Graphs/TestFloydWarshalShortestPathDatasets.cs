using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Graphs;
namespace Graphs
{
    [TestClass]
    public class TestFloydWarshalShortestPathDatasets
    {
        
        [TestMethod]
        public void TestIn1()
        {
            string sourceFile = "../../../../Data/MST1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);

            MathGraph<int> graph = new MathGraph<int>(true);
            MathGraph<int>.LoadGraph(graph, lines);
            var floydWarshalDist = graph.FloydWarshal();
            var floydWarshalDistSum = floydWarshalDist.Item2[1].Sum(x => x.Value);
            Assert.AreEqual(6, floydWarshalDistSum);
        }
        [TestMethod]
        public void TestIn3()
        {
            string sourceFile = "../../../../Data/MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            MathGraph<int> graph = new MathGraph<int>(true);
            MathGraph<int>.LoadGraph(graph, lines);
            var floydWarshalDist = graph.FloydWarshal();
            var floydWarshalDistSum = floydWarshalDist.Item2[1].Sum(x => x.Value);
            Assert.AreEqual(34, floydWarshalDistSum);
        }
        [TestMethod]
        public void TestIn4()
        {
            string sourceFile = "../../../../Data/MST4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            MathGraph<int> graph = new MathGraph<int>(true);
            MathGraph<int>.LoadGraph(graph, lines);
            var floydWarshalDist = graph.FloydWarshal();
            var floydWarshalDistSum = floydWarshalDist.Item2[1].Sum(x => x.Value);
            Assert.AreEqual(6, floydWarshalDistSum);
        }
        [TestMethod]
        public void TestInBellmanFord1()
        {
            string sourceFile = "../../../../Data/BellmanFord1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            MathGraph<int> graph = new MathGraph<int>(true);
            MathGraph<int>.LoadGraph(graph, lines);
            var floydWarshalDist = graph.FloydWarshal();
            var floydWarshalDistSum = floydWarshalDist.Item2[1].Sum(x => x.Value);
            Assert.AreEqual(5, floydWarshalDistSum);
            var item1 = graph.GetVertices().Where(x => x.Key == 1).First();
            var item3 = graph.GetVertices().Where(x => x.Key == 3).First();
            var pathFrom1To3 = MathGraph<int>.FloydWarshalPath(item1.Key,item3.Key, floydWarshalDist.Item1);
            foreach(var path in pathFrom1To3){
                Debug.WriteLine(path);
            }
        }
        [TestMethod]
        public void TestInBellmanFord2()
        {
            string sourceFile = "../../../../Data/BellmanFord2.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
            MathGraph<int> graph = new MathGraph<int>(true);
            MathGraph<int>.LoadGraph(graph, lines);
            var floydWarshalDist = graph.FloydWarshal();
            Assert.AreEqual(null, floydWarshalDist);
        }
        [TestMethod]
        public void TestInBellmanFord3()
        {
            // Not enough RAM
            string sourceFile = "../../../../Data/BellmanFord3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
        }
        [TestMethod]
        public void TestInBellmanFord4()
        {
            string sourceFile = "../../../../Data/BellmanFord4.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
        }
        [TestMethod]
        public void TestInBellmanFord5()
        {
            string sourceFile = "../../../../Data/BellmanFord5.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
        }
        [TestMethod]
        public void TestInBellmanFord6()
        {
            string sourceFile = "../../../../Data/BellmanFord6.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            
        }
    }
}