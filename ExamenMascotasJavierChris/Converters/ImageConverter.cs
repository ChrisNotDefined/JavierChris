using ExamenMascotasJavierChris.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ExamenMascotasJavierChris.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return "unnamed.png";
            }
            //return value;
            try
            {
                return new ImageService().ConvertImageFromBase64ToImageSource(value.ToString());
            }
            catch (Exception)
            {
                return "unnamed.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
