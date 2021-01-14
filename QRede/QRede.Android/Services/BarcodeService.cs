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
                        string cacheDirectory = FileSystem.CacheDirectory;
                        string fileName = fileResult.FileName;
                        string fullPath = System.IO.Path.Combine(cacheDirectory, fileName);

                        await fileStream.CopyToAsync(memStream);

                        byte[] bytes = memStream.ToArray();
                        await File.WriteAllBytesAsync(fullPath, bytes);


                        ZXing.Result result = await ReadBarcodeAsync(fullPath);

                        if (result != null)
                        {
                            wifiSummary = QRCodeService.ParseQRCodeString(result.Text);

                            if (wifiSummary == null)
                                return wifiSummary;

                            wifiSummary.QRCodeAsBytes = bytes;
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO: criar sistema de log de erros, ainda a definir
                        throw;
                    }

                }

                return wifiSummary;
            });
        }

        public async static Task<ZXing.Result> ReadBarcodeAsync(string filePath)
        {
            var reader = new ZXing.BarcodeReaderGeneric
            {
                Options = new DecodingOptions
                {
                    PureBarcode = true,
                    PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE },
                },
            };

            Android.Graphics.Bitmap bmap = await BitmapFactory.DecodeFileAsync(filePath);
            byte[] bytes = await GetRgbBytesAsync(bmap);

            var result = reader.Decode(new RGBLuminanceSource(bytes, bmap.Width, bmap.Height));
            return result;
        }

        private static Task<byte[]> GetRgbBytesAsync(Android.Graphics.Bitmap bitmap)
        {
            return Task.Run(() =>
            {
                var rgbList = new List<byte>();
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Android.Graphics.Color pixelColor = new Android.Graphics.Color(bitmap.GetPixel(x, y));

                        rgbList.AddRange(new[] { pixelColor.R, pixelColor.G, pixelColor.B });
                    }
                }
                return rgbList.ToArray();
            });
        }
    }
}