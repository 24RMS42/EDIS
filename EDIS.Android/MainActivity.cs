using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using EDIS.Shared;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Version.Plugin;

namespace EDIS.Droid
{
    [Activity(Label = "EDIS", Icon = "@drawable/edisIcon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            UserDialogs.Init(this);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Messier16.Forms.Android.Controls.Messier16Controls.InitAll();
            CarouselViewRenderer.Init();

            CrashManager.Register(this, "53573302cad44f01b7183ebcd345fa32");
            MetricsManager.Register(Application, "53573302cad44f01b7183ebcd345fa32");

            CheckForUpdates();

            LoadApplication(new App());
        }

        private void CheckForUpdates()
        {
            // Remove this for store builds!
            UpdateManager.Register(this, "53573302cad44f01b7183ebcd345fa32");
        }

        private void UnregisterManagers()
        {
            UpdateManager.Unregister();
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterManagers();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterManagers();
        }
    }
}

