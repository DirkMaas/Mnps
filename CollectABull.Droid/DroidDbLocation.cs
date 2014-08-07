using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using CollectABull.Core.Services.DataStore;
using System;
using System.IO;
using Android.Content.Res;
using Android.App;

namespace CollectABull.Droid
{   
	public class DroidDbLocation : DbLocation, IDbLocation
	{
		private readonly string _fromDir;
		private readonly string _toDir;

		public DroidDbLocation()
		{
			_fromDir = (Resource.Raw.test).ToString();
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
			var s = Application.Context.Resources.OpenRawResource(Resource.Raw.test);  // DATA FILE RESOURCE ID
			FileStream writeStream = 
				new FileStream(Path.Combine(TargetDbDirectory, DbName), FileMode.OpenOrCreate, FileAccess.Write);
			ReadWriteStream(s, writeStream);
		}

		public bool SourceDbExists()
		{
			return true;
		}

		public void ConsolePrint(string s)
		{
			Console.WriteLine(s);
		}


		// readStream is the stream you need to read
		// writeStream is the stream you want to write to
		private void ReadWriteStream(Stream readStream, Stream writeStream)
		{
			int Length = 256;
			Byte[] buffer = new Byte[Length];
			int bytesRead = readStream.Read(buffer, 0, Length);
			// write the required bytes
			while (bytesRead > 0)
			{
				writeStream.Write(buffer, 0, bytesRead);
				bytesRead = readStream.Read(buffer, 0, Length);
			}
			readStream.Close();
			writeStream.Close();
		}	
	}
}