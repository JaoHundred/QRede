using QRede.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace QRede.Services
{
    public static class ThemeService
    {
        public static Task ChangeThemeAsync(int themeId)
        {
            return Task.Run(async () =>
            {
                var mergedDic = Application.Current.Resources.MergedDictionaries;

                ResourceDictionary oldTheme = mergedDic.First();
                mergedDic.Remove(oldTheme);

                Device.BeginInvokeOnMainThread(() => 
                {
                    switch (themeId)
                    {
                        //carrega tema escuro
                        
                        case 0:
                            mergedDic.Add(new DarkTheme());
                            App.Current.Properties[App.themeKey] = themeId;
                            break;

                        //carrega tema claro
                        default:
                        case 1:
                            mergedDic.Add(new LightTheme());
                            App.Current.Properties[App.themeKey] = themeId;
                            break;
                    }
                });

                await App.Current.SavePropertiesAsync();
            });
        }
    }
}
