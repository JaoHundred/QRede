using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using QRede.Interfaces;
using QRede.Model;
using QRede.Services;
using Xamarin.Forms;

namespace QRede.Modules
{
    class QRCodeViewModel:BaseViewModel
    {
        public QRCodeViewModel(WifiSummary formatedWifiString)
        {
            CurrentWifiSummary = formatedWifiString;
            SaveCommand = new MvvmHelpers.Commands.AsyncCommand(OnSave);
        }

        private WifiSummary currentWifiSummary;

        public WifiSummary CurrentWifiSummary
        {
            get { return currentWifiSummary; }
            set { SetProperty(ref currentWifiSummary, value); }
        }

        public ICommand SaveCommand { get; set; }

        public async Task OnSave()
        {
            App.WifiSummaryCollection.Add(CurrentWifiSummary);
            await IOService<List<WifiSummary>>.WriteAsync(App.WifiSummaryCollection,Constants.QrCodeFilePath);
            DependencyService.Get<IToastService>().ToastLongMessage("Código QR Salvo com sucesso");
            await NavigationService.PopModalAsync();
        }
    }
}
