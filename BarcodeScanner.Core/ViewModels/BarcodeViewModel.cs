using System;
using System.Windows.Input;
using Acr.UserDialogs;
using BarcodeScanner.Core.Models;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ZXing.Mobile;
using MvvmCross.Plugins.Messenger;
using BarcodeScanner.Core.Messages;

namespace BarcodeScanner.Core.ViewModels
{
    public class BarcodeViewModel : MvxViewModel
    {
        private static readonly string _tag = typeof(BarcodesViewModel).Name;

        protected IMobileServiceClient Client { get; private set; }

        protected IUserDialogs Dialogs { get; private set; }

        protected IMvxMessenger Messenger { get; private set; }

        public BarcodeViewModel(IMobileServiceClient client, IUserDialogs dialogs, IMvxMessenger messenger)
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

            Model = new Barcode
            {
                Date = DateTime.Now
            };
        }

        public async void Init(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            
            Dialogs.ShowLoading();
            await Client.GetTable<Barcode>().LookupAsync(id).ContinueWith(task => {
                Dialogs.HideLoading();
                if (task.IsFaulted)
                {
                    Mvx.TaggedError(_tag, task.Exception.ToString());
                    Dialogs.ShowError(task.Exception.GetBaseException().Message);
                    return;
                }
                if (task.Result != null)
                {
                    Model = task.Result;
                }
            }).ConfigureAwait(false);
        }

        #region Model

        private Barcode _model;

        public Barcode Model
        {
            get { return _model; }
            private set
            {
                _model = value;
                RaiseAllPropertiesChanged();
            }
        }


        #endregion

        #region Id

        public string Id
        {
            get { return Model.Id; }
            set
            {
                Model.Id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        #endregion

        #region Name

        public string Name
        {
            get { return Model.Name; }
            set
            {
                Model.Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        #endregion

        #region Data

        public string Data
        {
            get { return Model.Data; }
            set
            {
                Model.Data = value;
                RaisePropertyChanged(() => Data);
            }
        }

        #endregion

        #region Date

        public DateTime Date
        {
            get { return Model.Date; }
            set
            {
                Model.Date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        #endregion

        #region Save Command

        private ICommand _saveCommand;

        public ICommand SaveCommand
        {
            get { return _saveCommand = _saveCommand ?? new MvxCommand(DoSaveCommand); }
        }

        private async void DoSaveCommand()
        {
            Dialogs.ShowLoading();
            await (string.IsNullOrEmpty(Model.Id) ? Client.GetTable<Barcode>().InsertAsync(Model) : Client.GetTable<Barcode>().UpdateAsync(Model)).ContinueWith(task =>
            {
                Dialogs.HideLoading();
                if (task.IsFaulted)
                {
                    Mvx.TaggedError(_tag, task.Exception.ToString());
                    Dialogs.ShowError(task.Exception.GetBaseException().Message);
                    return;
                }
                Messenger.Publish(new DataChangedMessage(this));
                Dialogs.ShowSuccess("Item saved");
            }).ConfigureAwait(false);
        }

        #endregion

        #region Scan Command

        private ICommand _scanCommand;

        public ICommand ScanCommand
        {
            get { return _scanCommand = _scanCommand ?? new MvxCommand(DoScanCommand); }
        }

        private async void DoScanCommand()
        {
            var result = await new MobileBarcodeScanner().Scan();
            Data = result == null ? null : result.Text;
        }

        #endregion

        public static BarcodeViewModel Create(Barcode barcode)
        {
            if (barcode == null)
            {
                throw new ArgumentNullException(nameof(barcode));
            }

            var viewModel = Mvx.IocConstruct<BarcodeViewModel>();
            viewModel.Model = barcode;

            return viewModel;
        }
    }
}

