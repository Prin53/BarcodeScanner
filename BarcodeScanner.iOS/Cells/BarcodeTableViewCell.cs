using System;
using BarcodeScanner.Core.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace BarcodeScanner.iOS.Cells
{
    public partial class BarcodeTableViewCell : TableViewCell
    {
        protected BarcodeTableViewCell(IntPtr handle) : base(handle)
        {
            /* Reuqired constructor */
        }

        public override void OnBind()
        {
            base.OnBind();

            var set = this.CreateBindingSet<BarcodeTableViewCell, BarcodeViewModel>();
            set.Bind(TextLabel).To(vm => vm.Name);
            set.Bind(DetailTextLabel).To(vm => vm.Data);
            set.Apply();
        }
    }
}
