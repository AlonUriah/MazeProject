namespace MazeGame.Model
{
    public delegate void GamesListReceivedHandler(object sender, string gamesList);

    public interface IMazeModel
    {   
        event GamesListReceivedHandler OnGamesListReceived;
        void GetGamesList();
    }
}
