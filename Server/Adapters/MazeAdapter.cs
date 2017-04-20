using System.Collections.Generic;
using SearchAlgorithmsLib;
using SearchAlgorithmsLib.Interfaces;
using Server.Interfaces;
using MazeLib;

namespace Server.Adapters
{
    class MazeAdapter : ISearchable<Position>,IMaze,IDrawable
    {
        private Maze _maze;

        public Solution<Position> Solution { set; get; }

        public MazeAdapter(Maze maze)
        {
            _maze = maze;
            var p = new Position();
            var c = maze[p.Row, p.Col];
        }

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
        public List<State<Position>> GetAllPossibleStates(State<Position> state)
        {
            //TODO: add came from and cost
            var neighbors = new List<State<Position>>();
            var relativeDirections = new List<Direction>{Direction.Up,Direction.Right,Direction.Down,Direction.Left};
            
            var currentPosition = state.Value;            
            var neighbor = new Position();

            foreach (var direction in relativeDirections)
            {
                if(TryGetFreeNeighbor(direction,currentPosition,out neighbor))
                {
                    neighbors.Add(new State<Position>(neighbor));//ref neighbor));
                }
            }

            return neighbors;
        }

        public State<Position> GetGoalState()
        {
            Position goalPosition = _maze.GoalPos;
            return new State<Position>(goalPosition);
        }

        public State<Position> GetInitialState()
        {
            Position initialPosition = _maze.InitialPos;
            return new State<Position>(initialPosition);
        }
        #endregion

        #region IMaze interface implementation
        public int Cols
        {
            get { return _maze.Cols; }
        }

        public int Rows
        {
            get { return _maze.Rows; }
        }

        public Position GoalPos
        {
            get { return _maze.GoalPos; }
            set { _maze.GoalPos = value; }
        }

        public Position InitialPos
        {
            get { return _maze.InitialPos; }
            set { _maze.InitialPos = value; }
        }

        public CellType this[int row, int col]
        {
            get { return _maze[row, col]; }
            set { _maze[row, col] = value; }
        }
        #endregion

        #region IDrawable interface implementation
        public char Draw(int x, int y)
        {
            var position = new Position(y,x);
            switch (_maze[x, y])
            {
                case CellType.Free:
                    if (Solution == null || !Solution.Contains(position))
                    {
                        return ' ';
                    }
                    else
                    {
                        return '.';
                    }
                default:
                case CellType.Wall:
                    return '*';
            }
        }

        #endregion
    }
}
