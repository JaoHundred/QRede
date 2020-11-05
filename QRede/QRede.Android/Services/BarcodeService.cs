using System;
using System.Collections.Generic;
using System.Drawing;
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
using QRede.Model;
using QRede.Services;
using Xamarin.Essentials;
using ZXing;
using ZXing.Common;

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
                Android.Graphics.Bitmap qrBitmap = barcodeWriter.Write(formatedWifiSummary);
                var stream = new MemoryStream();
                qrBitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, stream);  // this is the diff between iOS and Android
                stream.Position = 0;
                return stream.ToArray();
            });
        }

        public Task<WifiSummary> GetImageAsWifiSummary(FileResult fileResult)
        {
            return Task.Run(async () =>
            {
                WifiSummary wifiSummary = null;

                using (Stream fileStream = await fileResult.OpenReadAsync())
                using (var memStream = new MemoryStream())
                {
                    try
                    {
                        await fileStream.CopyToAsync(memStream);

                        byte[] bytes = memStream.ToArray();

                        string cacheDirectory = FileSystem.CacheDirectory;
                        string fileName = fileResult.FileName;
                        string fullPath = System.IO.Path.Combine(cacheDirectory, fileName);

                        await File.WriteAllBytesAsync(fullPath, bytes);

                        var reader = new ZXing.Android.BarcodeReader()
                        {
                            Options = new DecodingOptions()
                            {
                                PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                            },
                        };

                        var bitmap = await BitmapFactory.DecodeFileAsync(fullPath);

                        ZXing.Result result = reader.Decode(bitmap);

                        if (result != null)
                        {
                            wifiSummary = QRCodeService.ParseQRCodeString(result.Text);

                            if (wifiSummary == null)
                                return wifiSummary;

                            wifiSummary.QRCodeAsBytes = bytes;
                        }
                    }
                    catch (Exception)
                    {
                        //TODO: criar sistema de log de erros, ainda a definir
                        throw;
                    }

                }

                return wifiSummary;
            });
        }
    }
}