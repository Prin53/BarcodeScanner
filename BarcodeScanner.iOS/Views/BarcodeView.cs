using BarcodeScanner.Core.Converters;
using BarcodeScanner.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace BarcodeScanner.iOS.Views
{
    public partial class BarcodeView : MvxViewController
    {
        public BarcodeView() : base("BarcodeView", null)
        {
            /* Reuqired constructor */
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Barcode";

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Save", UIBarButtonItemStyle.Done, (sender, args) => { });

            TextData.Layer.BorderWidth = .5f;
            TextData.Layer.BorderColor = UIColor.FromRGB(205, 205, 205).CGColor;
            TextData.Layer.CornerRadius = 5;

            var set = this.CreateBindingSet<BarcodeView, BarcodeViewModel>();
            set.Bind(NavigationItem.RightBarButtonItem).For("Clicked").To(vm => vm.SaveCommand);
            set.Bind(TextName).To(vm => vm.Name);
            set.Bind(TextDate).To(vm => vm.Date).WithConversion(new DateStringConverter(), "D");
            set.Bind(TextDate.Picker).For(v => v.Date).To(vm => vm.Date);
            set.Bind(TextData).To(vm => vm.Data);
            set.Bind(ButtonScan).To(vm => vm.ScanCommand);
            set.Apply();
        }
    }
}

