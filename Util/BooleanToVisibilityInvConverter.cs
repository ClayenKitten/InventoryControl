using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InventoryControl.Util
{
    public sealed class BooleanToVisibilityInvConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool)
            {
                if ((bool)value)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Visibility)
            {
                if ((Visibility)value == Visibility.Visible)
                    return false;
                else
                    return true;
            }
            return true;
        }
    }
}
