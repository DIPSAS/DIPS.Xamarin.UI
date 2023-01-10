using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using DIPS.Xamarin.UI.Samples.Droid.MobileSettings;
using Java.Security;
using Xamarin.Forms;
using Essentials = Xamarin.Essentials;
using Content = Android.Content;
using Provider = Android.Provider;
using View = Android.Views.View;

namespace DIPS.Xamarin.UI.Samples.Droid
{
    [Activity(Label = "DIPS.Xamarin.UI.Samples", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Essentials.Platform.Init(this, savedInstanceState); //Xamarin essentials

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            DIPS.Xamarin.UI.Android.Library.Initialize(this);

            InternalShared.MobileSettings.Instance = new AndroidSettings(this);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Content.PM.Permission[] grantResults)
        {
            Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults); //Xamarin essentials

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}