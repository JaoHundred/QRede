using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QRede.Interfaces
{
    public interface IBarcodeService
    {
        Task<byte[]> ConvertBarcodeImageToBytes(string formatedWifiSummary);
    }
}
