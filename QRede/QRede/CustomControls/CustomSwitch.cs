using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace QRede.CustomControls
{
    public class CustomSwitch : Switch
    {
        public static readonly BindableProperty OffColorProperty = BindableProperty.Create(
         nameof(OffColor),
         returnType: typeof(Color),
         declaringType: typeof(CustomSwitch),
         defaultValue: Color.Default);

        public Color OffColor
        {
            get => (Color)GetValue(OffColorProperty);
            set => SetValue(OffColorProperty, value);
        }
    }
}
