using MazeLib;
using MazeGame.Common;
namespace MazeGame.ViewModel.Interfaces
{
    public interface IMultiPlayerViewModel
    {
        void PlayerMoved(object sender, string direction);
        MazeWrapper PlayerMaze { set; get; }
        int PlayerRow { set; get; }
        int PlayerColumn { set; get; }
        int OpponentRow { set; get; }
        int OpponentColumn { set; get; }
        char GetValueAtPos(Position pos);
        string[] List { get; }
        void Close();
    }
}
