using System;
using System.Collections.Generic;
using System.Linq;
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

[assembly: Xamarin.Forms.Dependency(typeof(WIFIStrenghtService))]
namespace QRede.Droid.Services
{
    public class WIFIStrenghtService : IWIFIStrenghtService
    {
        private WifiManager _wifiManager;

        public int CalculateStrenght()
        {
            if(_wifiManager == null)
                _wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

            WifiInfo wifiInfo = _wifiManager.ConnectionInfo;
            
            return WifiManager.CalculateSignalLevel(wifiInfo.Rssi, 4);
        }
    }
}