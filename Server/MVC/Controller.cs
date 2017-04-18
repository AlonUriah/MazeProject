using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            var regex = new Regex(@"(?'command'[a-z,A-Z]*)(?'parameters'.*)");
            Match match = regex.Match(command);

            // Command is not in the right format of <command> <params>
            if (!match.Success) return;

            string commandName = match.Groups["command"].Value;
            string parameters = match.Groups["parameters"].Value.TrimStart(' ');

            ICommand commandToApply;
            if (!commands.TryGetValue(commandName.ToLower(), out commandToApply))
            {
                //info - could not identify command
                return;
            }

            commandToApply.Execute(client,parameters);
        }
    }
}
