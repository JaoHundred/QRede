using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using QRede.Services;

namespace QRede.Modules
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            string themeKey = App.themeKey;
            int themeId = Convert.ToInt32(App.Current.Properties[themeKey]);

            if (themeId == 1)
            {
                IsDarkTheme = true;
                ThemeText = Language.Language.LightTheme;
            }
            else
            {
                IsDarkTheme = false;
                ThemeText = Language.Language.DarkTheme;
            }
        }

        private string themeText;
        public string ThemeText
        {
            get { return themeText; }
            set { SetProperty(ref themeText, value); }
        }

        private bool isDarkTheme;
        public bool IsDarkTheme
        {
            get { return isDarkTheme; }
            set
            {
                //tema escuro
                if (value)
                {
                    ThemeText = Language.Language.LightTheme;
                    ThemeService.ChangeThemeAsync(1);
                }
                
                //tema claro
                else
                {
                    ThemeText = Language.Language.DarkTheme;
                    ThemeService.ChangeThemeAsync(0);
                }

                SetProperty(ref isDarkTheme, value);
            }
        }
    }
}
