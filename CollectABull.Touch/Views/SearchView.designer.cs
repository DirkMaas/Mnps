// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace CollectABull.Touch
{
	[Register ("SearchView")]
	partial class SearchView
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnClear { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnShow { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblNumFound { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnClear != null) {
				btnClear.Dispose ();
				btnClear = null;
			}

			if (btnShow != null) {
				btnShow.Dispose ();
				btnShow = null;
			}

			if (lblNumFound != null) {
				lblNumFound.Dispose ();
				lblNumFound = null;
			}
		}
	}
}
