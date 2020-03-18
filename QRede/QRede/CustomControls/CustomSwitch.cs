using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRede.CustomControls
{
    public class CustomSwitch : Switch
    {

        //TODO: https://github.com/JaoHundred/QRede/issues/13

        public static readonly BindableProperty SwitchOffColorProperty =
     BindableProperty.Create(nameof(OffColor),
         typeof(Color), typeof(CustomSwitch),
         Color.Green);

        public Color OffColor
        {
            get { return (Color)GetValue(SwitchOffColorProperty); }
            set { SetValue(SwitchOffColorProperty, value); }
        }
    }
}
