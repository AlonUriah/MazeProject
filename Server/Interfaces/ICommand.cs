namespace Server
{
    public interface ICommand
    {
        /*
         * The Execute method.
         * The method executes a specific command,
         * according to its implementation, using
         * relevant parameters, and the sender's details
         * (Client).
         */
        void Execute(Player client, string parameters);
    }
}
