using QRede.Model;
using QRede.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRede
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public readonly static string themeKey = "AppTheme";

        public static List<WifiSummary> WifiSummaryCollection;

        protected override async void OnStart()
        {
            WifiSummaryCollection = await IOService<WifiSummary>.ReadAsync(Constants.QrCodeFilePath);
            if (App.Current.Properties.ContainsKey(themeKey))
            {
                int themeId = Convert.ToInt32(App.Current.Properties[themeKey]);

                await ThemeService.ChangeThemeAsync(themeId);
            }
            else
                App.Current.Properties.Add(themeKey, 0);

            await App.Current.SavePropertiesAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
