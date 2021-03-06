﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGame.View.Data
{
    public interface ICell
    {
        char Fill { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}
