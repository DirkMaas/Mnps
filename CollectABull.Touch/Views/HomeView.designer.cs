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
	[Register ("HomeView")]
	partial class HomeView
	{
		[Outlet]
		MonoTouch.UIKit.UIButton AddButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView ImageHolder { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView LatestImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LatestTitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ListButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton SearchButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SearchButton != null) {
				SearchButton.Dispose ();
				SearchButton = null;
			}

			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}

			if (ImageHolder != null) {
				ImageHolder.Dispose ();
				ImageHolder = null;
			}

			if (LatestImageView != null) {
				LatestImageView.Dispose ();
				LatestImageView = null;
			}

			if (LatestTitleLabel != null) {
				LatestTitleLabel.Dispose ();
				LatestTitleLabel = null;
			}

			if (ListButton != null) {
				ListButton.Dispose ();
				ListButton = null;
			}
		}
	}
}
