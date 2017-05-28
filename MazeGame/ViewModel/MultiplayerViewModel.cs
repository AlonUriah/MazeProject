using MazeGame.Common;
using MazeGame.Model.Interfaces;
using MazeGame.ViewModel.Interfaces;
using MazeLib;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MazeGame.ViewModel
{
    class MultiplayerViewModel : IMultiPlayerViewModel, INotifyPropertyChanged
    {
        public bool IsBla { private set; get; } = false;

        #region Board Constants
        const char FREE_CELL_CHAR = '0';
        const char START_CELL_CHAR = '*';
        const char END_CELL_CHAR = '#';
        const char WALL_CELL_CHAR = '1';
        #endregion

        const int PLAYER_WON = 0;
        const int OPPONENT_WON = 1;
        const int OPPONENT_QUIT = 2;

        private const int PLAYER_ID = 1;
        private const int OPPONENT_ID = 2;

        private  IMultiplayerModel _model;
        private MazeWrapper _playerMaze;
        private int _playerRow;
        private int _playerColumn;
        private int _opponentRow;
        private int _opponentColumn;
        private int _status;


        public event GameStatusChangedHandler OnGameStatusChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public int PlayerRow
        {
            set
            {
                _playerRow = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlayerRow"));
            }
            get
            {
                return _playerRow;
            }
        }
        public int PlayerColumn
        {
            set
            {
                _playerColumn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlayerColumn"));
            }
            get
            {
                return _playerColumn;
            }
        }
        public int OpponentRow
        {
            set
            {
                _opponentRow = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OpponentRow"));
            }
            get
            {
                return _opponentRow;
            }
        }
        public int OpponentColumn
        {
            set
            {
                _opponentColumn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OpponentColumn"));
            }
            get
            {
                return _opponentColumn;
            }
        }
        
        public MazeWrapper PlayerMaze
        {
            set
            {
                _playerMaze = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlayerMaze"));
            }
            get
            {
                return _playerMaze;
            }
        }
      
        public int Status
        {
            private set
            {
                _status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
            }
            get
            {
                return _status;
            }
        }

        public MultiplayerViewModel(IMultiplayerModel model, string gameName)
        {
            _model = model;
            _model.OnPlayerMoved += PlayerMoved;
            _model.OnOpponentMoved += OpponentMoved;
            _model.OnGameReceived += UpdateMaze;
            _model.JoinGame(gameName);
        }

        public MultiplayerViewModel(IMultiplayerModel model, string gameName, int mazeCols, int mazeRows)
        {
            _model = model;
            _model.OnPlayerMoved += PlayerMoved;
            _model.OnOpponentMoved += OpponentMoved;
            _model.OnGameReceived += UpdateMaze;
            _model.StartGame(gameName, mazeRows, mazeCols);
        }
        
        private void UpdateMaze(object sender, string gameJason)
        {
            JObject jObject;
            try
            {
                jObject = JObject.Parse(gameJason.TrimJasonEnd());
            }
            catch (JsonReaderException)
            {
                Status = 2;
                return;
            }

            PlayerMaze = jObject.ToMazeWrapper();
            PlayerRow = PlayerMaze.StartRow;
            PlayerColumn = PlayerMaze.StartCol;
            OpponentRow = PlayerMaze.StartRow;
            OpponentColumn = PlayerMaze.StartCol;

            Status = (PlayerMaze != null) ? 1 : 0;
        }

        private bool IsValidMove(int row, int column, string direction, out Position position)
        {
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
            if (row < 0 || column < 0 || row > _playerMaze.Rows - 1 || column > _playerMaze.Cols - 1)
            {
                return false;
            }

            return (_playerMaze[row, column] != WALL_CELL_CHAR);
        }

        private void OpponentMoved(object sender, string direction)
        {
            Position position;
            if(IsValidMove(OpponentRow, OpponentColumn,direction, out position))
            {
                OpponentRow = position.Row;
                OpponentColumn = position.Col;
                if(DidWin(OpponentRow, OpponentColumn))
                {
                    OnGameStatusChanged?.Invoke(this, OPPONENT_WON);
                    _model.OnPlayerMoved -= PlayerMoved;
                    _model.OnOpponentMoved -= OpponentMoved;
                }
            }            
        }

        public void PlayerMoved(object sender, string direction)
        {
            Position position;
            if(IsValidMove(PlayerRow, PlayerColumn, direction, out position))
            {
                PlayerRow = position.Row;
                PlayerColumn = position.Col;
                _model.Move(direction);
                if (DidWin(PlayerRow, PlayerColumn))
                {
                    OnGameStatusChanged?.Invoke(this, PLAYER_WON);
                    _model.OnPlayerMoved -= PlayerMoved;
                    _model.OnOpponentMoved -= OpponentMoved;
                }
            }
        }

        private bool DidWin(int row, int column)
        {
            return (row == _playerMaze.EndRow && column == _playerMaze.EndCol);
        }
        
        public char GetValueAtPos(Position pos)
        {
            return _playerMaze[pos.Row, pos.Col];
        }

        public void Close()
        {
            _model.Close();
        }
    }
}
