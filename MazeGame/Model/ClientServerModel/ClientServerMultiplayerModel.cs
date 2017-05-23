using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MazeGame.Model.Interfaces;
using System;

namespace MazeGame.Model.ClientServerModel
{
    internal class ClientServerMultiplayerModel : ClientServerSingleplayerModel, IMultiplayerModel
    {
        #region Constants
        private const string PLAY_COMMAND = @"play {0}";
        const int PLAYER = 1;
        const int RIVAL = 0;

        public event OpponentMovedHandler OnOpponentMoved;
        public event JoinGameHandler OnJoinningGame;
        #endregion

        public int RivalRow { set; get; }
        public int RivalColumn { set; get; }

        public ClientServerMultiplayerModel(Client client, string gameName, int mazeRows, int mazeCols) : base(client) { }
        public ClientServerMultiplayerModel(string gameName, int mazeRows, int mazeCols) : base() { }

        public override void Move(string direction)
        {
            base.Move(direction);

            var query = string.Format(PLAY_COMMAND, direction);
            _client.Broadcast(query);

            if (DidWin(PlayerRow, PlayerColumn, 1))
            {
           
            }
        }

        public override void ResponseReceived(string response)
        {
            JObject responseJason;
            try
            {
                responseJason = JObject.Parse(response);
            }
            catch (JsonReaderException)
            {
                // Alert
                return;
            }

            if (responseJason.Property("Direction") != null)
            {
                JToken token = responseJason.Property("Direction").Value;
                UpdateRivalLocation(token.Value<string>());
                return;
            }

            UpdateMaze(this, response);
        }

        protected override void UpdateMaze(object sender, string gameJason)
        {
            base.UpdateMaze(sender, gameJason);
            RivalRow = PlayerMaze.StartRow;
            RivalColumn = PlayerMaze.StartCol;
            IsLoading = false;
        }

        private void UpdateRivalLocation(string direction)
        {
            switch (direction.ToLower())
            {
                case "up":
                    RivalRow++;
                    break;
                case "down":
                    RivalRow--;
                    break;
                case "left":
                    RivalColumn--;
                    break;
                case "right":
                    RivalColumn++;
                    break;
                default:
                    break;
            }

            if (DidWin(RivalRow, RivalRow, 2))
            {
              
            }
        }

        public void JoinGame(string gameName)
        {
            if(OnJoinningGame != null)
            {
                OnJoinningGame.Invoke(this, gameName);
            }
        }
    }
}
