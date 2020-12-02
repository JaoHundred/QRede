using QRede.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRede.Modules.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LicenseView : ContentPage
    {
        public LicenseView()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender is Grid grid)
            {
                var color = ThemeService.GetCurrentTheme()["ItemSelectedColor"];
                grid.BackgroundColor = (Color)color;

                await Task.Delay(TimeSpan.FromSeconds(1));

                grid.BackgroundColor = Color.Transparent;
            }
        }
    }
}