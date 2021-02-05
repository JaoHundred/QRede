using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using QRede.Model;

namespace QRede.Converters
{
    public class ParseWifiStringWithParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WifiSummary wifiSummary && parameter is WifiParam wifiParam)
                return wifiSummary.ParseWifiString(wifiParam);

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
