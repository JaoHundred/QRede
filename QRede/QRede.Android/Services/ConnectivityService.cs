using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRede.Droid.Services;
using QRede.Interfaces;
using QRede.Language;
using QRede.Model;
using QRede.Services;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(QRede.Droid.Services.ConnectivityService))]
namespace QRede.Droid.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public async Task Connect(string result)
        {
            IToastService toastService = DependencyService.Get<IToastService>();

            if (!result.StartsWith("WIFI"))
            {
                toastService.ToastLongMessage(Language.Language.Invalid);
                return;
            }

            string ssid = WifiSummary.ParseWifiString(WifiParam.S, result);
            string password = WifiSummary.ParseWifiString(WifiParam.P, result);

            WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
            string info = GetCurrentWifiName();
            bool canConnect = CanConnect(wifiManager, ssid);
            if (info != ssid)
            {
                var wifiConfig = new WifiConfiguration
                {
                    Ssid = $"\"{ssid}\"",
                    PreSharedKey = $"\"{password.Replace("\"", "")}\""
                };
                var addNetwork = wifiManager.AddNetwork(wifiConfig);
                var network = wifiManager.ConfiguredNetworks
                     .FirstOrDefault(n => n.Ssid == wifiConfig.Ssid);
                var enableNetwork = wifiManager.EnableNetwork(network.NetworkId, true);
                int counter = 0;
                while (info != ssid && counter < 5)
                {
                    info = GetCurrentWifiName();
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    counter++;
                }
                info = GetCurrentWifiName();
                if (info == ssid)
                {
                    toastService.ToastLongMessage(Language.Language.Sucess);
                }
                else
                {
                    toastService.ToastLongMessage(Language.Language.Fail);
                }
            }
            else if (!canConnect)
            {
                toastService.ToastLongMessage(Language.Language.WifiUnreacheable);
            }
            else
            {
                toastService.ToastLongMessage(Language.Language.Alredy);
            }

        }

        private bool CanConnect(WifiManager wifiManager, string Target)
        {
            return wifiManager.ScanResults.Any(scanResult => scanResult.Ssid == Target);
        }


        public string GetCurrentWifiName()
        {
            string ssid = "";
            WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
            WifiInfo info = wifiManager.ConnectionInfo;
            int networkId = info.NetworkId;

            IList<WifiConfiguration> netConfList = wifiManager.ConfiguredNetworks;

            foreach (WifiConfiguration wificonf in netConfList)
            {
                if (wificonf.NetworkId == networkId)
                {
                    ssid = wificonf.Ssid;
                    break;
                }

            }

            return FormatSSid(ssid);// "\"formatoPadrão\""
        }

        public string GetCurrentSecurity()
        {
            string security = "";
            WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
            WifiInfo info = wifiManager.ConnectionInfo;
            int networkId = info.NetworkId;

            IList<WifiConfiguration> netConfList = wifiManager.ConfiguredNetworks;

            foreach (WifiConfiguration wificonf in netConfList)
            {
                if (wificonf.NetworkId == networkId)
                {
                    //TODO: testar essa parte com redes reais e ver se o código abaixo
                    //está funcionando corretamente

                    if (wificonf.AllowedKeyManagement.Get((int)KeyManagementType.WpaPsk))
                        security = "WPA";
                    else
                        security = wificonf.WepKeys.FirstOrDefault() != null ? "WEP" : "";

                    break;
                }

            }

            return security;
        }

        private string FormatSSid(string ssid)
        {
            char[] charToRemove = { (char)92, (char)34 }; // 92 = \   34 = "
            var builder = new StringBuilder();

            for (int i = 0; i < ssid.Length; i++)
            {
                char ch = ssid[i];

                if (!charToRemove.Contains(ch))
                    builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}