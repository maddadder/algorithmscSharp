using MSTMaze;
using Lib.DynamicProgramming;
/*
MazeGenerator mg = new MazeGenerator();
var maze = mg.GenerateMaze(true);
//MazeGenerator.printAdjacencyMatrix(maze); // ==> https://graphonline.ru/en/
var list = MazeGenerator.convertToAdjacencyList(maze);
var lines = MazeGenerator.convertToEdgeList(list);

//foreach(var line in lines){
//    Console.WriteLine(line);
//}
TestRenderGraph.PrintRandomGraph();
//TestRenderGraph.printMST3AsAdjacencyMatrix(); //https://graphonline.ru/en/

*/
var keys = new int [] {1, 2, 3};
var freq = new float[] {.8f, .1f, .1f};
OptimalBinarySearchTree obst = new OptimalBinarySearchTree();
var n = keys.Length;
var value = obst.ComputeCost(keys, freq, n);
