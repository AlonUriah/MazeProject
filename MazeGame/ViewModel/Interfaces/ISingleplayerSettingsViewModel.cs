namespace MazeGame.ViewModel.Interfaces
{
    public interface ISinglePlayerSettingsViewModel
    {
        string MazeName { set; get; }
        int MazeRows { get; }
        int MazeCols { get; }

        ISinglePlayerViewModel StartGame();
    }
}
