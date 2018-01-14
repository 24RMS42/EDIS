using CarouselView.FormsPlugin.iOS;
using EDIS.Shared;
using FFImageLoading.Forms.Touch;
using Foundation;
using HockeyApp.iOS;
using UIKit;

namespace EDIS.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            Messier16.Forms.iOS.Controls.Messier16Controls.InitAll();
            CarouselViewRenderer.Init();

            CachedImageRenderer.Init();

            SlideOverKit.iOS.SlideOverKit.Init();

            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure("acd1753596bc4dd4bd0923cc530bf871");
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation();

            return base.FinishedLaunching(app, options);
        }
    }
}
