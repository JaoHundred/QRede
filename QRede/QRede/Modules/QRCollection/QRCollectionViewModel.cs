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
    class QRCollectionViewModel
    {
        public QRCollectionViewModel()
        {
            WifiSummaryCollection = new ObservableRangeCollection<WifiSummary>();
        }

        public ObservableRangeCollection<WifiSummary> WifiSummaryCollection { get; set; }

        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                WifiSummaryCollection.ReplaceRange(App.WifiSummaryCollection);
            });
        }
    }
}
