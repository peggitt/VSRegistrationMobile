using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace HSNP.Convertors
{
    public class FontAwesomeConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var someValue = (string)value; // Convert 'object' to whatever type you are expecting
            // evaluate the converted value
            if (!string.IsNullOrEmpty(someValue))
                return App.Current.Resources[someValue]; 

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Usually unused, but inverse the above logic if needed
            throw new NotImplementedException();
        }
    }
}
