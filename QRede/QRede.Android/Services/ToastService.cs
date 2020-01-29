﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace QRede.Droid.Services
{
    public static class ToastService
    {
        public static void ToastLongMessage(string message, int fontSize = 14)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Toast toast = Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long);

                SetFontSize(toast, fontSize);

                toast.Show();
            });
        }

        private static void SetFontSize(Toast toast, int fontSize)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ViewGroup toastView = (ViewGroup)toast.View;

                if (toastView.ChildCount > 0 && toastView.GetChildAt(0) is TextView)
                {
                    var textView = (TextView)toastView.GetChildAt(0);
                    textView.SetTextSize(Android.Util.ComplexUnitType.Sp, fontSize);
                }
            });
        }
    }
}