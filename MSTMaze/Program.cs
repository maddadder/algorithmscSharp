using MSTMaze;

MazeGenerator mg = new MazeGenerator();
var maze = mg.GenerateMaze(true);
var list = MazeGenerator.convertToAdjacencyList(maze);
var lines = MazeGenerator.convertToEdgeList(list);
TestRenderGraph test = new TestRenderGraph(lines);
