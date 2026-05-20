using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CheltuieliApp
{
    public class TipToTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tip = value?.ToString()?.ToLower();
            return tip == "venit" ? Colors.Green : Colors.Red;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
