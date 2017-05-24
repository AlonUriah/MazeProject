namespace MazeGame.ViewModel.Interfaces
{
    public interface IMultiplayerSettingsViewModel
    {
        int ConnectionStatus { get; }

        string MazeName { set; get; }
        string MazeRowsStr { set; get; }
        string MazeColsStr { set; get; }
        string[] GamesList { set; get; }

        int MazeRows { get; }
        int MazeCols { get; }

        IMultiPlayerViewModel JoinGame();
        IMultiPlayerViewModel StartGame();
    }
}
