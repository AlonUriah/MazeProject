using System.IO;
using System.Net.Sockets;

namespace Server
{
    /*
     * The Command class.
     * The class handles an abstract command.
     * Each command inherits this class, and gets
     * its methods and model property for free.
     * It should implement the Execute command inside the ICommand
     * interface, that's why it is implemented abstractly here.
     */
    public abstract class Command : ICommand
    {
        // Model property
        protected IModel model;

        /*
         * The Command constructor.
         * The method initiates the model property
         * to be the one in the method arguments.
         */
        public Command(IModel model)
        {
            this.model = model;
        }

        /*
         * The Answer method.
         * The method send a response to the client, according 
         * to the client details, and the actual response.
         * It uses its stream details, and send the response
         * by the stream's WriteLine method.
         */
        protected void Answer(Player client, string response)
        {
            NetworkStream ns = client.Connection.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(response);
            sw.Flush();
        }
        /*
         * The Execute method.
         * The method executes a specific command,
         * according to its implementation, using
         * relevant parameters, and the sender's details
         * (Client).
         */
        public abstract void Execute(Player client, string parameters);
    }
}
