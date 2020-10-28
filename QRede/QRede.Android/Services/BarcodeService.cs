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

        public Task<WifiSummary> GetImageAsWifiSummary(string fullFilePath)
        {
            return Task.Run(() =>
            {
                WifiSummary wifiSummary = null;

                try
                {
                    string newPath = $"{fullFilePath.Replace(".png", string.Empty)}.bmp";
                    //var image = Image.FromStream(fileStream);

                    //image.Save(newPath, System.Drawing.Imaging.ImageFormat.Bmp);

                    //var imageBytes = File.ReadAllBytes(fullFilePath);

                    //TODO: a leitura do filemode Open falha para qualquer arquivo fora do escopo do aplicativo, investigar
                    //https://developercommunity.visualstudio.com/content/problem/899718/systemunauthorizedaccessexception-access-to-the-pa-1.html
                    System.Drawing.Bitmap bitmap;
                    using (var bmpStream = new FileStream(fullFilePath, FileMode.Open))
                    {
                        var image = Image.FromStream(bmpStream);
                        bitmap = new System.Drawing.Bitmap(image);
                    }

                    byte[] bytes;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                        bytes = ms.ToArray();
                    }
           
                    ZXing.BarcodeReader reader = new ZXing.BarcodeReader()
                    {
                        Options = new DecodingOptions
                        {
                            TryHarder = true,
                            PureBarcode = true,
                            PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
                        }
                    };

                    ZXing.Result result = reader.Decode(bytes, 500, 500, RGBLuminanceSource.BitmapFormat.RGB32);
                    //You have to declare a delegate which converts your byte array to a luminance source object.

                    if (result != null)
                    {
                        string text = result.Text;
                    }
                }
                catch (Exception ex)
                {


                }


                return wifiSummary;
            });
        }
    }
}