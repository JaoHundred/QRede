using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;

namespace QRede.Modules
{
    class QRCodeViewModel:BaseViewModel
    {
        public QRCodeViewModel(string formatedWifiString)
        {
            FormatedWifiString = formatedWifiString;
        }

        private string formatedWifiString;

        public string FormatedWifiString
        {
            get { return formatedWifiString; }
            set { SetProperty(ref formatedWifiString, value); }
        }

    }
}
