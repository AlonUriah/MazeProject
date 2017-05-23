namespace MazeGame.Model.Interfaces
{
    public delegate void SolutionReceivedHandler(object sender, string solution);
    public delegate void GenerateGameHandler(object sender, string game);

    public interface ISingleplayerModel : IGameModel
    {
        event SolutionReceivedHandler OnSolutionReceived;
        event GenerateGameHandler OnGameReceived;
        void SolveMaze(string mazeName, int algorithm);
        int CreateNewGame(string gameName, int mazeRows, int mazeCols);
    }
}
