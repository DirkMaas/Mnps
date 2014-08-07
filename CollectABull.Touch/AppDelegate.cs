using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using System.IO;
using System;
using CollectABull.Core.Services.DataStore;


namespace CollectABull.Touch
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate
	{
		UIWindow _window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
//			var destinationPath = Path.Combine (System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"test.db");
//			using (Stream source = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("test.db")) 
//			{
//				using (var destination = System.IO.File.Create (destinationPath)) 
//				{
//					source.CopyTo (destination);
//				}
//			}

#if MOVED_TO_CORE
			// Copy the database across (if it doesn't exist)
			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var library = Path.Combine(documents, "..", "Library");
			var directoryname = Path.Combine(library, "MNPS");
			var db = Path.Combine(directoryname, "test.db");
			if (!File.Exists(db)) 
			{
				Directory.CreateDirectory(directoryname);
				var seedFile = Path.Combine(NSBundle.MainBundle.BundlePath, "test.db");
				File.Copy(seedFile, db);
			}
#endif

			_window = new UIWindow(UIScreen.MainScreen.Bounds);

			var setup = new Setup(this, _window);
			setup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			_window.MakeKeyAndVisible();

			return true;
		}
	}
}