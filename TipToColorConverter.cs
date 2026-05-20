using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace CheltuieliApp
{
    public class TipToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tip = value?.ToString()?.ToLower();
            return tip == "venit" ? Color.FromArgb("#e6ffe6") : Color.FromArgb("#ffe6e6");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
