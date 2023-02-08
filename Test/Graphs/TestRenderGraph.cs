using Lib.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs 
{
    [TestClass]
    public class TestRenderGraph
    {
        public TestRenderGraph(){
            
        }

        public static void RenderLines(string[] lines)
        {
            MathGraph<int> mst = new MathGraph<int>(true);
            Dictionary<int, Lib.Graphs.Vertex<int>> graph = MathGraph<int>.LoadGraph(mst, lines);
            MathGraph<int>.renderGraph(graph);
        }
        [TestMethod]
        public void RenderMaze1()
        {
            string sourceFile = "../../../../Data/Maze1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> mst = new MathGraph<int>(true);
            Dictionary<int, Lib.Graphs.Vertex<int>> graph = MathGraph<int>.LoadGraph(mst, lines);
            MathGraph<int>.renderGraph(graph);
        }
        private static int[] RandomList(int length)
        {
            Random rand = new Random();
            return Enumerable.Range(0, length)
                    .Select(i => new Tuple<int, int>(rand.Next(length), i))
                    .OrderBy(i => i.Item1)
                    .Select(i => i.Item2).ToArray();
        }
        [TestMethod]
        public void PrintRandomGraph()
        {
            for(var k = 0;k<2000;k++)
            {
                var inputX = RandomList((int)Math.Pow(10,1) * 3).ToArray();
                Random rand = new Random();
                MathGraph<int> graph = new MathGraph<int>(false);

                for (int i = 0; i < inputX.Length-1; i++)
                {
                    var nodeA = inputX[i];
                    var nodeB = inputX[i+1];

                    if(!graph.ContainsVertex(nodeA))
                    {
                        graph.AddVertex(nodeA);
                    }

                    if(!graph.ContainsVertex(nodeB))
                    {
                        graph.AddVertex(nodeB);
                    }
                    graph.AddEdge(nodeA, nodeB, 1);
                }
                var vertices = graph.GetVertices();
                Console.SetCursorPosition(0, 0);
                MathGraph<int>.renderGraph(vertices);
            }
        }
        //Visualize online at https://graphonline.ru/en/
        [TestMethod]
        public void printMST3AsAdjacencyMatrix()
        {
            string sourceFile = "../../../../Data/MST3.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> mst = new MathGraph<int>(true);
            Dictionary<int, Lib.Graphs.Vertex<int>> graph = MathGraph<int>.LoadGraph(mst, lines);
            MathGraph<int>.printAdjacencyMatrix(graph);
        }
    }
}