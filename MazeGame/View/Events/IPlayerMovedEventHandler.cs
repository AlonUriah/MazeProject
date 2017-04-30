namespace MazeGame.View.Events
{
    interface IPlayerMovedEventHandler
    {
        void OnPlayerMoved(object sender, PlayerMovedEventArgs e);
    }
}
