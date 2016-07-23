using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using UIKit;

namespace BarcodeScanner.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            new Setup(this, Window).Initialize();

            Mvx.Resolve<IMvxAppStart>().Start();

            Window.MakeKeyAndVisible();

            return true;
        }

        private static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}

