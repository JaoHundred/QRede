using QRede.Model;
using QRede.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace QRede.Converters
{
    public class DecryptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WifiSummary wifiSummary)
            {
                string result = EncryptionService.DecryptPassword(wifiSummary.EncryptedWifiString, wifiSummary.Key); 
                return result;
            }
            
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
