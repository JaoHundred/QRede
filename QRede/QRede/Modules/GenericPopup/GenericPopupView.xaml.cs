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
    public partial class GenericPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public GenericPopupView()
        {
            InitializeComponent();            
        }
    }
}