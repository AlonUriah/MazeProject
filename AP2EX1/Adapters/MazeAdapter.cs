using System.Collections.Generic;
using SearchAlgorithmsLib;
using SearchAlgorithmsLib.Interfaces;
using MazeLib;
using AP2EX1.Interfaces;

namespace AP2EX1.Adapters
{
    /// <summary>
    /// Adapter for Maze.
    /// This MazeAdapter is Searchable and Drawable.
    /// </summary>
    class MazeAdapter : ISearchable<Position>,IMaze,IDrawable
    {
        private Maze _maze;

        /// <summary>
        /// Ctor.
        /// Initiate _maze class member.
        /// </summary>
        /// <param name="maze">A maze to adapt to an ISearchable object</param>
        public MazeAdapter(Maze maze)
        {
            _maze = maze;
        }

        /// <summary>
        /// Try to get a free neihbor in a relative location
        /// from currentPos.
        /// </summary>
        /// <param name="relativeLocation">Direction to look at, relative to currentLocation</param>
        /// <param name="currentPos">Current Position in Maze</param>
        /// <param name="neighbor">Out Position if cell type is Free</param>
        /// <returns>Returns true if a neighnor in relativeLocation is free</returns>
        private bool TryGetFreeNeighbor(Direction relativeLocation, Position currentPos, out Position neighbor)
        {
            int neighborCol = currentPos.Col, neighborRow = currentPos.Row;

            switch (relativeLocation)
            {
                case Direction.Left:
                    neighborCol--;
                    break;
                case Direction.Right:
                    neighborCol++;
                    break;
                case Direction.Down:
                    neighborRow--;
                    break;
                case Direction.Up:
                default:
                    neighborRow++;
                    break;
            }

            neighbor = new Position(neighborRow, neighborCol);

            if (neighborCol < 0 || neighborRow < 0 || 
                neighborCol == _maze.Cols || neighborRow == _maze.Rows ||
                _maze[neighborRow,neighborCol]==CellType.Wall)
            {
                return false;
            }

            return true;
        }

        #region ISearchable interface implementation
        /// <summary>
        /// Returns all available moves from current state.
        /// </summary>
        /// <param name="state">Current state to look from</param>
        /// <returns>A list of states containing all free neighbors</returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> state)
        {
            var neighbors = new List<State<Position>>();
            var relativeDirections = new List<Direction>{Direction.Up,Direction.Right,Direction.Down,Direction.Left};
            
            Position currentPosition = state.Value;            
            var neighbor = new Position();

            foreach (var direction in relativeDirections)
            {
                if(TryGetFreeNeighbor(direction,currentPosition,out neighbor))
                {
                    neighbors.Add(new State<Position>(neighbor));
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Returns _maze Goal State
        /// </summary>
        /// <returns>Goal state of _maze</returns>
        public State<Position> GetGoalState()
        {
            Position goalPosition = _maze.GoalPos;
            return new State<Position>(goalPosition);
        }

        /// <summary>
        /// Returns _maze Initial State
        /// </summary>
        /// <returns>Initial state of _maze</returns>
        public State<Position> GetInitialState()
        {
            Position initialPosition = _maze.InitialPos;
            return new State<Position>(initialPosition);
        }
        #endregion

        #region IMaze interface implementation
        /// <summary>
        /// Returns _maze Cols
        /// </summary>
        public int Cols
        {
            get
            {
                return _maze.Cols;
            }
        }

        /// <summary>
        /// Returns _maze Rows
        /// </summary>
        public int Rows
        {
            get
            {
                return _maze.Rows;
            }
        }

        /// <summary>
        /// _maze GoalPos Property accessor
        /// </summary>
        public Position GoalPos
        {
            get
            {
                return _maze.GoalPos;
            }
            set
            {
                _maze.GoalPos = value;
            }
        }

        /// <summary>
        /// _maze InitialPos Property accessor
        /// </summary>
        public Position InitialPos
        {
            get
            {
                return _maze.InitialPos;
            }
            set
            {
                _maze.InitialPos = value;
            }
        }

        /// <summary>
        /// _maze Indexer accessor
        /// Returns _maze CellType in a specific location
        /// </summary>
        /// <param name="row">Row in _maze</param>
        /// <param name="col">Col in _maze</param>
        /// <returns>CellType - Free/Wal in a specific location</returns>
        public CellType this[int row, int col]
        {
            get
            {
                return _maze[row, col];
            }
            set
            {
                _maze[row, col] = value;
            }
        }
        #endregion

        #region IDrawable interface implementation
        /// <summary>
        /// Returns a CellType in location [x,y] char representation
        /// </summary>
        /// <param name="x">Cell location x axis</param>
        /// <param name="y">Cell location y axis</param>
        /// <returns>A CellType representation</returns>
        public char Draw(int x, int y)
        {
            const char FREE_SYMBOL = ' ';
            const char WALL_SYMBOL = '*';

            var position = new Position(y,x);
            if(_maze[x,y] == CellType.Wall)
                return WALL_SYMBOL;

            return FREE_SYMBOL;
        }

        #endregion
    }
}
