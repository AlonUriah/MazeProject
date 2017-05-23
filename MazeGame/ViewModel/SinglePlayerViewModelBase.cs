using MazeLib;
using MazeGame.ViewModel.Interfaces;
using MazeGame.Common;

namespace MazeGame.ViewModel
{
    public delegate void PlayerMovedHandler(object sender, string direction);

    abstract class SinglePlayerViewModelBase : ISinglePlayerViewModel
    {
        private int _playerColumn;
        private int _playerRow;

        public int PlayerColumn
        {
            get
            {
                return _playerColumn;
            }
            set
            {
                _playerColumn = value;
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
            }
        }

        public event PlayerMovedHandler OnPlayerMoved;
        public virtual void PlayerMoved(object sender, string direction)
        {
            if (OnPlayerMoved != null)
                OnPlayerMoved.Invoke(sender, direction);
        }

        abstract public void Restart();
        abstract public void Solve();
        abstract public char GetValueAtPos(Position pos);
        abstract public MazeWrapper PlayerMaze { set; get; }
    }
}