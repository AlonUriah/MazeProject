namespace MazeGame.ViewModel.Interfaces
{
    public interface ISingleplayerSettingsViewModel
    {
        string MazeName { set; get; }
        int MazeRows { get; }
        int MazeCols { get; }

        ISinglePlayerViewModel StartGame();
    }
}
