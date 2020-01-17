using QRede.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using MvvmHelpers;

namespace QRede.Model
{
    public class WifiSummary : ObservableObject
    {
        public WifiSummary()
        {
        }

        public string SSID { get; set; }

        private string password;

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string wifiState;
        public string WifiState
        {
            get { return wifiState; }
            set { SetProperty(ref wifiState, value); }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { SetProperty(ref imagePath, value); }
        }
    }
}
