using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRede
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        protected async override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "CurrentItem" && Device.RuntimePlatform == Device.Android)
                await Task.Delay(300);
            base.OnPropertyChanged(propertyName);
        }
    }
}
