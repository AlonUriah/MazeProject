using MazeGame.Common;
using MazeGame.View.Data;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace MazeGame.View.ValueConverters
{
    public class MazeToCellsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            MazeWrapper maze = (MazeWrapper)(value);

            if (maze == null)
                return null;


            ObservableCollection<ICell> cells = new ObservableCollection<ICell>();

            string representation = maze.MazeStr;
            
            for (int i = 0; i < maze.Rows; i++)
                for (int j = 0; j < maze.Cols; j++)
                    cells.Add(new Cell(j, i, representation[i * maze.Cols + j]));

            return cells;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
