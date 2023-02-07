
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lib.MST 
{
    public class MazeGenerator
    {
        public MazeGenerator(){

        }
        public MazeGenerator(int height, int width)
        {
            _cells = new int[height,width];
        }
        private readonly Random _rnd = new();
        // NOTE: cells grid dimensions must be odd, odd (will give size 1 border around maze)
        private readonly int[,] _cells = new int[41,41]; // All maze cells default to wall (false), not path (true)

        private struct CellPosition {
            public int X;
            public int Y;

            public override string ToString() {
                return $"{X}, {Y}";
            }
        }
        
        public int[,] GenerateMaze(bool render) {
            if(render)
                Console.Clear();

            // Random starting position must be odd, odd (will give size 1 border around maze)

            //var posRnd = new CellPosition {
            //    X = _rnd.Next(0, _cells.GetLength(0)),
            //    Y = _rnd.Next(0, _cells.GetLength(1))
            //};
            var posRnd = new CellPosition {
                X = 1,
                Y = 1
            };
            setCell(posRnd, 1); // Set initial random cell to path

            var candidateCells = new HashSet<CellPosition>();
            candidateCells.UnionWith(getCandidateCellsFor(posRnd, false)); // Get cell's wall candidates
            while (candidateCells.Count > 0) {
                // Pick random cell from candidate collection
                var thisCell = candidateCells.ElementAt(_rnd.Next(0, candidateCells.Count));

                // Get cell's path candidates
                var pathCandidates = getCandidateCellsFor(thisCell, true);

                if (pathCandidates.Count > 0) {
                    // Connect random path candidate with cell
                    connectCell(pathCandidates[_rnd.Next(0, pathCandidates.Count)], thisCell);
                }

                // Add this candidate cell's wall candidates to collection to process
                candidateCells.UnionWith(getCandidateCellsFor(thisCell, false));

                // Remove this candidate call from hashset collection
                candidateCells.Remove(thisCell);
                if(render){
                    renderMaze(_cells);
                    Thread.Sleep(50);
                }
            }

            return _cells;
        }

        /// <summary>
        /// Sets the cell in the given position to the given state.
        /// </summary>
        /// <param name="posRnd">The position of the cell to set.</param>
        /// <param name="isPath">The state to set the cell to.  If true, sets to path; otherwise, sets to wall.</param>
        private void setCell(CellPosition posRnd, int isPath) {
            _cells[posRnd.X, posRnd.Y] = isPath;
        }

        /// <summary>
        /// Connects two cells that are a distance of 2 apart with path, where cell A is already a path cell.
        /// </summary>
        /// <param name="cellA">Path cell to connect cell B to.</param>
        /// <param name="cellB">Wall cell to connect to cell A.</param>
        private void connectCell(CellPosition cellA, CellPosition cellB) {
            var x = (cellA.X + cellB.X) / 2;
            var y = (cellA.Y + cellB.Y) / 2;
            _cells[cellB.X, cellB.Y] = 1;
            _cells[x, y] = 1;
        }

        private bool cellHasValidPosition(CellPosition position) {
            return
                position.X >= 0 &&
                position.Y >= 0 &&
                position.X < _cells.GetLength(0) &&
                position.Y < _cells.GetLength(1);
        }

        /// <summary>
        /// Gets candidate cells for a given cell given its position.
        /// </summary>
        /// <param name="position">The cell's position.</param>
        /// <param name="getPathCells">If true, gets path candidate cells; otherwise, gets wall candidate cells.</param>
        /// <returns>The candidate cells for the given cell.</returns>
        private IList<CellPosition> getCandidateCellsFor(CellPosition position, bool getPathCells) {
            var candidatePathCells = new List<CellPosition>();
            var candidateWallCells = new List<CellPosition>();

            var northCandidate = new CellPosition { X = position.X, Y = position.Y - 2 };
            var eastCandidate = new CellPosition { X = position.X + 2, Y = position.Y };
            var southCandidate = new CellPosition { X = position.X, Y = position.Y + 2 };
            var westCandidate = new CellPosition { X = position.X - 2, Y = position.Y };

            if (cellHasValidPosition(northCandidate)) {
                if (_cells[northCandidate.X, northCandidate.Y] == 1) { candidatePathCells.Add(northCandidate); }
                else { candidateWallCells.Add(northCandidate); }
            }
            if (cellHasValidPosition(eastCandidate)) {
                if (_cells[eastCandidate.X, eastCandidate.Y] == 1) { candidatePathCells.Add(eastCandidate); }
                else { candidateWallCells.Add(eastCandidate); }
            }
            if (cellHasValidPosition(southCandidate)) {
                if (_cells[southCandidate.X, southCandidate.Y] == 1) { candidatePathCells.Add(southCandidate); }
                else { candidateWallCells.Add(southCandidate); }
            }
            if (cellHasValidPosition(westCandidate)) {
                if (_cells[westCandidate.X, westCandidate.Y] == 1) { candidatePathCells.Add(westCandidate); }
                else { candidateWallCells.Add(westCandidate); }
            }

            if (getPathCells) { return candidatePathCells; }
            else { return candidateWallCells; }
        }

        private static void renderMaze(int[,] maze, string title = "Generating maze...") {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(title);
            Console.WriteLine("");
            for (var x = 0; x < maze.GetLength(0); x++) {
                for (var y = 0; y < maze.GetLength(1); y++) {
                    Console.Write($"{(maze[x,y] == 1 ? "⬜" : "⬛")}");
                }
                Console.WriteLine("");
            }
        }
        public static string renderMazeHtml(int[,] maze, string title = "Generating maze...") {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table cellpadding=0 cellspacing=0 style='line-height:1'>");
            for (var x = 0; x < maze.GetLength(0); x++) {
                sb.Append("<tr>");
                for (var y = 0; y < maze.GetLength(1); y++) 
                {
                    var cell = ($"{(maze[x,y] == 1 ? "⬜" : "⬛")}");
                    sb.Append($"<td>{cell}</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }
        public static void printAdjacencyMatrix(int[,] maze, string title = "Generating maze...") {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(title);
            Console.WriteLine("");
            for (var x = 0; x < maze.GetLength(0); x++) {
                for (var y = 0; y < maze.GetLength(1); y++) {
                    if(y == maze.GetLength(1) - 1)
                        Console.Write($"{(maze[x,y] == 1 ? "1" : "0")}");
                    else
                        Console.Write($"{(maze[x,y] == 1 ? "1," : "0,")}");
                }
                Console.WriteLine("");
            }
        }
        // Function to convert adjacency
        // list to adjacency matrix
        public static List<List<int>> convertToAdjacencyList(int[,] adjacencyMatrix)
        {
            // no of vertices
            int l = adjacencyMatrix.GetLength(0);
            List<List<int>> adjListArray = new List<List<int>>(l);
            int i, j;

            // Create a new list for each
            // vertex such that adjacent
            // nodes can be stored
            for (i = 0; i < l; i++)
            {
                adjListArray.Add(new List<int>());
            }


            for (i = 0; i < adjacencyMatrix.GetLength(0); i++)
            {
                for (j = 0; j < adjacencyMatrix.GetLength(1); j++)
                {
                    if (adjacencyMatrix[i,j] == 1)
                    {
                        adjListArray[i].Add(j);
                    }
                }
            }

            return adjListArray;
        }

        // Function to print the adjacency list
        public static string[] convertToEdgeList(List<List<int>> adjListArray)
        {
            List<string> lines = new List<string>();
            
            lines.Add("1 1"); //<-- unknown and ignored
            var first = "";
            // Print the adjacency list
            for (int v = 0; v < adjListArray.Count; v++)
            {
                foreach (int u in adjListArray[v])
                {
                    if(string.IsNullOrEmpty(first))
                        first = v.ToString();
                    lines.Add(v + " " + u + " " + 1);
                }
            }
            lines.Add(first);
            return lines.ToArray();
        }
    }
}