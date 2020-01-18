using QRede.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Xamarin.Forms;
using System.IO;
using Color = System.Drawing.Color;
using System.Drawing.Imaging;

namespace QRede.Services
{
    public static class QRCodeGenerateService
    {
        /// <summary>
        /// parâmetros 
        /// {0} = SSID (nome da rede)
        /// {1} = WPA || WEP (security)
        /// {2} = senha
        /// </summary>
        public static readonly string FormatedWifiString = "WIFI:S:{0};T:{1};P:{2};;";

        public static Task<string> GenerateAsync(WifiSummary wifiSummary)
        {
            return Task.Run(() =>
            {
                wifiSummary.FormatedWifiString = string.Format(FormatedWifiString, wifiSummary.SSID, "WPA", wifiSummary.Password);
                return wifiSummary.FormatedWifiString;
            });
        }
    }
}
