using QRede.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace QRede.Model
{
    public class WifiSummary
    {
        public WifiSummary(string wifiName)
        {
            if (string.IsNullOrEmpty(wifiName))
                WifiState = WifiState.Disabled;
            else
            {
                SSID = wifiName;
                WifiState = WifiState.Enabled;
            }
        }

        public string SSID { get; set; }
        public WifiState WifiState { get; set; }
    }
}
