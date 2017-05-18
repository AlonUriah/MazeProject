using MazeLib;
namespace MazeGame.ViewModel.Interfaces
{
    public interface IMultiplayerViewModel
    {
        //dummy comment for check in
        void PlayerMoved(object sender, string direction);
        Maze MultiplayerMaze { set; get; }
        int PlayerRow { set; get; }
        int PlayerColumn { set; get; }
        int OpponentRow { set; get; }
        int OpponentColumn { set; get; }
    }
}
