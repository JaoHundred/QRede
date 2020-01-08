using QRede.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRede.Interfaces
{
    public interface IConnectivityService
    {
        WifiSummary GetCurrentWifi();
    }
}
