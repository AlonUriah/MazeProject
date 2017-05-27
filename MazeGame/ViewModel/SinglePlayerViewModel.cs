using System.Threading;
using System.ComponentModel;
using MazeGame.Model.Interfaces;
using MazeLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MazeGame.Common;
using System;
using MazeGame.ViewModel.Interfaces;

namespace MazeGame.ViewModel
{
    public delegate void PlayerMovedHandler(object sender, string direction);

    class SinglePlayerViewModel : ISinglePlayerViewModel, IDisposable, INotifyPropertyChanged
    {
        #region Movement Constants
        const char LEFT_CHAR = '0';
        const char RIGHT_CHAR = '1';
        const char UP_CHAR = '3';
        const char DOWN_CHAR = '2';
        #endregion

        #region Board Constants
        const char FREE_CELL_CHAR = '0';
        const char START_CELL_CHAR = '*';
        const char END_CELL_CHAR = '#';
        const char WALL_CELL_CHAR = '1';
        #endregion

        #region Communication Codes
        const int ERROR_CODE = 0;
        const int SUCCESS_CODE = 1;
        const int LOADING_CODE = 2;
        #endregion 

        private ISingleplayerModel _model;
        private MazeWrapper _maze;
        private int _playerColumn;
        private int _playerRow;
        private bool _didWin = false;
        private bool isSolved = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public event SolutionReceivedHandler OnSolutionReceived;
        public event GenerateGameHandler OnGameReceived;
        public event PlayerMovedHandler OnPlayerMoved;

        public MazeWrapper PlayerMaze
        {
            get
            {
                return _maze;
            }
            set
            {
                _maze = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PlayerMaze"));
            }
        }
        public int PlayerColumn
        {
            get
            {
                return _playerColumn;
            }
            set
            {
                _playerColumn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlayerColumn"));
            }
        }
        public int PlayerRow
        {
            get
            {
                return _playerRow;
            }

            set
            {
                _playerRow = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlayerRow"));
            }
        }
        public int Status { protected set; get; }
        public bool DidWin
        {
            set
            {
                _didWin = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DidWin"));
            }get
            {
                return _didWin;
            }
        }

        public SinglePlayerViewModel(ISingleplayerModel model, string name, int rows, int cols)
        {
            _model = model;
            _model.OnGameReceived += UpdateMaze;
            _model.OnSolutionReceived += AnimateSolution;
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
            PlayerRow = _maze.StartRow;
            PlayerColumn = _maze.StartCol;
            Status = (_maze != null) ? SUCCESS_CODE : ERROR_CODE;
        }

        private bool IsValidMove(string direction, out Position position)
        {
            int row = PlayerRow;
            int column = PlayerColumn;

            switch (direction.ToLower())
            {
                case "up":
                    row--;
                    break;
                case "down":
                    row++;
                    break;
                case "left":
                    column--;
                    break;
                case "right":
                    column++;
                    break;
            }

            position = new Position(row, column);
            if (row < 0 || column < 0 || row > _maze.Rows - 1 || column > _maze.Cols - 1)
            {
                return false;
            }

            return (_maze[row, column] != WALL_CELL_CHAR);
        }
        public virtual void PlayerMoved(object sender, string direction)
        {
            if (OnPlayerMoved != null)
                OnPlayerMoved.Invoke(sender, direction);
        }
        public void SinglePlayerMoved(object sender, string direction)
        {
            Position position;
            if (!IsValidMove(direction, out position)) return;

            PlayerColumn = position.Col;
            PlayerRow = position.Row;

            if(PlayerColumn == _maze.EndCol && PlayerRow == _maze.EndRow)
            {
                DidWin = true;
            }
        }
        public void Solve()
        {
            int algorithm = Properties.Settings.Default.Algorithm;            
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
                        PlayerRow--;
                        break;
                    case DOWN_CHAR:
                        PlayerRow++;
                        break;
                }
                Thread.Sleep(200);
            }
            isSolved = true;
        }
        public void Restart()
        {
            PlayerRow = _maze.StartRow;
            PlayerColumn = _maze.StartCol;
        }
        public char GetValueAtPos(Position pos)
        {
            return _maze[pos.Row, pos.Col];
        }
        public void Close()
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