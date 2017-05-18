using MazeLib;
namespace MazeGame.ViewModel.Interfaces
{
    public interface ISinglePlayerViewModel
    {
        // dummy comment for check in
        Maze SingleMaze { get; }
        int PlayerRow { set; get; }
        int PlayerColumn { set; get; }
        void PlayerMoved(object sender, string direction);
        void Solve();
        void Restart();
    }
}