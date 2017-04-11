using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Controller : IController
    {
        
        private IModel model;
        private Dictionary<string, ICommand> commands;

        public Controller(IModel model)
        {
            this.model = model;
            this.commands = new Dictionary<string, ICommand>();

            // Add the allowed commands to the commands list.
            this.commands.Add("generate", new GenerateCommand(this.model));
            this.commands.Add("start", new StartCommand(this.model));
            this.commands.Add("join", new JoinCommand(this.model));
            this.commands.Add("play", new PlayCommand(this.model));
            this.commands.Add("close", new CloseCommand(this.model));
            this.commands.Add("list", new ListCommand(this.model));

        }
        public void ApplyCommand(Player client, string command)
        {
            // Calculate the index of the first space
            int cmdIndex = command.IndexOf(' ');

            string cmd = null;
            string parameters = null;

            try
            {
                // Till this index, will be the command name
                cmd = command.Substring(0, cmdIndex);

                // After this index, will be the command parameters
                parameters = command.Substring(cmdIndex + 1, command.Length - cmdIndex - 1);
            }
            catch (Exception e)
            {
                // If there's no space, it must be 'close' command.
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
                return;
            }
        }
    }
}
