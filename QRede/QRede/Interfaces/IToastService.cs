using System;
using System.Collections.Generic;
using System.Text;

namespace QRede.Interfaces
{
    public interface IToastService
    {
        void ToastLongMessage(string message, int fontSize = 14);
    }
}
