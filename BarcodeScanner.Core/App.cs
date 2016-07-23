using Acr.UserDialogs;
using BarcodeScanner.Core.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace BarcodeScanner.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.RegisterSingleton<IMobileServiceClient>(new MobileServiceClient("http://barcodescannertest.azurewebsites.net/"));
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<BarcodesViewModel>());
        }
    }
}

