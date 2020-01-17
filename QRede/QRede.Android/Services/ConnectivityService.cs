using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRede.Droid.Services;
using QRede.Interfaces;
using QRede.Model;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(QRede.Droid.Services.ConnectivityService))]
namespace QRede.Droid.Services
{
    public class ConnectivityService : IConnectivityService
    {
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

        private string FormatSSid(string ssid)
        {
            char[] charToRemove = { (char)92, (char)34 }; // 92 = \   34 = "
            var builder = new StringBuilder();

            for (int i = 0; i < ssid.Length; i++)
            {
                char ch = ssid[i];

                if(!charToRemove.Contains(ch))
                    builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}