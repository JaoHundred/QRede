using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers;
using magno = MvvmHelpers.Commands;
using Xamarin.Forms;
using ZXing;
using QRede.Interfaces;


namespace QRede.Modules
{
    public class QRScannerViewModel:BaseViewModel
    {
        public QRScannerViewModel()
        {
            ScanCommand = new magno.Command(OnScan);
        }

        private Result result;

        public Result Result
        {
            get { return result; }
            set { result = value; }
        }


        public ICommand ScanCommand { get; private set; }


        private void OnScan()
        {
                DependencyService.Get<IConnectivityService>().Conect(Result.Text);           
        }


    }
}
