using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.View.Data
{
    public class Cell : ICell
    {
        private char fill;
        private int x;
        private int y;
        private int height = 20;
        private int width = 20;

        public Cell(int x, int y, char fill)
        {
            this.fill = fill;
            this.x = x;
            this.y = y;

        }

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public char Fill
        {
            get
            {
                return this.fill;
            }
            set
            {
                this.fill = value;
            }
        }

    }
}
