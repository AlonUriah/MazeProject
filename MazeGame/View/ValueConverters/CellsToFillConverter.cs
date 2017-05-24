using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MazeGame.View.ValueConverters
{
    public class CellsToFillConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                char fill = (char)(values[0]);
                int player_row = (int)(values[1]);
                int player_col = (int)(values[2]);

                int cell_row = (int)(values[3]);
                int cell_col = (int)(values[4]);

                bool player_is_here = player_row == cell_row && player_col == cell_col;

                switch (fill)
                {
                    case '1':
                        return new SolidColorBrush(Colors.Black);
                    case '0':
                        if (player_is_here)
                            return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MazeGame;component/View/Resources/player.png")));
                        return new SolidColorBrush(Colors.White);
                    case '*':
                        if (player_is_here)
                            return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MazeGame;component/View/Resources/player.png")));
                        return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MazeGame;component/View/Resources/start.png")));
                    case '#':
                        if (player_is_here)
                            return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MazeGame;component/View/Resources/player.png")));
                        return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MazeGame;component/View/Resources/end.png")));
                    default:
                        throw new Exception("No such symbol!");
                }
            }
            catch (Exception e)
            {
                return null;//new SolidColorBrush(Colors.White);
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
