using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRede.Modules
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScannerView : ContentPage
    {
        public QRScannerView()
        {
            InitializeComponent();
            BindingContext = new QRScannerViewModel();
        }
    }
}