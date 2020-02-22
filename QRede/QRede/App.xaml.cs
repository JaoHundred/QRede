using QRede.Services;
using System;
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

        protected override async void OnStart()
        {
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
