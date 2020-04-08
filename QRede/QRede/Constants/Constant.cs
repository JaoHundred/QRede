using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QRede
{
    public static class Constants
    {
        public static string QrCodeFilePath = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"AplicationData");
    }
}
