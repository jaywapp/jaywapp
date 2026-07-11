using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Jaywapp.Infrastructure.Helpers
{
    public static class ColorHelper
    {
        public static string GetColorName(this Color color)
        {
            Type colors = typeof(Colors);
            foreach (var prop in colors.GetProperties())
            {
                if (((Color)prop.GetValue(null, null)) == color)
                    return prop.Name;
            }

            return color.ToString();
        }

        public static Color ToColor(this string str, Color? defaultColor = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                    return (Color)ColorConverter.ConvertFromString(str);
            }
            catch { }

            return defaultColor ?? Colors.Black;
        }

        public static bool TryConvertColor(this string str, out Color color)
        {
            color = default;

            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    color = (Color)ColorConverter.ConvertFromString(str);
                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
