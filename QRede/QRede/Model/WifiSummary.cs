using QRede.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using MvvmHelpers;
using ZXing;
using Newtonsoft.Json;
using System.IO;

namespace QRede.Model
{
    [JsonObject]
    public class WifiSummary : ObservableObject
    {
        public WifiSummary(BarcodeFormat barcodeFormat)
        {
            BarcodeFormat = barcodeFormat;
        }

        public string SSID { get; set; }

        private string password;

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string wifiState;
        [JsonIgnore]
        public string WifiState
        {
            get { return wifiState; }
            set { SetProperty(ref wifiState, value); }
        }

        private string imagePath;
        [JsonIgnore]
        public string ImagePath
        {
            get { return imagePath; }
            set { SetProperty(ref imagePath, value); }
        }

        private BarcodeFormat barcodeFormat;
        [JsonIgnore]
        public BarcodeFormat BarcodeFormat
        {
            get { return barcodeFormat; }
            set { barcodeFormat = value; }
        }

        private string formatedWifiString;
        public string FormatedWifiString
        {
            get { return formatedWifiString; }
            set { SetProperty(ref formatedWifiString, value); }
        }

        public byte[] QRCodeAsBytes { get; set; }
    }
}
