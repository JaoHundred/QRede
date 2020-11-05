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
    public static class QRCodeService
    {
        /// <summary>
        /// parâmetros 
        /// {0} = SSID (nome da rede)
        /// {1} = WPA || WEP (security)
        /// {2} = senha
        /// </summary>
        public static readonly string FormatedWifiString = "WIFI:S:{0};T:{1};P:{2};;";

        public static Task GenerateQRStringAsync(WifiSummary wifiSummary)
        {
            return Task.Run(() =>
            {
                wifiSummary.FormatedWifiString = string.Format(FormatedWifiString, wifiSummary.SSID, "WPA", wifiSummary.Password);
            });
        }

        public static WifiSummary ParseQRCodeString(string text)
        {
            WifiSummary wifiSummary = null;

            if (!string.IsNullOrWhiteSpace(text) &&
               text.ToUpperInvariant().StartsWith("WIFI:", StringComparison.Ordinal))
            {
                string[] parser = text.Replace("{", "").Replace("}", "").Split(';', ':');
                //FORMATO DO PARSER
                //[0]WIFI
                //[1]S
                //[2]SSID
                //[3]T
                //[4]WPA
                //[5]P
                //[6]SENHA
                //[7]""
                //[8]""
                if (parser?.Length == 9 && parser[1] == "S" && parser[3] == "T" && parser[5] == "P")
                {
                    string SSID = parser[2];
                    string password = parser[6];

                    wifiSummary = new WifiSummary
                    {
                        SSID = SSID,
                        Password = password,
                        FormatedWifiString = text,
                    };
                }

            }

            return wifiSummary;
        }
    }
}
