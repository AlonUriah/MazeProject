using System;
using MazeGame.Common;
using MazeGame.ViewModel.Interfaces;
using MazeLib;

namespace MazeGame.ViewModel
{
    abstract class MultiplayerViewModelBase : IMultiplayerViewModel
    {
        private int _opponentRow;
        private int _opponentCol;
        private ISinglePlayerViewModel _singlePlayerViewModel;

        abstract public MazeWrapper OponnentMaze { set; get; }
        public MazeWrapper PlayerMaze
        {
            set
            {
                _singlePlayerViewModel.PlayerMaze = value;
            }
            get
            {
                return _singlePlayerViewModel.PlayerMaze;
            }
        }

        public int OpponentColumn
        {
            get
            {
                return _opponentCol;
            }

            set
            {
                _opponentCol = value;
            }
        }
        public int OpponentRow
        {
            get
            {
                return _opponentRow;
            }

            set
            {
                _opponentRow = value;
            }
        }
        public int PlayerColumn
        {
            get
            {
                return _singlePlayerViewModel.PlayerColumn;
            }

            set
            {
                _singlePlayerViewModel.PlayerColumn = value;
            }
        }
        public int PlayerRow
        {
            get
            {
                return _singlePlayerViewModel.PlayerRow;
            }

            set
            {
                _singlePlayerViewModel.PlayerRow = value;
            }
        }

        public string[] List
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void PlayerMoved(object sender, string direction)
        {
            _singlePlayerViewModel.PlayerMoved(sender, direction);
        }

        public char GetValueAtPos(Position pos)
        {
            throw new NotImplementedException();
        }

        public void Close(int code)
        {
            throw new NotImplementedException();
        }
    }
}
