using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QRede.Interfaces
{
    public interface IAsyncInitialization
    {
        Task LoadTask { get;}

        Task LoadAsync();

    }

}
