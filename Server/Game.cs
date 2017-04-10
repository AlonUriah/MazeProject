using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
    public abstract class Game
    {
        protected readonly object ready_locker = new object();

        private bool ready;

        public string Name { get; private set; }
        public Maze Maze { get; private set; }
        public bool Ready {
            get
            {
                lock (this.ready_locker)
                {
                    return this.ready;
                }
            }
            set
            {
                lock (this.ready_locker)
                {
                    this.ready = value;
                }
            }
        } 

        public Game(string name, Maze maze)
        {
            Name = name;
            Maze = maze;
        }

        public bool isFull()
        {
            return !Ready;
        }

        public string ToJSON()
        {
            JObject start = new JObject();
            start["Row"] = Maze.InitialPos.Row;
            start["Col"] = Maze.InitialPos.Col;

            JObject end = new JObject();
            end["Row"] = Maze.GoalPos.Row;
            end["Col"] = Maze.GoalPos.Col;

            JObject game = new JObject();
            game["Name"] = Name;
            game["Maze"] = Maze.ToString();
            game["Rows"] = Maze.Rows;
            game["Cols"] = Maze.Cols;
            game["Start"] = start;
            game["End"] = end;

            return game.ToString();
        }
    }
}
