using System;
using System.Diagnostics;
using System.IO;
//using ServiceStack.Text;
using CollectABull.Core;

namespace CollectABull.Touch
{
	public class TouchDebugLog : IDebugLog
	{
#if DEBUG
		public static bool DebugMode = true;
#else
		public static bool DebugMode = false;
#endif
//		public static void Dump<T> (T o)
//		{
//			if (o == null) {
//				Log ("LogObject was NULL");
//				return;
//			}
//
//			try {
//				string json = JsonSerializer.SerializeToString<T> (o);
//				//string.Format gets funny with all the {}
//				Log (json.Replace ("{", "{{").Replace ("}", "}}"));
//
//			} catch (Exception ex) {
//				Log ("error logging object");
//				LogException (ex);
//			}
//		}

		static object loggingGate = new object ();

		public void Log (string msg, params object[] param)
		{
			string res = msg;
			if (param.Length > 0)
				res = string.Format(msg, param);

			if (DebugMode) 
			{
				Console.WriteLine(res);

				lock (loggingGate) 
				{
					using (StreamWriter sw = File.AppendText(LogFilename))
					{
						sw.WriteLine (DateTime.Now.ToString ("yyyyMMdd/HHmmss") + ": " + res);
						sw.Flush ();
						sw.Close ();
					}
				}

			}
		}

		public string LogContent {
			get {
				lock (loggingGate) {
					if (!File.Exists (LogFilename))
						return "No log file present";
					return File.ReadAllText (LogFilename);
				}
			}
		}

		public void LogException(Exception ex)
		{
			Log ("Exception: " + ex.ToString ());
			Log ("StackTrace: " + ex.StackTrace.ToString ());
			if (ex.InnerException != null) {
				LogException (ex.InnerException);
			}
		}

		public void LogCurrentStack()
		{
			StackTrace st = new StackTrace ();
			Log ("Stack trace: " + st.ToString ());
		}

		public void CheckLogsForCleanup ()
		{

			if (File.Exists (LogFilename)) {
				FileInfo fi = new FileInfo (LogFilename);
				//BTLogger.Log ("Log file is {0:0} kb", fi.Length / 1024);
				if (fi.Length > (70 * 1024)) 
				{
					DeleteLogFile ();
					Log ("Log file purged - more than 70k");
				}
			}

		}

		public void DeleteLogFile ()
		{
			lock (loggingGate) {
				if (File.Exists (LogFilename)) {
					File.Delete (LogFilename);
				}
			}
		}

		public string LogFilename {
			get {
				return Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "bigted.log");
			}
		}
	}
}


