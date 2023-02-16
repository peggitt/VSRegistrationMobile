﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace HSNP.Convertors
{
    public class BooleanConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var someValue = (bool)value; // Convert 'object' to whatever type you are expecting

                if (someValue)
                    return false;
                return true;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Usually unused, but inverse the above logic if needed
            throw new NotImplementedException();
        }
    }
}