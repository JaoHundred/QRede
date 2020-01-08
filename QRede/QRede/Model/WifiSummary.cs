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
            SSID = wifiName;
        }

        public string SSID { get; set; }
    }
}
