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
    [Activity(Label = "MorningBrew", Theme = "@style/splashscreen", Icon = "@drawable/brewicon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(global::Android.Resource.Style.ThemeHoloLight);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            AnimationViewRenderer.Init();
            LoadApplication(new App());
        }
    }
}

