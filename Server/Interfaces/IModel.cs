using System.Collections.Generic;

namespace Server
{
    /*
     * The IModel interface.
     * The interface handles a model's logic,
     * which can be implemented in many ways.
     */
    public interface IModel
    {
        /*
         * The GetAvailableGames method.
         * The method returns the available games
         * to join, by their name.
         */
        List<string> GetAvailableGames();
        /*
         * The AddSinglePlayerGame method.
         * The method adds a single player game to the model,
         * and returns it.
         */
        Game AddSinglePlayerGame(string name, int rows, int cols);
        /*
         * The AddSinglePlayerGame method.
         * The method adds a multi player game to the model,
         * and returns it.
         */
        Game AddMultiPlayerGame(Player client, string name, int rows, int cols);
        /*
         * The DeleteMultiPlayerGame method.
         * The method deletes a multi player game to the model,
         * and returns it.
         */
        bool DeleteGame(string name);
        /*
         * The JoinMultiPlayerGame method.
         * The method adds the client to the 'name' game,
         * and returns the specific game.
         */
        Game JoinMultiPlayerGame(Player client, string name);
        /*
         * The GetRival method.
         * The method returns the rival of the 'player'
         * client.
         */
        Player GetRival(Player player);
        /*
         * The GetGame method.
         * The method returns the game's name,
         * which 'player' is involved in.
         */
        string GetGame(Player player);
        /*
         * The GetGame method.
         * The method returns the game by its name.
         */
        Game GetGame(string player);
    }
}
