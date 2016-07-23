using MvvmCross.Plugins.Messenger;

namespace BarcodeScanner.Core.Messages
{
    public class DataChangedMessage : MvxMessage
    {
        public DataChangedMessage(object sender) : base(sender)
        {
            /* Required constructor */
        }
    }
}

