using MvvmHelpers.Commands;
using QRede.Model;
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
    class LicenseViewModel
    {
        public LicenseViewModel()
        {

            Licenses = new List<LicenseTemplate>
            {
                new LicenseTemplate{LibName="FluentValidation", Author="Jeremy Skinner", Link= LinkLicenseConstants.FluentValidation },
                new LicenseTemplate{LibName="LiteDB", Author="Maurício David", Link= LinkLicenseConstants.LiteDB},
                new LicenseTemplate{LibName="NETStandard.Library", Author="Microsoft", Link= LinkLicenseConstants.NetStandardLibrary },
                new LicenseTemplate{LibName="Refractored.MvvmHelpers", Author="James Montemagno", Link= LinkLicenseConstants.RefactoredMvvmHelpers },
                new LicenseTemplate{LibName="Rg.Plugins.Popup", Author="Kirill Lyubimov", Link= LinkLicenseConstants.RgPluginsPopup },
                new LicenseTemplate{LibName="System.Drawing.Common", Author="Microsoft", Link= LinkLicenseConstants.SystemDrawingCommon },
                new LicenseTemplate{LibName="Xam.Plugin.Connectivity", Author="James Montemagno", Link= LinkLicenseConstants.XamPluginConnectivity },
                new LicenseTemplate{LibName="Xamarin.Essentials", Author="Microsoft", Link= LinkLicenseConstants.XamarimEssentials },
                new LicenseTemplate{LibName="Xamarin.FFImageLoading.Forms", Author="Daniel Luberda, Fabien Molinet", Link= LinkLicenseConstants.XamarimFFImageLoadingForms },
                new LicenseTemplate{LibName="Xamarin.Forms", Author="Microsoft", Link= LinkLicenseConstants.XamarimForms },
                new LicenseTemplate{LibName="ZXing.Net.Mobile", Author="Redth", Link= LinkLicenseConstants.ZxingNetMobile },
                new LicenseTemplate{LibName="ZXing.Net.Mobile.Forms", Author="Redth", Link= LinkLicenseConstants.ZxingNetMobileForms },
            };

            OpenWebViewCommand = new AsyncCommand<string>(OpenWebView);
        }

        public IList<LicenseTemplate> Licenses { get; set; }


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
