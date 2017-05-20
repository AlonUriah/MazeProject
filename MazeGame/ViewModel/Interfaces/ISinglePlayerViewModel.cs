using MazeLib;
namespace MazeGame.ViewModel.Interfaces
{
    public interface ISinglePlayerViewModel
    {
        // dummy comment for check in
        Maze PlayerMaze { get; }
        void PlayerMoved(object sender, string direction);
        int PlayerRow { set; get; }
        int PlayerColumn { set; get; }
        char GetValueAtPos(Position pos);
        void Solve();

    }
}