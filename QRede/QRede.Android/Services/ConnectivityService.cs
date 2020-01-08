using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public WifiSummary GetCurrentWifi()
        {
            var androidWifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
            string wifiName = androidWifiManager.ConnectionInfo.SSID;
            //androidWifiManager.ConnectionInfo.
            

            return new WifiSummary(wifiName);
        }
    }
}