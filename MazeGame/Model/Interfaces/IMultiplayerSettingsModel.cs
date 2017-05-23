using MazeGame.Common;
namespace MazeGame.Model.Interfaces
{
    public delegate void GameReceivedHandler(MazeWrapper maze);
    public delegate void GamesListReceivedHandler(string gamesListStr);

    public interface IMultiplayerSettingsModel
    {
        event GamesListReceivedHandler OnGamesListReceived;
        event GameReceivedHandler OnGameReceived;

        bool TryGetList();
        void StartGame(string name, int cols, int rows);
        void JoinGame(string gameName);
    }
}
