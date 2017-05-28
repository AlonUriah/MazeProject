using MazeLib;
using MazeGame.Common;
using System;

namespace MazeGame.ViewModel.Interfaces
{
    public interface IMultiPlayerViewModel
    {
        int OpponentRow { set; get; }
        int OpponentColumn { set; get; }
        event GameStatusChangedHandler OnGameStatusChanged;
        MazeWrapper PlayerMaze { set; get; }
        void PlayerMoved(object sender, string direction);
        int PlayerRow { set; get; }
        int PlayerColumn { set; get; }
        char GetValueAtPos(Position pos);
        void Close();
        int Status { get; }
        bool IsBla { get; }
        event EventHandler OnGameReady;
    }
}
