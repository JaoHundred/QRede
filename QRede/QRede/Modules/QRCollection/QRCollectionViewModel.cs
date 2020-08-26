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
using System.Linq;

namespace QRede.Modules
{
    class QRCollectionViewModel
    {
        public QRCollectionViewModel()
        {
            WifiSummaryCollection = new ObservableRangeCollection<WifiSummary>();
            DeleteCommand = new magno.Command<WifiSummary>(OnDelete);
            ConnectCommand = new magno.AsyncCommand<WifiSummary>(OnConnect);
            SearchCommand = new magno.AsyncCommand(OnSearch);
        }

        public string QRSearch { get; set; }


        public ObservableRangeCollection<WifiSummary> WifiSummaryCollection { get; set; }

        private List<WifiSummary> OriginalWifiSummaryCollection;

        public ICommand DeleteCommand { get; set; }

        private void OnDelete(WifiSummary wifiSummary)
        {
            WifiSummaryCollection.Remove(wifiSummary);
            OriginalWifiSummaryCollection.Remove(wifiSummary);
            App.liteDatabase.GetCollection<WifiSummary>().Delete(wifiSummary.Id);
        }

        public ICommand ConnectCommand { get; set; }
        private async Task OnConnect(WifiSummary wifiSummary)
        {
            await DependencyService.Get<IConnectivityService>().Connect(wifiSummary.FormatedWifiString);
        }

        public ICommand SearchCommand { get; set; }

        private async Task OnSearch()
        {
            var task = Task.Run(() =>
            {
                return OriginalWifiSummaryCollection.Where(wifiSummary => wifiSummary.SSID.ToLower().Contains(QRSearch.ToLower()));
            });
            
            WifiSummaryCollection.ReplaceRange(await task);
        }

        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                OriginalWifiSummaryCollection = App.liteDatabase.GetCollection<WifiSummary>().FindAll().ToList();
                WifiSummaryCollection.ReplaceRange(OriginalWifiSummaryCollection);
            });
        }
    }
}
