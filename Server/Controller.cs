﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Controller : IController
    {
        private Model model;
        private Dictionary<string, ICommand> commands;

        public Controller(Model model)
        {
            this.model = model;
            this.commands = new Dictionary<string, ICommand>();
        }
        public string ApplyCommand(int id, string command)
        {
            // Calculate the index of the first space
            int cmdIndex = command.IndexOf(' ');

            // Till this index, will be the command name
            string cmd = command.Substring(0, cmdIndex);
            // After this index, will be the command parameters
            string parameters = command.Substring(cmdIndex + 1, command.Length - cmdIndex - 1);

            // Execute the correct command
            return this.commands[cmd].Execute(id, parameters);
        }
    }
}
