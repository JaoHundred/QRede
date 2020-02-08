using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
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
        public void Connect(string result)
        {
            if (string.IsNullOrWhiteSpace(result))
                return;
            if (!result.ToUpperInvariant().StartsWith("WIFI:", StringComparison.Ordinal))
                return;
            string[] parser = result.Replace("{", "").Replace("}", "").Split(';', ':');
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
                WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
                string info = GetCurrentWifiName();
                if (info != SSID)
                {
                    
                    var wifiConfig = new WifiConfiguration
                    {
                        Ssid = $"\"{SSID}\"" ,
                        PreSharedKey = $"\"{password.Replace("\"","")}\""
                    };
                    var addNetwork = wifiManager.AddNetwork(wifiConfig);
                    var network = wifiManager.ConfiguredNetworks
                         .FirstOrDefault(n => n.Ssid == wifiConfig.Ssid);
                    var enableNetwork = wifiManager.EnableNetwork(network.NetworkId, true);

                    //TODO:avisar ao usuário via o ToasTService se conseguiu se conectar na rede escaneada
                }
            }
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