using QRede.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using Xamarin.Forms;
using System.IO;
using Color = System.Drawing.Color;
using System.Drawing.Imaging;

namespace QRede.Services
{
    public static class QRCodeGenerateService
    {
        //Todo a biblioteca Qrcoder está com diferenças de versão de outras bibliotecas do xamarin forms, pesquisar por outra solução para gerar qrcode
        public static Task<ImageSource> GenerateAsync(WifiSummary wifiSummary)
        {
            return Task.Run(() =>
            {
                Bitmap qrCodeImage = default;
                try
                {
                    PayloadGenerator.WiFi wifiPayload = new PayloadGenerator.WiFi(wifiSummary.SSID, wifiSummary.Password, PayloadGenerator.WiFi.Authentication.WPA);
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(wifiPayload.ToString(), QRCodeGenerator.ECCLevel.Q);
                    QRCode qRCode = new QRCode(qrCodeData);
                    qrCodeImage = qRCode.GetGraphic(20);
                }
                catch (Exception ex)
                {

                    
                }
                

                return BitMapToImageSource(qrCodeImage);
            });
        }

        public static ImageSource BitMapToImageSource(Bitmap bmp)
        {
            Stream imageStream = new MemoryStream();
            int rgb;
            Color c;

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    c = bmp.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            bmp.Save(imageStream, ImageFormat.Jpeg);

            ImageSource igmSrc = ImageSource.FromStream(() =>
             {
                 return imageStream;
             });

            return igmSrc;
        }
    }
}
