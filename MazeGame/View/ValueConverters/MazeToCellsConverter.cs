using MazeGame.View.Data;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MazeGame.View.ValueConverters
{
    public class MazeToCellsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<ICell> cells = new ObservableCollection<ICell>();

            Maze maze = (Maze)(value);

            string representation = maze.ToString();
            Console.WriteLine(representation);
            representation = Regex.Replace(representation, @"\t|\n|\r", "");

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
