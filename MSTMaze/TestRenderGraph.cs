using Lib.Graphs;
namespace MSTMaze 
{
    public class TestRenderGraph
    {
        public TestRenderGraph(){
            string sourceFile = "../../../Maze1.txt";
            string[] lines = System.IO.File.ReadAllLines(sourceFile);
            MathGraph<int> mst = new MathGraph<int>();
            var isUndirectedGraph = false;
            SortedDictionary<int, Lib.Graphs.Vertex<int>> graph = MathGraph<int>.managePrimsMST(mst, lines, isUndirectedGraph);
            MathGraph<int>.renderGraph(graph);
        }

        public TestRenderGraph(string[] lines)
        {
            MathGraph<int> mst = new MathGraph<int>();
            var isUndirectedGraph = false;
            SortedDictionary<int, Lib.Graphs.Vertex<int>> graph = MathGraph<int>.managePrimsMST(mst, lines, isUndirectedGraph);
            MathGraph<int>.renderGraph(graph);
        }
    }
}