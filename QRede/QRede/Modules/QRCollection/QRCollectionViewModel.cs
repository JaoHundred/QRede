using QRede.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using QRede.Interfaces;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using System.Windows.Input;
using QRede.Services;

namespace QRede.Modules
{
    class QRCollectionViewModel
    {
        public QRCollectionViewModel()
        {
            WifiSummaryCollection = new ObservableRangeCollection<WifiSummary>();
            DeleteCommand = new AsyncCommand<WifiSummary>(OnDelete);
        }

        public ObservableRangeCollection<WifiSummary> WifiSummaryCollection { get; set; }

        public ICommand DeleteCommand { get; set; }

        private async Task OnDelete(WifiSummary wifiSummary)
        {
            WifiSummaryCollection.Remove(wifiSummary);
            App.WifiSummaryCollection.Remove(wifiSummary);
            await IOService<List<WifiSummary>>.WriteAsync(App.WifiSummaryCollection, Constants.QrCodeFilePath);

        }

        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                WifiSummaryCollection.ReplaceRange(App.WifiSummaryCollection);
            });
        }
    }
}
