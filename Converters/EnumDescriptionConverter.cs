using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Jaywapp.Infrastructure.Helpers;
using System.Windows.Data;

namespace Jaywapp.Wpf.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        private Type _currentType;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum target)
            {
                _currentType = target.GetType();
                return target.GetDescriptionOrToString();
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string target
                && _currentType != null
                && EnumHelper.TryParseValueFromDescription(target, _currentType, out var result))
            {
                return result;
            }

            return Binding.DoNothing;
        }
    }
}