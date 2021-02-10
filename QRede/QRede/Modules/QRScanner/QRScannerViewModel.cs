using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers;
using magno = MvvmHelpers.Commands;
using Xamarin.Forms;
using ZXing;
using QRede.Interfaces;
using System.Threading.Tasks;
using QRede.Services;
using QRede.Model;

namespace QRede.Modules
{
    public class QRScannerViewModel : BaseViewModel
    {
        public QRScannerViewModel()
        {
            IsScanning = true;
            ScanCommand = new magno.AsyncCommand(OnScan);
        }

        private Result result;
        public Result Result
        {
            get { return result; }
            set { result = value; }
        }

        private bool isScanning;

        public bool IsScanning
        {
            get { return isScanning; }
            set { SetProperty(ref isScanning, value); }
        }


        public ICommand ScanCommand { get; private set; }
        private async Task OnScan()
        {
            IsScanning = false;
            if (NavigationService.CanPopupNavigate<GenericPopupViewModel>())
            {
                string message = string.Format(Language.Language.Connecting, WifiSummary.ParseWifiString(WifiParam.S, Result.Text));
                await NavigationService.NavigateAsync<GenericPopupViewModel>(Language.Language.Warning, message, Language.Language.Conect, Language.Language.Cancel, new Action(async () =>
             {

                 await DependencyService.Get<IConnectivityService>().Connect(Result.Text);

             }),
                new Action(() =>
                {
                    IsScanning = true;
                }));

            }
        }
    }
}
