namespace MazeGame.Model.Interfaces
{
    public delegate void GamesListReceivedHandler(string gamesListStr);

    public interface IMultiplayerSettingsModel
    {
        event GamesListReceivedHandler OnGamesListReceived;
        string SelectedGame { get; }
        int GetList();
    }
}
