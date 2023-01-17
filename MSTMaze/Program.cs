using MSTMaze;

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