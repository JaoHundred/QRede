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
using System.Linq;
using QRede.Interfaces;

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

        public static Task<string> GenerateQRStringAsync(WifiSummary wifiSummary)
        {
            return Task.Run(() =>
            {
                string securityType = DependencyService.Get<IConnectivityService>().GetCurrentSecurity();

                if (string.IsNullOrWhiteSpace(securityType))
                    securityType = "nopass";

                return string.Format(FormatedWifiString, wifiSummary.SSID, securityType, wifiSummary.DecryptPassword());
            });
        }
        public static WifiSummary ParseQRCodeString(string text)
        {
            WifiSummary wifiSummary = null;

            if (!string.IsNullOrWhiteSpace(text) &&
               text.ToUpperInvariant().StartsWith("WIFI:", StringComparison.Ordinal))
            {
                //remove(0,5) remove o termo WIFI: da cadeia de string
                string[] parser = text.Remove(0, 5).Split(';');

                //FORMATO DO PARSER
                //T para segurança da rede
                //S para nome da rede
                //P para senha da rede
                //H para dizer se a rede é oculta(opcional)

                string SSID = parser.First(p => p.StartsWith("S:")).Remove(0, 2);
                string password = parser.First(p => p.StartsWith("P:")).Remove(0, 2);

                wifiSummary = new WifiSummary
                {
                    SSID = SSID,
                    FormatedWifiString = text,
                };

                wifiSummary.EncryptPassword(password);

#if DEBUG
                string result =
                    $"SSID: {wifiSummary.SSID} {Environment.NewLine}" +
                    $"EncriptPassword: {wifiSummary.EncryptedPassword} {Environment.NewLine}" +
                    $"Key: {wifiSummary.Key} {Environment.NewLine}" +
                    $"FormatedWifiString: {wifiSummary.FormatedWifiString} {Environment.NewLine}" +
                    $"DecriptPassword: { wifiSummary.DecryptPassword() }";

                Console.WriteLine(result);
#endif

            }

            return wifiSummary;
        }
    }
}
