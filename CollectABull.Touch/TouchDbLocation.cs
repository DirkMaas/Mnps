using MonoTouch.UIKit;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using CollectABull.Core.Services.DataStore;
using System.IO;
using MonoTouch.Foundation;
using System;
using Cirrious.CrossCore;

namespace CollectABull.Touch
{
	public class TouchDbLocation : DbLocation, IDbLocation
	{
		private readonly string _fromDir;
		private readonly string _toDir;

		public TouchDbLocation()
		{
			_fromDir = NSBundle.MainBundle.BundlePath;
			// TODO make sure this is the right place to put the database
			// I believe this code comes from 
			// http://developer.xamarin.com/Guides/ios/Application_Fundamentals/Working_with_the_File_System/
//			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
//			var library = Path.Combine(documents, "..", "Library");
//			_toDir = Path.Combine(library, "MNPS");

//			this is where Stuart puts it
			_toDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}

		public string SourceDbDirectory 
		{
			// TODO not including set means it's read only?
			get {return _fromDir;}
		}
		public string TargetDbDirectory 
		{
			get {return _toDir;}
		}


		public void CopyDb()
		{
			// Copy the database across (if it doesn't exist)
//			var appdir = NSBundle.MainBundle.ResourcePath;
//			var seedFile = Path.Combine (appdir, "data.sqlite");
//			if (!File.Exists (Database.DatabaseFilePath))
//				File.Copy (seedFile, Database.DatabaseFilePath);
			File.Copy(Path.Combine(SourceDbDirectory, DbName), Path.Combine(TargetDbDirectory, DbName));
		}

		public bool SourceDbExists()
		{
			return File.Exists(Path.Combine(SourceDbDirectory, DbName));
		}

		public void ConsolePrint(string s)
		{
			Console.WriteLine(s);
		}
	}
}