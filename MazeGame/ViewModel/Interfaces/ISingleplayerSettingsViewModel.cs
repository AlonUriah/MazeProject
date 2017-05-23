namespace MazeGame.ViewModel.Interfaces
{
    public interface ISinglePlayerSettingsViewModel
    {
        string MazeName { set; get; }
        string MazeRowsStr { set; get; }
        string MazeColsStr { set; get; }
        int MazeRows { get; }
        int MazeCols { get; }

        ISinglePlayerViewModel StartGame();
    }
}
