using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using System.Net.Sockets;

namespace Server
{
    public class Model : IModel
    {
        private Dictionary<string, Game> games;
        private Dictionary<Player[], string> players;
        private DFSMazeGenerator mazer;

        private readonly object games_locker = new object();
        private readonly object players_locker = new object();

        public Model()
        {
            this.games = new Dictionary<string, Game>();
            this.players = new Dictionary<Player[], string>();
            this.mazer = new DFSMazeGenerator();
        }
        public List<string> GetAvailableGames()
        {
            List<string> list = new List<string>();

            lock (this.games_locker)
            {
                foreach (Game game in this.games.Values)
                    if (!game.isFull())
                        list.Add(game.Name);
            }

            return list;
        }

        public Game AddSinglePlayerGame(string name, int rows, int cols)
        {
            lock (this.games_locker)
            {
                // Check for existing game
                foreach (Game game in this.games.Values)
                    if (game.Name == name)
                        return null;

                Maze maze = this.mazer.Generate(rows, cols);
                maze.Name = name;

                Game result = new SinglePlayerGame(name, maze);
                this.games.Add(name, result);
                return result;
            }
        }

        public Game AddMultiPlayerGame(Player client, string name, int rows, int cols)
        {
            lock (this.games_locker)
            {
                lock (this.players_locker)
                {
                    // Check for existing game
                    foreach (Game game in this.games.Values)
                        if (game.Name == name)
                            return null;

                    // Check for existing client
                    foreach (Player[] clients in this.players.Keys)
                        if ((clients[0] != null && clients[0].Id == client.Id) ||
                            (clients[1] != null && clients[1].Id == client.Id))
                            return null;

                    // Add the client to a pair
                    this.players.Add(new Player[] { client, null }, name);

                    // If validation passed - add the game.
                    Maze maze = this.mazer.Generate(rows, cols);
                    maze.Name = name;

                    Game result = new MultiPlayerGame(name, maze);
                    this.games.Add(name, result);

                    // Return a json representation of the game.
                    return result;
                }
            }
        }

        public void DeleteMultiPlayerGame(string name)
        {
            lock (this.games_locker)
            {
                this.games.Remove(name);

                lock (this.players_locker)
                {
                    foreach (Player[] clients in this.players.Keys)
                        if (this.players[clients] == name)
                        {
                            this.players.Remove(clients);
                            break;
                        }
                }
            }
        }

        public Game JoinMultiPlayerGame(Player client, string name)
        {
            lock (this.players_locker)
            {
                lock (this.games_locker)
                {
                    foreach (Player[] clients in this.players.Keys)
                        if (this.players[clients] == name && clients[1] == null)
                        {
                            clients[1] = client;
                            this.games[name].Ready = true;
                            return this.games[name];
                        }
                }
            }

            return null;
        }

        public Player GetRival(Player player)
        {
            lock (this.players_locker)
            {
                foreach (Player[] pl in this.players.Keys)
                {
                    if (pl[0].Id == player.Id)
                        return pl[1];
                    if (pl[1].Id == player.Id)
                        return pl[0];
                }
            }
            return null;
        }

        public string GetGame(Player player)
        {
            lock (this.players_locker)
            {
                foreach (Player[] pl in this.players.Keys)
                    if (pl[0].Id == player.Id || pl[1].Id == player.Id)
                        return this.players[pl];
            }
            return null;
        }
    }
}
