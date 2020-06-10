using QRede.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using QRede.Interfaces;
using MvvmHelpers;
using magno = MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using QRede.Services;
using Xamarin.Forms;

namespace QRede.Modules
{
    class QRCollectionViewModel
    {
        public QRCollectionViewModel()
        {
            WifiSummaryCollection = new ObservableRangeCollection<WifiSummary>();
            DeleteCommand = new magno.Command<WifiSummary>(OnDelete);
            ConnectCommand = new magno.AsyncCommand<WifiSummary>(OnConnect);
        }

        public ObservableRangeCollection<WifiSummary> WifiSummaryCollection { get; set; }

        public ICommand DeleteCommand { get; set; }

        private void OnDelete(WifiSummary wifiSummary)
        {
            WifiSummaryCollection.Remove(wifiSummary);
            App.liteDatabase.GetCollection<WifiSummary>().Delete(wifiSummary.Id);
        }

        public ICommand ConnectCommand { get; set; }
        private async Task OnConnect(WifiSummary wifiSummary)
        {
            await DependencyService.Get<IConnectivityService>().Connect(wifiSummary.FormatedWifiString);
        }

        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                WifiSummaryCollection.ReplaceRange(App.liteDatabase.GetCollection<WifiSummary>().FindAll());
            });
        }
    }
}
