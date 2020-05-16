using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Nio;
using QRede.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(QRede.Droid.Services.BarcodeService))]
namespace QRede.Droid.Services
{
    public class BarcodeService : IBarcodeService
    {
        public Task<byte[]> ConvertBarcodeImageToBytes(string formatedWifiSummary)
        {
            return Task.Run(() =>
            {
                var barcodeWriter = new ZXing.Mobile.BarcodeWriter
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 500,
                        Height = 500,
                        Margin = 2
                    }
                };

                barcodeWriter.Renderer = new ZXing.Mobile.BitmapRenderer();
                Bitmap qrBitmap = barcodeWriter.Write(formatedWifiSummary);
                var stream = new MemoryStream();
                qrBitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);  // this is the diff between iOS and Android
                stream.Position = 0;
                return stream.ToArray();
            });
        }
    }
}