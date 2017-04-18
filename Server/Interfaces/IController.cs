namespace Server
{
    /*
     * The IController interface.
     * The interface handles a controller logic,
     * which can be implemented in many ways.
     */
    public interface IController
    {
        /*
         * The ApplyCommand method.
         * The method gets a user input from the client,
         * and activate the correct command. If there's no
         * such command, it exits the method, and does nothing.
         */
        void ApplyCommand(Player client, string command);
    }
}
