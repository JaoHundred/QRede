using QRede.Model;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace QRede.Interfaces
{
    public interface IConnectivityService
    {
        string GetCurrentWifiName();

        Task Connect(string result);
    }
}
