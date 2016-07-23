using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using BarcodeScanner.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using BarcodeScanner.Core.Messages;

namespace BarcodeScanner.Core.ViewModels
{
    public class BarcodesViewModel : MvxViewModel
    {
        private static readonly string _tag = typeof(BarcodesViewModel).Name;

        private MvxSubscriptionToken _token;

        protected IMobileServiceClient Client { get; private set; }

        protected IUserDialogs Dialogs { get; private set; }

        protected IMvxMessenger Messenger { get; private set; }

        public BarcodesViewModel(IMobileServiceClient client, IUserDialogs dialogs, IMvxMessenger messenger)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (dialogs == null)
            {
                throw new ArgumentNullException(nameof(dialogs));
            }

            if (messenger == null)
            {
                throw new ArgumentNullException(nameof(messenger));
            }

            Client = client;
            Dialogs = dialogs;
            Messenger = messenger;

            _token = Messenger.Subscribe<DataChangedMessage>((message) => GetData());
        }

        public void Init()
        {
            GetData();
        }

        private async void GetData()
        {
            Dialogs.ShowLoading();
            await Client.GetTable<Barcode>().ToCollectionAsync().ContinueWith(task =>
            {
                Dialogs.HideLoading();
                if (task.IsFaulted)
                {
                    Mvx.TaggedError(_tag, task.Exception.ToString());
                    Dialogs.ShowError(task.Exception.GetBaseException().Message);
                    return;
                }
                Items = new ObservableCollection<BarcodeViewModel>(task.Result.Select(item => BarcodeViewModel.Create(item)));
            }).ConfigureAwait(false);
        }

        #region Items

        private ObservableCollection<BarcodeViewModel> _items;

        public ObservableCollection<BarcodeViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        #endregion

        #region Add Command

        private ICommand _addCommand;

        public ICommand AddCommand
        {
            get { return _addCommand = _addCommand ?? new MvxCommand(DoAddCommand); }
        }

        private void DoAddCommand()
        {
            ShowViewModel<BarcodeViewModel>();
        }

        #endregion

        #region Remove Command

        private ICommand _removeCommand;

        public ICommand RemoveCommand
        {
            get { return _removeCommand = _removeCommand ?? new MvxCommand<BarcodeViewModel>(DoRemoveCommand); }
        }

        private async void DoRemoveCommand(BarcodeViewModel viewModel)
        {
            Dialogs.ShowLoading();
            await Client.GetTable<Barcode>().DeleteAsync(viewModel.Model).ContinueWith(task =>
            {
                Dialogs.HideLoading();
                if (task.IsFaulted)
                {
                    Mvx.TaggedError(_tag, task.Exception.ToString());
                    Dialogs.ShowError(task.Exception.GetBaseException().Message);
                    return;
                }
                GetData();
            }).ConfigureAwait(false);
        }

        #endregion

        #region Show Command

        private ICommand _showCommand;

        public ICommand ShowCommand
        {
            get { return _showCommand = _showCommand ?? new MvxCommand<BarcodeViewModel>(DoShowCommand); }
        }

        private void DoShowCommand(BarcodeViewModel viewModel)
        {
            ShowViewModel<BarcodeViewModel>(new { id = viewModel.Id });
        }

        #endregion
    }
}

