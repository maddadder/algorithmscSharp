using MSTMaze;
using OBST;
using Lib.Wordle;
using Test.Encoding;

var mode = "Test_Tiny_OBST";


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
    else if(mode == "renderMaze1"){
        TestRenderGraph.RenderMaze1();
    }
    else if(mode == "PrintRandomGraph"){
        TestRenderGraph.PrintRandomGraph();
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
        TestRenderGraph.printMST3AsAdjacencyMatrix(); 
    }
    else if(mode == "Test_Tiny_OBST"){
        TestOBST.Test_Tiny_OBST();
    }
    else if(mode == "HuffmanAlgorithmTest"){
        HuffmanAlgorithmTest huffmanAlgorithmTest = new HuffmanAlgorithmTest();
        huffmanAlgorithmTest.HuffmanAlgorithm_Test();
    }
    else if(mode == "Wordle"){
        WordleSolver solver = new WordleSolver();
        solver.Wordle();
    }
}