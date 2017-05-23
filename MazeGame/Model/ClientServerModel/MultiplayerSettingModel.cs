using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MazeGame.Common;
using MazeGame.Model.Interfaces;
using System;

namespace MazeGame.Model.ClientServerModel
{
    public class MultiplayerSettingModel : ClientServerModelBase, IMultiplayerSettingsModel
    {
        #region Commands
        private const string JOIN_COMMAND = @"join {0}";
        private const string START_COMMAND = @"start {0} {1} {2}";
        private const string LIST_COMMAND = "list";
        #endregion

        public event GameReceivedHandler OnGameReceived;
        public event Interfaces.GamesListReceivedHandler OnGamesListReceived;

        public MultiplayerSettingModel(Client client) : base(client) { }

        public bool TryGetList()
        {
            return (_client.Broadcast(LIST_COMMAND) == 1);
        }
        public void JoinGame(string gameName)
        {
            var query = string.Format(JOIN_COMMAND, gameName);
            _client.Broadcast(query);
            IsLoading = true;
        }
        public void StartGame(string name, int cols, int rows)
        {
            var query = string.Format(START_COMMAND, name, cols, rows);
            _client.Broadcast(query);
            IsLoading = true;
        }

        public override void ResponseReceived(string response)
        {
            JObject jObject;
            try
            {
                jObject = JObject.Parse(response);
            }
            catch (JsonReaderException e)
            {
                // Warn
                return;
            }

            if(OnGameReceived != null)
            {
                OnGameReceived.Invoke(jObject.ToMazeWrapper());
            }
        }
    }
}
