using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Plugin.Connectivity;
using QRede.Interfaces;
using QRede.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using QRede.Services;
using magno = MvvmHelpers.Commands;

namespace QRede.Modules
{
    public class HomeViewModel : BaseViewModel, IAsyncInitialization
    {
        public HomeViewModel()
        {
            //ToDo Descomentar quando o método estiver implementado
            WifiSummary = new WifiSummary(ZXing.BarcodeFormat.QR_CODE);
            LoadTask = LoadAsync();
            GenerateQRCodeCommand = new magno.AsyncCommand(OnGenerateQRCode);
        }

        public Task LoadTask { get; }
        public async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                Device.StartTimer(TimeSpan.FromSeconds(2), () =>
                {
                    var connectivityAndroid = DependencyService.Get<IConnectivityService>();
                    WifiSummary.SSID = SSID = connectivityAndroid.GetCurrentWifiName();

                    if (string.IsNullOrEmpty(SSID))
                    {
                        WifiSummary.WifiState = "Desconectado";
                        WifiSummary.ImagePath = "WiFiDisconected.png";
                    }
                    else
                    {
                        WifiSummary.WifiState = "Conectado";
                        WifiSummary.ImagePath = "WiFiFull.png";
                    }

                    return true;
                });
            });
        }

        #region Property's
        private WifiSummary wifiSummary;
        public WifiSummary WifiSummary
        {
            get { return wifiSummary; }
            set { wifiSummary = value; }
        }

        private string sSID;
        public string SSID
        {
            get { return sSID; }
            set { SetProperty(ref sSID, value); }
        }

        private string barcodeValue;
        public string BarcodeValue
        {
            get { return barcodeValue; }
            set { SetProperty(ref barcodeValue, value); }
        }

        #endregion

        public ICommand GenerateQRCodeCommand { get; private set; }
        private async Task OnGenerateQRCode()
        {
            string formatedWifiString = await QRCodeGenerateService.GenerateAsync(WifiSummary);


        //TODO: abrir tela modal passando como parâmetro a string formatada da rede
        //mover controles zxing e hack para a janela modal
        //chamar aqui o serviço de abrir a janela modal
        //https://docs.microsoft.com/pt-br/dotnet/api/system.activator.createinstance?view=netframework-4.8
        //https://docs.microsoft.com/pt-br/dotnet/api/system.type.getconstructor?view=netframework-4.8
        }
    }
}
