using System.Threading;
using System.Configuration;
using MazeGame.Model.Interfaces;
using MazeLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MazeGame.Common;
using System;

namespace MazeGame.ViewModel
{
    class SinglePlayerViewModel : SinglePlayerViewModelBase, IDisposable
    {
        const char LEFT_CHAR = '3';
        const char RIGHT_CHAR = '2';
        const char UP_CHAR = '1';
        const char DOWN_CHAR = '0';
        const char FREE_CELL_CHAR = '0';
        const int ERROR_CODE = 0;
        const int SUCCESS_CODE = 1;
        const int LOADING_CODE = 2;

        private ISingleplayerModel _model;
        private MazeWrapper _maze;
        private bool isSolved = false;
        
        public SinglePlayerViewModel(ISingleplayerModel model, string name, int rows, int cols)
        {
            _model = model;
            _model.OnGameReceived += UpdateMaze;
            Status = _model.CreateNewGame(name, rows, cols);
        }

        private void UpdateMaze(object sender, string game)
        {
            JObject gameJason = null;
            OnPlayerMoved += SinglePlayerMoved;
            try
            {
                gameJason = JObject.Parse(game);
            }
            catch (JsonReaderException)
            {
                Status = ERROR_CODE;
            }

            _maze = gameJason.ToMazeWrapper();
            Status = (_maze != null) ? SUCCESS_CODE : ERROR_CODE;
        }

        public override MazeWrapper PlayerMaze
        {
            get
            {
                return _maze;
            }

            set
            {
                _maze = value;
            }
        }
 
        private bool IsValidMove(string direction, out Position position)
        {
            int row = PlayerRow;
            int column = PlayerColumn;

            switch (direction.ToLower())
            {
                case "up":
                    row++;
                    break;
                case "down":
                    row--;
                    break;
                case "left":
                    column--;
                    break;
                case "right":
                    column++;
                    break;
            }

            position = new Position(row, column);
            return (_maze[row, column] == FREE_CELL_CHAR);
        }
        public void SinglePlayerMoved(object sender, string direction)
        {
            Position position;
            if (!IsValidMove(direction, out position)) return;

            PlayerColumn = position.Col;
            PlayerRow = position.Row;

            if(PlayerColumn == _maze.Cols && PlayerRow == _maze.Rows)
            {
                DidWin = true;
            }
        }
        public override void Solve()
        {
            int algorithm = Properties.Settings.Default.Algorithm;            
            _model.OnSolutionReceived += AnimateSolution;
            _model.SolveMaze(_maze.Name,algorithm);
        }
        private void AnimateSolution(object sender, string solution)
        {
            foreach(char dirChr in solution)
            {
                switch (dirChr)
                {
                    case LEFT_CHAR:
                        PlayerColumn--;
                        break;
                    case RIGHT_CHAR:
                        PlayerColumn++;
                        break;
                    case UP_CHAR:
                        PlayerRow++;
                        break;
                    case DOWN_CHAR:
                        PlayerRow--;
                        break;
                }
                Thread.Sleep(1000);
            }
            isSolved = true;
        }
        public override void Restart()
        {
            PlayerRow = _maze.StartRow;
            PlayerColumn = _maze.StartCol;
        }
        public override char GetValueAtPos(Position pos)
        {
            return _maze[pos.Row, pos.Col];
        }
        public override void Close()
        {
            if (!isSolved)
            {
                int algorithm = Properties.Settings.Default.Algorithm;
                _model.OnSolutionReceived -= AnimateSolution;
                _model.SolveMaze(_maze.Name, algorithm);
            }
        }

        public void Dispose()
        {
            _model.Close();
        }
    }
}