using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GuessMate.Helpers
{
    internal class LetterColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string color)
                return Brushes.White;

            return color switch
            {
                "Gray" => new SolidColorBrush(Color.FromRgb(120, 124, 126)),
                "Yellow" => new SolidColorBrush(Color.FromRgb(200, 179, 96)),
                "Green" => new SolidColorBrush(Color.FromRgb(109, 169, 104)),
                _ => Brushes.White
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

