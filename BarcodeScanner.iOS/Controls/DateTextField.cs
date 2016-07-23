using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace BarcodeScanner.iOS.Controls
{
    [Register("DateTextField")]
    public class DateTextField : UITextField
    {
        public UIDatePicker Picker { get; private set; }

        public DateTextField(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        public DateTextField(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public DateTextField(CGRect frame) : base(frame)
        {
            Initialize();
        }

        private void Initialize()
        {
            Picker = new UIDatePicker
            {
                Mode = UIDatePickerMode.Date
            };

            var toolbar = new UIToolbar();
            toolbar.SizeToFit();
            toolbar.SetItems(
                new[] {
                    new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
                    new UIBarButtonItem(
                        "Done",
                        UIBarButtonItemStyle.Done,
                        (sender, args) => ResignFirstResponder()
                    )
                },
                true
            );

            InputView = Picker;
            InputAccessoryView = toolbar;
        }

        public override CGRect GetCaretRectForPosition(UITextPosition position)
        {
            return new CGRect(0, 0, 0, 0);
        }

        public override bool CanPerform(ObjCRuntime.Selector action, NSObject withSender)
        {
            return false;
        }
    }
}

