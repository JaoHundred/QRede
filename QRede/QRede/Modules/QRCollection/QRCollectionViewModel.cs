using QRede.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using QRede.Interfaces;
using MvvmHelpers;
using System.Threading.Tasks;

namespace QRede.Modules
{
    class QRCollectionViewModel : IAsyncInitialization
    {
        public QRCollectionViewModel()
        {
            WifiSummaryCollection = new ObservableRangeCollection<WifiSummary>();
            LoadTask = LoadAsync();
        }

        public ObservableRangeCollection<WifiSummary> WifiSummaryCollection { get; set; }

        public Task LoadTask { get; }

        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                WifiSummaryCollection.AddRange(App.WifiSummaryCollection);
            });
        }
    }
}
