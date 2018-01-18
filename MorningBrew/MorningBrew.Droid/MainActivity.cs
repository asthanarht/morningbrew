using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Lottie.Forms.Droid;
using Android.Widget;
using Android.OS;

namespace MorningBrew.Droid
{
    [Activity(Label = "MorningBrew", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            AnimationViewRenderer.Init();
            LoadApplication(new App());
        }
    }
}

