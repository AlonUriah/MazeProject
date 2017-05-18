using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MazeGame.View.ValueConverters
{
    public class WidthToPixelsConverter : IValueConverter
    {
        private const int cell_width = 20;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)(cell_width * (int)(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
