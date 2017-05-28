using MazeGame.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MazeGame.Model.Interfaces;
using MazeGame.Model.ClientServerModel;
using System;

namespace MazeGame.Model.ClientServerModel
{
    internal class ClientServerMultiplayerModel : ClientServerModelBase, IMultiplayerModel
    {
        #region Constants
        private const string PLAY_COMMAND = @"play {0}";
        private const string JOIN_COMMAND = @"join {0}";
        private const string START_COMMAND = @"start {0} {1} {2}";
        const int PLAYER = 1;
        const int RIVAL = 0;

        public event OpponentMovedHandler OnOpponentMoved;
        public event JoinGameHandler OnJoinningGame;
        public event PlayerMovedHandler OnPlayerMoved;
        #endregion

        public event GenerateGameHandler OnGameReceived;

        public MazeWrapper PlayerMaze;
        public int RivalRow { set; get; }
        public int RivalColumn { set; get; }

        public ClientServerMultiplayerModel(Client client) : base(client) { }

        public void Move(string direction)
        {
            //base.Move(direction);

            var query = string.Format(PLAY_COMMAND, direction);
            _client.Broadcast(query);

            //if (DidWin(PlayerRow, PlayerColumn, 1))
            //{
           
            //}
        }

        public override void ResponseReceived(string response)
        {
            response = response.TrimJasonEnd();
            JObject responseJason;

            try
            {
                responseJason = JObject.Parse(response);
            }
            catch (JsonReaderException e)
            {
                return;
            }

            if (responseJason.Property("Direction") != null)
            {
                JToken token = responseJason.Property("Direction").Value;
                UpdateRivalLocation(token.Value<string>());
                return;
            }

            OnGameReceived?.Invoke(this, response);
        }

        public virtual void GameReceived(object sender, string gameJason)
        {
            JObject game;
            try
            {
                game = JObject.Parse(gameJason);
            }
            catch (JsonReaderException)
            {
                return;
            }

            PlayerMaze = game.ToMazeWrapper();
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

            //if (DidWin(RivalRow, RivalRow, 2))
            //{
              
            //}
        }

        public void JoinGame(string gameName)
        {
            IsLoading = true;
            var query = string.Format(JOIN_COMMAND, gameName);
            _client.Broadcast(query);
        }
        public void StartGame(string gameName, int rows, int cols)
        {
            var query = string.Format(START_COMMAND, gameName, rows, cols);
            _client.Broadcast(query);
        }
    }
}
