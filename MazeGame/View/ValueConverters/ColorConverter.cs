using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MazeGame.View.ValueConverters
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char color = (char)(value);
            switch (color)
            {
                case '1':
                    return "Black";
                case '0':
                    return "White";
                case '*':
                    return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MazeGame;component/View/Resources/start.png")));
                case '#':
                    return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/MazeGame;component/View/Resources/end.png")));
                default:
                    throw new Exception("No such symbol!");
            }             
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
