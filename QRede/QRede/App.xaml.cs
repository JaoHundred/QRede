using QRede.Model;
using QRede.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LiteDB;

namespace QRede
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "Shapes_Experimental" });

            InitializeComponent();

            MainPage = new AppShell();


            Xamarin.Essentials.VersionTracking.Track();
        }

        public readonly static string themeKey = "AppTheme";

        public static LiteDatabase liteDatabase; 

        protected override async void OnStart()
        {
            if (App.Current.Properties.ContainsKey(themeKey))
            {
                int themeId = Convert.ToInt32(App.Current.Properties[themeKey]);

                await ThemeService.ChangeThemeAsync(themeId);
            }
            else
            //1 é tema claro, garante que a aplicação em estado limpo(instalado pela primeira vez)
            //não vai trocar de tema sozinho quando abrir pela segunda vez
                App.Current.Properties.Add(themeKey, 1);

            BsonMapper bsonMapper = BsonMapper.Global;
            bsonMapper.Entity<WifiSummary>().Id(x => x.Id)
                .Ignore(x=>x.WifiState)
                .Ignore(x=>x.ImagePath)
                .Ignore(x=>x.BarcodeFormat);
            liteDatabase = new LiteDatabase($"Filename={Constants.QrCodeFilePath}", bsonMapper);
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
