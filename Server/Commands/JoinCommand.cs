namespace Server
{
    /*
     * The JoinCommand class.   
     * The class handles the 'join' command.
     * The command joins a player to a specific game,
     * and retrieves it to the player.
     */
    public class JoinCommand : Command
    {       
        /*
         * The JoinCommand constructor.
         * Inherited by the Command class.
         */
        public JoinCommand(IModel model) : base(model)
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
            // The join commands gets only 1 parameter - game's name.
            string name = parameters;

            // Get the game from the model, and join it.
            Game game = this.model.JoinMultiPlayerGame(client, name);

            /*
             * If the game is not null, that means the 'join' action
             * succeeded, and we can retrieve the serialized game
             * to the player.
             * Otherwise, we send an error, and close the connection.
             */
            if (game != null)
                this.Answer(client, game.ToJSON());
            else
            {
                this.Answer(client, "Error: Illegal game or you're already playing.");
                client.Connection.Close();
            }
        }
    }
}
