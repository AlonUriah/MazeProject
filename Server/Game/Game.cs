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
    /*
     * The Game class.
     * The class handles an abstract game, which
     * can be either single/multi player game.
     */
    public abstract class Game
    {
        // A locker for the ready property.
        protected readonly object ready_locker = new object();

        // The ready member.
        protected bool ready;

        // The game's name property
        public string Name { get; private set; }
        // The game's maze property
        public Maze Maze { get; private set; }
        // The game's ready property
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

        /*
         * The Game constructor.
         * The method sets the name and maze
         * properties according to the method arguments.
         */
        public Game(string name, Maze maze)
        {
            Name = name;
            Maze = maze;
        }

        /*
         * The isFull method.
         * The method returns if the game is full
         * or available to join.
         */
        public bool isFull()
        {
            /*
             * Done by the ready property.
             * The logic is - if the game is ready to begin,
             * then it is full. Otherwise - it won't have been
             * ready to be played.
             */
            return this.ready;
        }
        /*
         * The ToJSON method.
         * The method converts a Game instance
         * to a serialized json string.
         */
        public string ToJSON()
        {
            // Serialize 'start' position.
            JObject start = new JObject();
            start["Row"] = Maze.InitialPos.Row;
            start["Col"] = Maze.InitialPos.Col;

            // Serialize 'end' position.
            JObject end = new JObject();
            end["Row"] = Maze.GoalPos.Row;
            end["Col"] = Maze.GoalPos.Col;

            // Serialize the whole object.
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
