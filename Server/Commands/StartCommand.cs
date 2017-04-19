using System.Threading;

namespace Server
{
    /*
     * The StartCommand class.   
     * The class handles the 'start' command.
     * It initiate a new multi player game, and waits
     * for another player to join the game.
     */
    public class StartCommand : Command
    {
        /*
         * The StartCommand constructor.
         * Inherited by the Command class.
         */
        public StartCommand(IModel model) : base(model)
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
            // Split the arguments by the space character.
            string[] args = parameters.Split(' ');
            // First argument - the game's name.
            string name = args[0];
            // Second argument - the game's number of rows.
            int rows = int.Parse(args[1]);
            // Third argument - the game's number of cols.
            int cols = int.Parse(args[2]);

            // Add the game to the model, and retrieve it.
            Game game = this.model.AddMultiPlayerGame(client, name, rows, cols);

            /*
             * If the game is not null, that means the creation succeeded,
             * Then wait for the ready property to be true (it will occur 
             * when another player will join the game).
             * Otherwise, send the client an appropriate error, and close the
             * connection.
             */
            if (game != null)
            {
                // Wait for another player to join the game.
                while (!game.Ready)
                    Thread.Sleep(500);

                // Send the game to the creator.
                this.Answer(client, game.ToJSON());
            }
            else
            {
                this.Answer(client, "Error: Illegal name or you're already playing.");
                client.Connection.Close();
            }
        }
    }
}
