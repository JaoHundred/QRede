﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRede.Modules
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScannerView : ContentPage
    {
        public QRScannerView()
        {
            InitializeComponent();
            BindingContext = new QRScannerViewModel();
        }

        private void OnPageAppearing(object sender, EventArgs e)
        {
            //TODO: verificar linha abaixo pra saber se o delay ajuda ao QRCode funcionar(fazer mais alguns testes)
            Task.Run(async () => { await Task.Delay(TimeSpan.FromSeconds(4)); });
        }
    }
}