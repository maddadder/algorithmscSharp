using Lib.Wordle;
using Test.Encoding;
using Lib.MST;
var mode = "MazeGenerator";


RunTest(mode);
//RunAll();

static void RunAll()
{
    var modes = new List<string>(){
        "MazeGenerator",
        "renderMaze1",
        "printAdjacencyMatrix",
        "printAdjacencyList",
        "visualizeGraph",
        "Test_Tiny_OBST",
        "HuffmanAlgorithmTest",
        "PrintRandomGraph",
        "Wordle"
    };
    foreach(var mode in modes){
        RunTest(mode);
    }
}

static void RunTest(string mode){
    if(mode == "MazeGenerator"){
        MazeGenerator mg = new MazeGenerator();
        var maze = mg.GenerateMaze(true);
    }
    else if(mode == "renderMaze1")
    {
        Graphs.TestRenderGraph test = new Graphs.TestRenderGraph();
        test.RenderMaze1();
    }
    else if(mode == "PrintRandomGraph"){
        Graphs.TestRenderGraph test = new Graphs.TestRenderGraph();
        test.PrintRandomGraph();
    }
    else if(mode == "printAdjacencyMatrix"){
        MazeGenerator mg = new MazeGenerator();
        var maze = mg.GenerateMaze(false);
        MazeGenerator.printAdjacencyMatrix(maze);
    }
    else if(mode == "printAdjacencyList"){
        MazeGenerator mg = new MazeGenerator();
        var maze = mg.GenerateMaze(false);
        var list = MazeGenerator.convertToAdjacencyList(maze);
        var lines = MazeGenerator.convertToEdgeList(list);
        foreach(var line in lines){
            Console.WriteLine(line);
        }
    }
    else if(mode == "visualizeGraph"){
        //Visualize online at https://graphonline.ru/en/
        Graphs.TestRenderGraph test = new Graphs.TestRenderGraph();
        test.printMST3AsAdjacencyMatrix(); 
    }
    else if(mode == "Test_Tiny_OBST"){
        Test.DynamicProgramming.OptimalBinarySearchTreeTest test = new Test.DynamicProgramming.OptimalBinarySearchTreeTest();
        test.Test_Tiny_OBST();
    }
    else if(mode == "HuffmanAlgorithmTest"){
        HuffmanAlgorithmTest huffmanAlgorithmTest = new HuffmanAlgorithmTest();
        huffmanAlgorithmTest.Render_HuffmanAlgorithm();
    }
    else if(mode == "Wordle"){
        WordleSolver solver = new WordleSolver();
        solver.Wordle();
    }
}