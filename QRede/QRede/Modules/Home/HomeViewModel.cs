﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Plugin.Connectivity;
using QRede.Interfaces;
using QRede.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace QRede.Modules
{
    public class HomeViewModel : BaseViewModel, IAsyncInitialization
    {
        public HomeViewModel()
        {
            //ToDo Descomentar quando o método estiver implementado
            //LoadTask =LoadAsync();
            GenerateQRCodeCommand = new Command(OnGenerateQRCode);
        }

        public Task LoadTask { get; }
        public Task LoadAsync()
        {
            //ToDo Implementar checador periodico de rede  
            throw new NotImplementedException();
        }

        #region Property's
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }
        #endregion

        public ICommand GenerateQRCodeCommand
        { get; private set; }

        private void OnGenerateQRCode()
        {
            //ToDo fazer a ligação com o serviço de geração de código QR

            var connectivityAndroid = DependencyService.Get<IConnectivityService>();
            WifiSummary wifiSummary = connectivityAndroid.GetCurrentWifiName();

            if (wifiSummary.WifiState == WifiState.Enabled)
            {
                Name = wifiSummary.SSID;
                Status = "Conectado";
            }
            else
            {
                Name = "Nome não encontrado";
                Status = "Desconectado";
            }
        }
    }
}