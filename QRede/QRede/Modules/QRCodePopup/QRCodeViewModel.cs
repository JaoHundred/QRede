using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FFImageLoading;
using MvvmHelpers;
using QRede.Interfaces;
using QRede.Model;
using QRede.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QRede.Modules
{
    class QRCodeViewModel : BaseViewModel
    {
        public QRCodeViewModel(WifiSummary formatedWifiString, BaseViewModel contextViewModel)
        {
            CurrentWifiSummary = formatedWifiString;

            if (contextViewModel is HomeViewModel)
            {
                IsHomeViewModel = true;
                SaveCommand = new MvvmHelpers.Commands.AsyncCommand(OnSave);
            }
            else
            {
                IsHomeViewModel = false;
                ShareCommand = new MvvmHelpers.Commands.AsyncCommand(OnShare);
            }
        }

        private WifiSummary currentWifiSummary;

        public WifiSummary CurrentWifiSummary
        {
            get { return currentWifiSummary; }
            set { SetProperty(ref currentWifiSummary, value); }
        }

        private bool _isHomeViewModel;
        public bool IsHomeViewModel
        {
            get { return _isHomeViewModel; }
            set { SetProperty(ref _isHomeViewModel, value); }
        }

        public ICommand SaveCommand { get; set; }

        public async Task OnSave()
        {
            Task<byte[]> qrCodeTask = DependencyService.Get<IBarcodeService>().ConvertBarcodeImageToBytes(CurrentWifiSummary.FormatedWifiString);

            CurrentWifiSummary.QRCodeAsBytes = await qrCodeTask;

            App.liteDatabase.GetCollection<WifiSummary>().Insert(CurrentWifiSummary);
            DependencyService.Get<IToastService>().ToastLongMessage(Language.Language.SavedWifi);

            await NavigationService.PopModalAsync();
        }

        public ICommand ShareCommand { get; set; }

        public async Task OnShare()
        {
            if (IsNotBusy)
            {
                IsBusy = true;

                string fullPath = Path.Combine(FileSystem.CacheDirectory, $"{CurrentWifiSummary.SSID}.png");
                File.WriteAllBytes(fullPath, CurrentWifiSummary.QRCodeAsBytes);

                //TODO: testar o compartilhamento em um dispositivo real quando o VS estiver funcionando novamente para gerar o APK

                await Share.RequestAsync(new ShareFileRequest($"{Language.Language.Sharing} {CurrentWifiSummary.SSID}", new ShareFile(fullPath)));

                IsBusy = false;
            }
        }

    }
}
