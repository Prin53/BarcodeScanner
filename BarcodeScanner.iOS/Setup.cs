using BarcodeScanner.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using UIKit;

namespace BarcodeScanner.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
        {
            /* Required constructor */
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }
    }
}

