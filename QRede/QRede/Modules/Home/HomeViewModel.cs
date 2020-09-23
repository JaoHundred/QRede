using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Plugin.Connectivity;
using QRede.Interfaces;
using QRede.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using QRede.Services;
using magno = MvvmHelpers.Commands;
using Rg.Plugins;
using QRede.Modules;
using Rg.Plugins.Popup.Pages;
using FluentValidation;
using QRede.Model.Validator;
using FluentValidation.Results;
using System.Linq;

namespace QRede.Modules
{
    public class HomeViewModel : BaseViewModel, IAsyncInitialization
    {
        //TODO:https://github.com/JaoHundred/QRede/issues/18

        public HomeViewModel()
        {
            LoadTask = LoadAsync();
            GenerateQRCodeCommand = new magno.AsyncCommand(OnGenerateQRCode);
        }

        public Task LoadTask { get; }
        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                var connectivityAndroid = DependencyService.Get<IConnectivityService>();
                var wifiStrenghtService = DependencyService.Get<IWIFIStrenghtService>();

                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    SSID = connectivityAndroid.GetCurrentWifiName();

                    int strenght = wifiStrenghtService.CalculateStrenght();

                    switch (strenght)
                    {
                        case int str when strenght == 0:
                            WifiState = Language.Language.WiFiStateOff;
                            ImagePath = "WiFiDisconected.png";
                            break;

                        case int str when strenght == 1:
                            WifiState = Language.Language.WiFiStateOn;
                            ImagePath = "WiFiLow.png";
                            break;

                        case int str when strenght == 2:
                            WifiState = Language.Language.WiFiStateOn;
                            ImagePath = "WiFiMedium.png";
                            break;

                        case int str when strenght == 3:
                            WifiState = Language.Language.WiFiStateOn;
                            ImagePath = "WiFiFull.png";
                            break;
                    }

                    return true;
                });
            });
        }

        #region Property's

        private string sSID;
        public string SSID
        {
            get { return sSID; }
            set { SetProperty(ref sSID, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _wifiState;
        public string WifiState
        {
            get { return _wifiState; }
            set { SetProperty(ref _wifiState, value); }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }
        #endregion

        public ICommand GenerateQRCodeCommand { get; private set; }
        private async Task OnGenerateQRCode()
        {
            WifiSummary wifiSummary = new WifiSummary(ZXing.BarcodeFormat.QR_CODE)
            {
                SSID = SSID,
                Password = Password,
            };

            await QRCodeService.GenerateQRStringAsync(wifiSummary);

            await NavigationService.NavigateAsync<QRCodeViewModel>(wifiSummary, this);
        }
    }
}
