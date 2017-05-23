namespace MazeGame.Model.Interfaces
{
    public delegate void OpponentMovedHandler(object sender, string move);
    public delegate void JoinGameHandler(object sender, string game);

    public interface IMultiplayerModel : IGameModel
    {
        event OpponentMovedHandler OnOpponentMoved;
        event JoinGameHandler OnJoinningGame;
    }
}
