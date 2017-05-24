using MazeGame.Model.Interfaces;

namespace MazeGame.Model.ClientServerModel
{
    public class MultiplayerSettingModel : ClientServerModelBase, IMultiplayerSettingsModel
    {
        private const string LIST_COMMAND = "list";

        public MultiplayerSettingModel(Client client) : base(client) { }

        public event GamesListReceivedHandler OnGamesListReceived;

        public int GetList()
        {
            return _client.Broadcast(LIST_COMMAND);
        }
        
        public override void ResponseReceived(string response)
        {
            if(OnGamesListReceived != null)
            {
                OnGamesListReceived.Invoke(response);
            }       
        }
    }
}
