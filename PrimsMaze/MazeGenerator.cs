namespace PrimsMaze 
{
    public class MazeGenerator
    {
        private readonly Random _rnd = new();
        // NOTE: cells grid dimensions must be odd, odd (will give size 1 border around maze)
        private readonly bool[,] _cells = new bool[21,21]; // All maze cells default to wall (false), not path (true)

        private struct CellPosition {
            public int X;
            public int Y;

            public override string ToString() {
                return $"{X}, {Y}";
            }
        }

        public bool[,] GenerateMaze() {
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
            setCell(posRnd, true); // Set initial random cell to path

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

                renderMaze(_cells);
                Thread.Sleep(50);
            }

            return _cells;
        }

        /// <summary>
        /// Sets the cell in the given position to the given state.
        /// </summary>
        /// <param name="posRnd">The position of the cell to set.</param>
        /// <param name="isPath">The state to set the cell to.  If true, sets to path; otherwise, sets to wall.</param>
        private void setCell(CellPosition posRnd, bool isPath) {
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
            _cells[cellB.X, cellB.Y] = true;
            _cells[x, y] = true;
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
                if (_cells[northCandidate.X, northCandidate.Y]) { candidatePathCells.Add(northCandidate); }
                else { candidateWallCells.Add(northCandidate); }
            }
            if (cellHasValidPosition(eastCandidate)) {
                if (_cells[eastCandidate.X, eastCandidate.Y]) { candidatePathCells.Add(eastCandidate); }
                else { candidateWallCells.Add(eastCandidate); }
            }
            if (cellHasValidPosition(southCandidate)) {
                if (_cells[southCandidate.X, southCandidate.Y]) { candidatePathCells.Add(southCandidate); }
                else { candidateWallCells.Add(southCandidate); }
            }
            if (cellHasValidPosition(westCandidate)) {
                if (_cells[westCandidate.X, westCandidate.Y]) { candidatePathCells.Add(westCandidate); }
                else { candidateWallCells.Add(westCandidate); }
            }

            if (getPathCells) { return candidatePathCells; }
            else { return candidateWallCells; }
        }

        private static void renderMaze(bool[,] maze, string title = "Generating maze...") {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine("");
            for (var x = 0; x < maze.GetLength(0); x++) {
                for (var y = 0; y < maze.GetLength(1); y++) {
                    Console.Write($"{(maze[x,y] ? "⬜" : "⬛")}");
                }
                Console.WriteLine("");
            }
        }
    }
}