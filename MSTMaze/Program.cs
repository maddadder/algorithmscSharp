using MSTMaze;

MazeGenerator mg = new MazeGenerator();
var maze = mg.GenerateMaze(true);
//MazeGenerator.printAdjacencyMatrix(maze);
var list = MazeGenerator.convertToAdjacencyList(maze);
var lines = MazeGenerator.convertToEdgeList(list);
//foreach(var line in lines){
//    Console.WriteLine(line);
//}
TestRenderGraph.PrintRandomGraph();