using System;

namespace Server
{
    /*
     * The CloseCommand class.
     * The class handles the 'close' command.
     * The command closes the game, and terminates
     * the connection of both players.
     * Moreover, it deletes the game data from the
     * Model.
     */
    public class CloseCommand : Command
    {
        /*
         * The CloseCommand constructor.
         * Inherited by the Command class.
         */
        public CloseCommand(IModel model) : base(model)
        {
        }

        /*
         * The Execute method.
         * The method executes a specific command,
         * according to its implementation, using
         * relevant parameters, and the sender's details
         * (Client).
         */
        public override void Execute(Player client, string parameters)
        {
            // Get game name.
            string name = parameters;

            // Get the rival details.
            Player rival = this.model.GetRival(client);

            try
            {
                rival.Connected = false;
                client.Connected = false;
                // Close both players connections, means the game is over.
                client.Connection.Close();
                rival.Connection.Close();

                // Delete the game from the model.
                this.model.DeleteGame(name);
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
