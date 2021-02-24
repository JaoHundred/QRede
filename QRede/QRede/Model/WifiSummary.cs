using QRede.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using MvvmHelpers;
using ZXing;
using System.IO;
using System.Linq;
using QRede.Services;

namespace QRede.Model
{
    public class WifiSummary : ObservableObject
    {
        public WifiSummary(BarcodeFormat barcodeFormat)
        {
            BarcodeFormat = barcodeFormat;
        }

        public WifiSummary()
        {

        }

        public int Id { get; set; }

        /// <summary>
        /// chave usada para descriptografar
        /// </summary>
        public string Key { get; set; }

        private string wifiState;
        public string WifiState
        {
            get { return wifiState; }
            set { SetProperty(ref wifiState, value); }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { SetProperty(ref imagePath, value); }
        }

        private BarcodeFormat barcodeFormat;
        public BarcodeFormat BarcodeFormat
        {
            get { return barcodeFormat; }
            set { barcodeFormat = value; }
        }

        private string _encryptedWifiString;
        public string EncryptedWifiString
        {
            get { return _encryptedWifiString; }
            set { SetProperty(ref _encryptedWifiString, value); }
        }

        public byte[] QRCodeAsBytes { get; set; }

        /// <summary>
        /// faz um parse e pega a informação desejada de EncryptedWifiString 
        /// </summary>
        /// <param name="wifiParam"></param>
        /// <returns></returns>
        public string ParseWifiString(WifiParam wifiParam)
        {

#if DEBUG
            string consoleResult =
                $"Key: {Key} {Environment.NewLine}" +
                $"FormatedWifiString: {EncryptionService.DecryptPassword(EncryptedWifiString, Key)} {Environment.NewLine}";

            Console.WriteLine(consoleResult);
#endif
            string wifiString = EncryptionService.DecryptPassword(EncryptedWifiString, Key);

            return ParseWifiString(wifiParam, wifiString);

        }

        /// <summary>
        /// faz um parse diretamente em uma dada wifiString
        /// </summary>
        /// <param name="wifiParam">especifica qual informação vai extrair da wifiString</param>
        /// <param name="wifiString">string que representa a conexão wifi via QR code</param>
        /// <returns></returns>
        public static string ParseWifiString(WifiParam wifiParam, string wifiString)
        {
            if (!string.IsNullOrWhiteSpace(wifiString) &&
              wifiString.ToUpperInvariant().StartsWith("WIFI:", StringComparison.Ordinal))
            {

                //FORMATO DO PARSER
                //T para segurança da rede
                //S para nome da rede
                //P para senha da rede
                //H para dizer se a rede é oculta(opcional)

                //remove(0,5) remove o termo WIFI: da cadeia de string
                string[] parser = wifiString.Remove(0, 5).Split(';');

                switch (wifiParam)
                {
                    case WifiParam.P:
                        return parser.First(p => p.StartsWith("P:")).Remove(0, 2);

                    case WifiParam.S:
                        return parser.First(p => p.StartsWith("S:")).Remove(0, 2);

                    default:
                        break;
                }
            }

            return string.Empty;
        }

        public static bool IsWifiQRCode(string Result)
        {
            if(Result.StartsWith("WIFI:"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    /// <summary>
    /// enumera os parâmetros encontrados em uma string wifi de QR code
    /// </summary>
    public enum WifiParam
    {
        /// <summary>
        /// parametro de senha
        /// </summary>
        P,
        /// <summary>
        /// parametro de nome da rede(SSID)
        /// </summary>
        S,
    };
}
