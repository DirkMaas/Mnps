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
	[Register ("DetailView")]
	partial class DetailView
	{
		[Outlet]
		MonoTouch.UIKit.UILabel CaptionLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DateTimeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton DeleteButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LocationLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView MainImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel NotesLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CaptionLabel != null) {
				CaptionLabel.Dispose ();
				CaptionLabel = null;
			}

			if (NotesLabel != null) {
				NotesLabel.Dispose ();
				NotesLabel = null;
			}

			if (LocationLabel != null) {
				LocationLabel.Dispose ();
				LocationLabel = null;
			}

			if (MainImageView != null) {
				MainImageView.Dispose ();
				MainImageView = null;
			}

			if (DateTimeLabel != null) {
				DateTimeLabel.Dispose ();
				DateTimeLabel = null;
			}

			if (DeleteButton != null) {
				DeleteButton.Dispose ();
				DeleteButton = null;
			}
		}
	}
}
