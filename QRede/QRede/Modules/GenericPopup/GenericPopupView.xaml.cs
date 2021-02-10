using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Contracts;

namespace QRede.Modules
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenericPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public GenericPopupView()
        {
            InitializeComponent(); 
            
        }

        protected override void OnDisappearing()
        {
            var ViewModel = (GenericPopupViewModel)this.BindingContext;
            ViewModel.RefuseAction?.Invoke();
            base.OnDisappearing();
        }


    }
}