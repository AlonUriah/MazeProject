using MazeLib;
using System.ComponentModel;
using System.Windows.Input;

namespace MazeGame.ViewModel.Interfaces
{
    public interface ISinglePlayerViewModel : INotifyPropertyChanged
    {
        // dummy comment for check in
        Maze PlayerMaze { get; }
        void PlayerMoved(object sender, string direction);
        int PlayerRow { set; get; }
        int PlayerColumn { set; get; }
        char GetValueAtPos(Position pos);
        void Solve();
        void create(object sender, MouseButtonEventArgs e);
    }
}