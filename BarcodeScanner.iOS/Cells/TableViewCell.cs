using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;

namespace BarcodeScanner.iOS.Cells
{
    public abstract class TableViewCell : MvxTableViewCell
    {
        protected TableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(OnBind);
        }

        public virtual void OnBind()
        {
            /* Nothing to do */
        }
    }

    public class TableViewCell<TViewModel> : TableViewCell
    {
        public TViewModel ViewModel
        {
            get { return (TViewModel)DataContext; }
        }

        protected TableViewCell(IntPtr handle) : base(handle)
        {
            /* Reuqired constructor */
        }
    }
}

