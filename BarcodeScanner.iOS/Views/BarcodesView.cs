using BarcodeScanner.Core.ViewModels;
using BarcodeScanner.iOS.Cells;
using BarcodeScanner.iOS.Sources;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace BarcodeScanner.iOS.Views
{
    public class BarcodesView : MvxTableViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Barcodes";

            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Add", UIBarButtonItemStyle.Done, (sender, args) => { });
            NavigationItem.RightBarButtonItem = EditButtonItem;

            var source = new TableViewSource(TableView);
            source.RegisterCellForReuse<BarcodeTableViewCell>();

            TableView.Source = source;

            var set = this.CreateBindingSet<BarcodesView, BarcodesViewModel>();
            set.Bind(NavigationItem.LeftBarButtonItem).For("Clicked").To(vm => vm.AddCommand);
            set.Bind(source).To(vm => vm.Items);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.ShowCommand);
            set.Bind(source).For(v => v.RemoveCommand).To(vm => vm.RemoveCommand);
            set.Apply();
        }
    }
}

