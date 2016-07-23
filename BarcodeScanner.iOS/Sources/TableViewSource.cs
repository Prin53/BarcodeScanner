using System;
using System.Windows.Input;
using Foundation;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace BarcodeScanner.iOS.Sources
{
    public class TableViewSource : MvxTableViewSource
    {
        public ICommand RemoveCommand { get; set; }

        public TableViewSource(UITableView tableView) : base(tableView)
        {
            /* Required constructor */
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableView.AutomaticDimension;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return tableView.DequeueReusableCell(GetCellIdentifier(item.GetType()), indexPath);
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch(editingStyle) {
                case UITableViewCellEditingStyle.Delete:
                    var removeCommand = RemoveCommand;
                    if (removeCommand != null) {
                        removeCommand.Execute(ItemsSource.ElementAt(indexPath.Row));
                    }
                    break;
            }
        }

        public void RegisterCellForReuse<TCell>() where TCell : MvxTableViewCell
        {
            TableView.RegisterNibForCellReuse(UINib.FromName(typeof(TCell).Name, NSBundle.MainBundle), typeof(TCell).Name);
        }

        private static string GetCellIdentifier(Type itemType)
        {
            return itemType.Name.Replace("ViewModel", "TableViewCell");
        }
    }
}

