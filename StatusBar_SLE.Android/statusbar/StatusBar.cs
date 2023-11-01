using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.CurrentActivity;
using StatusBar_SLE.Droid.statusbar;
using StatusBar_SLE.VistaModelo;
using Xamarin.Forms;

[assembly:Dependency(typeof(StatusBar))]

namespace StatusBar_SLE.Droid.statusbar
{
    internal class StatusBar : VMstatusbar
    {

        WindowManagerFlags _originalFlag;

        Window GetCurrentWindow()
        {
            var window = CrossCurrentActivity.Current.Activity.Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            return window;
        }
        public void CambiarColor()
        {
            if (Build.VERSION.SdkInt>= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutStable;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Rgb(18,18,18));
                });
            }
            
        }


        public void MostrarStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlag;
            activity.Window.Attributes = attrs;
        }


        public void OcultarStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlag = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;



        }

        public void Transparente()
        {

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutFullscreen;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Transparent);
                });
            }
        }
        public void Traslucido()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlag = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }
    }
}