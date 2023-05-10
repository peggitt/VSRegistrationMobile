using HSNP.Models;

namespace HSNP.Mobile.Convertors
{
    public class IntBooleanConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try {
                if (value != null)
                {
                    var someValue = (SystemCodeDetail)value; // Convert 'object' to whatever type you are expecting

                    if (someValue.Description.ToLower().Contains("other"))
                        return true;
                    return false;
                }
                return false;
            }
            catch {
                return false;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Usually unused, but inverse the above logic if needed
            throw new NotImplementedException();
        }
    }
}