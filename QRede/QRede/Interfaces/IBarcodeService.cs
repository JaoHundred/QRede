﻿using QRede.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace QRede.Interfaces
{
    public interface IBarcodeService
    {
        Task<byte[]> ConvertBarcodeImageToBytes(string formatedWifiSummary);
        Task<WifiSummary> GetImageAsWifiSummary(FileResult fullFilePath);
    }
}
