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

        public static Task<string> GenerateQRStringAsync(string ssid, string passsword)
        {
            return Task.Run(() =>
            {
                string securityType = DependencyService.Get<IConnectivityService>().GetCurrentSecurity();

                if (string.IsNullOrWhiteSpace(securityType))
                    securityType = "nopass";

                return string.Format(FormatedWifiString, ssid, securityType, passsword);
            });
        }
       
    }
}
