using MvvmHelpers.Commands;
using QRede.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace QRede.Modules.Settings
{
    class AboutViewModel
    {
        public AboutViewModel()
        {
            OpenWebViewCommand = new AsyncCommand<string>(OpenWebView);
        }

        public ICommand OpenWebViewCommand { get; private set; }

        private async Task OpenWebView(string url)
        {
            ResourceDictionary CurrentTheme = ThemeService.GetCurrentTheme();
            await Xamarin.Essentials.Browser.OpenAsync(url, new BrowserLaunchOptions()
            {
                PreferredToolbarColor = (Color)CurrentTheme["NavigationBarColor"],
                PreferredControlColor = (Color)CurrentTheme["TextColor"],
                LaunchMode = BrowserLaunchMode.SystemPreferred
            });
        }
    }
}
