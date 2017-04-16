using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /*
     * The Controller class.
     * The class handles a part of the MVC architectural pattern,
     * as it controls the behavior of the server.
     * It has the commands list, and has an execution logic,
     * of command types.
     */
    public class Controller : IController
    {
        // Model property
        private IModel model;
        // Commands list
        private Dictionary<string, ICommand> commands;

        /*
         * The Controller constructor.
         * The method sets the model as a property,
         * and initiate a new commands list.
         * Then, it adds all the allowed commands to the 
         * list by their names as keys.
         */
        public Controller(IModel model)
        {
            // Sets the model as a property
            this.model = model;
            // Initiate the commands list
            this.commands = new Dictionary<string, ICommand>();

            // Add the allowed commands to the commands list.
            this.commands.Add("generate", new GenerateCommand(this.model));
            this.commands.Add("start", new StartCommand(this.model));
            this.commands.Add("join", new JoinCommand(this.model));
            this.commands.Add("play", new PlayCommand(this.model));
            this.commands.Add("close", new CloseCommand(this.model));
            this.commands.Add("list", new ListCommand(this.model));
            this.commands.Add("solve", new SolveCommand(this.model));
        }
        /*
         * The ApplyCommand method.
         * The method gets a user input from the client,
         * and activate the correct command. If there's no
         * such command, it exits the method, and does nothing.
         */
        public void ApplyCommand(Player client, string command)
        {
            // Calculate the index of the first space
            int cmdIndex = command.IndexOf(' ');

            // Declare two parts of the commands - name and arguments.
            string cmd = null;
            string parameters = null;

            try
            {
                // Till this index, will be the command name.
                cmd = command.Substring(0, cmdIndex);

                // After this index, will be the command parameters
                parameters = command.Substring(cmdIndex + 1, command.Length - cmdIndex - 1);
            }
            catch (Exception e)
            {
                // If there's no space, it must be 'close' or 'list' commands.
                cmd = command;
                parameters = "";
            }

            try
            {
                // Execute the correct command
                this.commands[cmd].Execute(client, parameters);
            }
            catch (Exception e)
            {
                /*
                 * After we've done the parsing job, 
                 * No command can be executed unless it is
                 * in the correct format.
                 * Otherwise, just return - means do nothing.
                 */
                return;
            }
        }
    }
}
