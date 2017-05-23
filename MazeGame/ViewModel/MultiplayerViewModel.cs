using System;
using MazeGame.Common;
using MazeGame.Model.Interfaces;

using MazeLib;

namespace MazeGame.ViewModel
{
    class MultiplayerViewModel : MultiplayerViewModelBase
    {
        private  IMultiplayerModel _model;

        public override MazeWrapper OponnentMaze
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public MultiplayerViewModel(IMultiplayerModel model)
        {
            _model = model;
            _model.OnOpponentMoved += OpponentMoved;
        }

        private void OpponentMoved(object sender, string direction)
        {
            int row = OpponentRow;
            int column = OpponentColumn;

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

            OpponentRow = row;
            OpponentColumn = column;
        }
    }
}
