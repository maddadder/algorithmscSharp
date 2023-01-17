using MSTMaze;

MazeGenerator mg = new MazeGenerator();
var maze = mg.GenerateMaze(true);
var list = MazeGenerator.convert(maze);
MazeGenerator.printList(list);
TestRenderGraph test = new TestRenderGraph();
