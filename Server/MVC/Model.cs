using System.Collections.Generic;
using MazeGeneratorLib;
using MazeLib;

namespace Server
{
    /*
     * The Model class.
     * The class handles the database
     * of the server, which implements the IModel
     * interface. It has necessary functionality for
     * the game implementation.
     */
    public class Model : IModel
    {
        // List of games, mapped BY their name unique index.
        private Dictionary<string, Game> games;

        // List of players, mapped TO the game which they're involved in.
        private Dictionary<Player[], string> players;

        // A maze generator
        private DFSMazeGenerator mazer;

        // Databases lockers
        private readonly object games_locker = new object();
        private readonly object players_locker = new object();

        /*
         * The Model constructor.
         * The method constructs a new Model object,
         * and sets the data structures of the program.
         */
        public Model()
        {
            // Databases
            this.games = new Dictionary<string, Game>();
            this.players = new Dictionary<Player[], string>();

            // Maze generator
            this.mazer = new DFSMazeGenerator();
        }
        /*
         * The GetAvailableGames method.
         * The method returns the available games
         * to join, by their name.
         */
        public List<string> GetAvailableGames()
        {
            // Instantiate a new list of games.
            List<string> list = new List<string>();

            /*
             * Foreach game in the games' list,
             * check if it is not full - that means
             * it is a good option to join to.
             */
            lock (this.games_locker)
            {
                foreach (Game game in this.games.Values)
                    if (!game.isFull())
                        list.Add(game.Name);
            }
            // Return the list of names
            return list;
        }
        /*
         * The AddSinglePlayerGame method.
         * The method adds a single player game to the model,
         * and returns it.
         */
        public Game AddSinglePlayerGame(string name, int rows, int cols)
        {
            lock (this.games_locker)
            {
                // Validate there's no existing game with 'name'.
                foreach (Game game in this.games.Values)
                    if (game.Name == name)
                        return null;

                // Generate a new maze, and set its name.
                Maze maze = this.mazer.Generate(rows, cols);
                maze.Name = name;

                // Create a game, with the generated maze
                Game result = new SinglePlayerGame(name, maze);

                // Add to the list of games.
                this.games.Add(name, result);

                // Return the game
                return result;
            }
        }
        /*
         * The AddSinglePlayerGame method.
         * The method adds a multi player game to the model,
         * and returns it.
         */
        public Game AddMultiPlayerGame(Player client, string name, int rows, int cols)
        {
            lock (this.games_locker)
            {
                lock (this.players_locker)
                {
                    // Validate there's no existing game with 'name'.
                    foreach (Game game in this.games.Values)
                        if (game.Name == name)
                            return null;

                    // Validate the client which asked for the game, is not already playing.
                    foreach (Player[] clients in this.players.Keys)
                        if ((clients[0] != null && clients[0].Id == client.Id) ||
                            (clients[1] != null && clients[1].Id == client.Id))
                            return null;

                    // [--- Validation passed ---]

                    // Add the client to a pair
                    this.players.Add(new Player[] { client, null }, name);

                    // If validation passed - add the game.
                    Maze maze = this.mazer.Generate(rows, cols);
                    maze.Name = name;

                    // Create a new multiplayer game.
                    Game result = new MultiPlayerGame(name, maze);

                    // Add the game to the list.
                    this.games.Add(name, result);

                    // Return the game
                    return result;
                }
            }
        }
        /*
         * The DeleteMultiPlayerGame method.
         * The method deletes a multi player game to the model,
         * and returns it.
         */
        public bool DeleteGame(string name)
        {
            lock (this.games_locker)
            {
                // Delete the game from the list.
                if (this.games.ContainsKey(name))
                    this.games.Remove(name);
                else
                    return false;

                lock (this.players_locker)
                {
                    /*
                     * Delete the players from the list
                     * of players.
                     */
                    foreach (Player[] clients in this.players.Keys)
                        if (this.players[clients] == name)
                        {
                            this.players.Remove(clients);
                            break;
                        }
                }
            }
            return true;
        }
        /*
         * The JoinMultiPlayerGame method.
         * The method adds the client to the 'name' game,
         * and returns the specific game.
         */
        public Game JoinMultiPlayerGame(Player client, string name)
        {
            lock (this.players_locker)
            {
                lock (this.games_locker)
                {
                    /*
                     * Search for the game the client asked to join to,
                     * and when found, insert the client to the pair,
                     * and change the Ready property of the game to be true,
                     * and return the game.
                     */
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
        /*
         * The GetRival method.
         * The method returns the rival of the 'player'
         * client.
         */
        public Player GetRival(Player player)
        {
            lock (this.players_locker)
            {
                /*
                 * Foreach pair of players, if the current
                 * player is one of the pair, return the other
                 * one.
                 */
                foreach (Player[] pl in this.players.Keys)
                {
                    if (pl[0] != null && pl[0].Id == player.Id)
                        return pl[1];
                    if (pl[1] != null && pl[1].Id == player.Id)
                        return pl[0];
                }
            }

            // If we didn't found a pair, return null.
            return null;
        }
        /*
         * The GetGame method.
         * The method returns the game's name,
         * which 'player' is involved in.
         */
        public string GetGame(Player player)
        {
            lock (this.players_locker)
            {
                /*
                 * Foreach pair, if the player is one of them,
                 * return the name of the game they are playing.
                 */
                foreach (Player[] pl in this.players.Keys)
                    if ((pl[0] != null && pl[0].Id == player.Id) ||
                        (pl[1] != null && pl[1].Id == player.Id))
                        return this.players[pl];
            }
            // If there's no such game - return null.
            return null;
        }
        /*
         * The GetGame method.
         * The method returns the Game by its name.
         */
        public Game GetGame(string name)
        {
            lock (this.games_locker)
            {
                if (this.games.ContainsKey(name))
                    return this.games[name];
            }
            return null;
        }
    }
}
