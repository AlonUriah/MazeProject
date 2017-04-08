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
    public class Model
    {
        private List<Game> games;
        private Dictionary<Player[], string> players;

        private readonly object games_locker = new object();
        private readonly object players_locker = new object();

        public Model()
        {
            this.games = new List<Game>();
            this.players = new Dictionary<Player[], string>();
        }
        public List<string> GetAvailableGames()
        {
            List<string> list = new List<string>();

            lock (this.games_locker)
            {
                foreach (Game game in this.games)
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
                foreach (Game game in this.games)
                    if (game.Name == name)
                        return null;

                Game result = new SinglePlayerGame(name, new Maze(rows, cols) { Name = name });
                this.games.Add(result);
                return result;
            }
        }

        public Game AddMultiPlayerGame(Player client, string name, int rows, int cols)
        {
            lock (this.games_locker)
            {
                // Check for existing game
                foreach (Game game in this.games)
                    if (game.Name == name)
                        return null;

                // Check for existing client
                foreach (Player[] clients in this.players.Keys)
                    if (clients[0].Id == client.Id || clients[1].Id == client.Id)
                        return null;

                // Add the client to a pair
                this.players.Add(new Player[] { client, null }, name);

                // If validation passed - add the game.
                Game result = new MultiPlayerGame(name, new Maze(rows, cols) { Name = name });
                this.games.Add(result);

                // Return a json representation of the game.
                return result;
            }
        }

        public Player[] DeleteMultiPlayerGame(string name)
        {
            Player[] players_arr = null;

            lock (this.games_locker)
            {
                foreach (Game game in this.games)
                    if (game.Name == name)
                    { 
                        this.games.Remove(game);

                        lock (this.players_locker)
                        {
                            foreach (Player[] clients in this.players.Keys)
                                if (this.players[clients] == name)
                                {
                                    players_arr = clients;
                                    this.players.Remove(clients);
                                    break;
                                }
                        }
                        break;
                    }
            }

            return players_arr;
        }

        public Game JoinMultiPlayerGame(Player client, string name)
        {
            lock (this.players_locker)
            {
                foreach (Player[] clients in this.players.Keys)
                    if (this.players[clients] == name)
                    {
                        clients[1] = client;

                        lock (this.games_locker)
                        {
                            foreach (Game game in this.games)
                                if (game.Name == name)
                                {
                                    game.Ready = true;
                                    return game;
                                }
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
                        return pl[0];
                    if (pl[1].Id == player.Id)
                        return pl[1];
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
