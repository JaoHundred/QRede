using QRede.Model;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace QRede.Interfaces
{
    public interface IConnectivityService
    {
        string GetCurrentWifiName();

        void Connect(string result);
    }
}
