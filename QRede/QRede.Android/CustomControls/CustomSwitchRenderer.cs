using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRede.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSwitch)
    , typeof(QRede.Droid.CustomControls.CustomSwitchRenderer))]
namespace QRede.Droid.CustomControls
{
    public class CustomSwitchRenderer : SwitchRenderer
    {
        public CustomSwitchRenderer(Context context) : base(context)
        { }

        private CustomSwitch view;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;
            view = (CustomSwitch)Element;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                if (this.Control != null)
                {
                    if (this.Control.Checked)
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.OnColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                    }
                    else
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.OffColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                    }
                    this.Control.CheckedChange += this.OnCheckedChange;
                }
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (this.Control.Checked)
            {
                this.Control.TrackDrawable.SetColorFilter(view.OnColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
            }
            else
            {
                this.Control.TrackDrawable.SetColorFilter(view.OffColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
            }

            Element.IsToggled = Control.Checked;
        }

        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }
    }
}
