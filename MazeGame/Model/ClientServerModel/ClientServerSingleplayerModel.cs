﻿using MazeGame.Common;
using MazeGame.Model.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace MazeGame.Model.ClientServerModel
{
    internal class ClientServerSingleplayerModel : ClientServerModelBase, ISingleplayerModel
    {
        #region Constants
        private const char LEFT_SYMBOL = '0';
        private const char RIGHT_SYMBOL = '1';
        private const char UP_SYMBOL = '2';
        private const char DOWN_SYMBOL = '3';

        private const string SOLVE_COMMAND = @"solve {0} {1}";
        private const string CREATE_NEW_GAME_COMMAND = @"generate {0} {1} {2}";
        #endregion

        public event GenerateGameHandler OnGameReceived;
        public event SolutionReceivedHandler OnSolutionReceived;

        protected MazeWrapper PlayerMaze { set; get; }
        protected int PlayerRow { set; get; }
        protected int PlayerColumn { set; get; }

        public ClientServerSingleplayerModel() : base ()
        {

        }
        public ClientServerSingleplayerModel(Client client) : base (client)
        {
        }

        public ClientServerSingleplayerModel(Client client, string gameName, int mazeRows, int mazeCols) : base(client)
        {
            OnGameReceived += UpdateMaze;
            CreateNewGame(gameName, mazeRows, mazeCols);
        }

        public ClientServerSingleplayerModel(string gameName, int mazeRows, int mazeCols) : base()
        {
            OnGameReceived += UpdateMaze;
            CreateNewGame(gameName, mazeRows, mazeCols);
        }

        public void CreateNewGame(string gameName, int mazeRows, int mazeCols)
        {
            IsLoading = true;
            var query = string.Format(CREATE_NEW_GAME_COMMAND, gameName, mazeRows, mazeCols);
            _client.Broadcast(query);
        }
        protected virtual void UpdateMaze(object sender, string gameJason)
        {
            try
            {
                var gameObj = JObject.Parse(gameJason);
                PlayerMaze = gameObj.ToMazeWrapper();
                PlayerRow = PlayerMaze.StartRow;
                PlayerColumn = PlayerMaze.StartCol;

                if (OnGameReceived != null)
                {
                    OnGameReceived.Invoke(this, gameJason);
                }

                IsLoading = false;
            }
            catch (JsonReaderException e)
            {
                //Alert
                IsLoading = true;
            }
        }

        public virtual void Move(string direction)
        {
            if (!IsValidMove(direction))
            {
                return;
            }

            switch (direction.ToLower())
            {
                case "up":
                    PlayerRow++;
                    break;
                case "down":
                    PlayerRow--;
                    break;
                case "left":
                    PlayerColumn--;
                    break;
                case "right":
                    PlayerColumn++;
                    break;
                default:
                    break;
            }
        }

        protected bool IsValidMove(string direction)
        {
            throw new NotImplementedException();
        }

        public void SolveMaze(string mazeName, string algorithm)
        {
            if (!algorithm.EqualsLower("BFS") && !algorithm.EqualsLower("DFS"))
            {
                //Warn
                return;
            }

            IsLoading = true;
            var query = string.Format(SOLVE_COMMAND, mazeName, (algorithm.EqualsLower("BFS")) ? 0 : 1);
            _client.Broadcast(query);
        }

        private void SimulateSolution(string solution)
        {
            PlayerRow = PlayerMaze.StartRow;
            PlayerColumn = PlayerMaze.StartCol;

            foreach (char direction in solution)
            {
                switch (direction)
                {
                    case LEFT_SYMBOL:
                        Move("left");
                        break;
                    case RIGHT_SYMBOL:
                        Move("Right");
                        break;
                    case UP_SYMBOL:
                        Move("Up");
                        break;
                    case DOWN_SYMBOL:
                        Move("Down");
                        break;
                    default:
                        return;
                }
                Thread.Sleep(1000);
            }
        }

        public override void ResponseReceived(string response)
        {
            if(response == "generated game")
            {
                //game jason
                UpdateMaze(this, response);
            }
            else if (response.Contains("Solution"))
            {
                //solution jason
                SimulateSolution(response);
            }
        }
    }
}
