﻿using QRede.Model;
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
        string GetCurrentSecurity();

        Task Connect(string result);
    }
}
