using MazeGame.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Text.RegularExpressions;

namespace MazeGame.ViewModel
{
    public class VM : ISinglePlayerViewModel
    {
        private MazeGeneratorLib.DFSMazeGenerator mazer;
        private Maze maze;
        private int player_row;
        private int player_col;
        private string rep;

        public VM()
        {
            this.mazer = new MazeGeneratorLib.DFSMazeGenerator();
            this.maze = this.mazer.Generate(20, 35);
            this.maze.Name = "Uriah";
            this.rep = this.maze.ToString();
            this.rep = Regex.Replace(this.rep, @"\t|\n|\r", "");


            // Default player position

            this.player_row = this.maze.InitialPos.Row;
            this.player_col = this.maze.InitialPos.Col;
        }

        public Maze SingleMaze
        {
            get
            {
                return this.maze;
            }
        }
        public int PlayerRow
        {
            get
            {
                return this.player_row;
            }
            set
            {
                this.player_row = value;
            }
        }
        public int PlayerColumn
        {
            get
            {
                return this.player_col;
            }
            set
            {
                this.player_col = value;
            }
        }

        public Maze PlayerMaze => throw new NotImplementedException();

        public char GetValueAtPos(Position pos)
        {
            return this.rep[pos.Row * this.maze.Cols + pos.Col];
        }

        public void PlayerMoved(object sender, string direction)
        {
            throw new NotImplementedException();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void Solve()
        {
            throw new NotImplementedException();
        }
    }
}
