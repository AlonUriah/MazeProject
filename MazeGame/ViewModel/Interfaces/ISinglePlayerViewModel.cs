﻿using MazeLib;
using MazeGame.Common;

namespace MazeGame.ViewModel.Interfaces
{
    public delegate void GameStatusChangedHandler(object sender, int? code);
    public interface ISinglePlayerViewModel
    {
        event GameStatusChangedHandler OnGameStatusChanged;
        MazeWrapper PlayerMaze { set; get; }
        void PlayerMoved(object sender, string direction);
        int PlayerRow { set; get; }
        int PlayerColumn { set; get; }
        char GetValueAtPos(Position pos);
        void Restart();
        void Solve();
        void Close();
        int Status { get; }
        bool DidWin { get; }        
    }
}