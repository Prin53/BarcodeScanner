// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace BarcodeScanner.iOS.Views
{
	[Register ("BarcodeView")]
	partial class BarcodeView
	{
		[Outlet]
		UIKit.UIButton ButtonScan { get; set; }

		[Outlet]
		UIKit.UITextView TextData { get; set; }

		[Outlet]
		BarcodeScanner.iOS.Controls.DateTextField TextDate { get; set; }

		[Outlet]
		UIKit.UITextField TextName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ButtonScan != null) {
				ButtonScan.Dispose ();
				ButtonScan = null;
			}

			if (TextData != null) {
				TextData.Dispose ();
				TextData = null;
			}

			if (TextName != null) {
				TextName.Dispose ();
				TextName = null;
			}

			if (TextDate != null) {
				TextDate.Dispose ();
				TextDate = null;
			}
		}
	}
}
