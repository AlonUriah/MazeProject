namespace MazeGame.Model.Interfaces
{
    public delegate void PlayerMovedHandler(object sender, string move);
    public delegate void OpponentMovedHandler(object sender, string move);
    public delegate void JoinGameHandler(object sender, string game);

    public interface IMultiplayerModel : IGameModel
    {
        event PlayerMovedHandler OnPlayerMoved;
        event OpponentMovedHandler OnOpponentMoved;
        event GenerateGameHandler OnGameReceived;

        void StartGame(string gameName, int rows, int cols);
        void JoinGame(string gameName);
    }
}
