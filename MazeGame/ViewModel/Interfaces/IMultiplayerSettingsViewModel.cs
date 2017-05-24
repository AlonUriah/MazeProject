using System.Collections.ObjectModel;

namespace MazeGame.ViewModel.Interfaces
{
    public interface IMultiplayerSettingsViewModel
    {
        int ConnectionStatus { get; }

        string MazeName { set; get; }
        string MazeRowsStr { set; get; }
        string MazeColsStr { set; get; }
        ObservableCollection<string> GamesList { get; }

        int MazeRows { get; }
        int MazeCols { get; }

        void RefreshGamesList();

        IMultiPlayerViewModel JoinGame();
        IMultiPlayerViewModel StartGame();
    }
}
