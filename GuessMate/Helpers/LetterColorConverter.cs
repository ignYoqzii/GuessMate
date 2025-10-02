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
                "Gray" => Brushes.LightGray,
                "Yellow" => Brushes.Gold,
                "Green" => Brushes.LightGreen,
                _ => Brushes.White
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

