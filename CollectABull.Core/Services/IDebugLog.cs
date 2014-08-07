using System;

namespace CollectABull.Core
{
	public interface IDebugLog
	{
//		void ConsolePrint(string s);
		void Log(string msg, params object[] param);
		string LogContent { get; }
		void LogException(Exception ex);
		void LogCurrentStack();
		void CheckLogsForCleanup();
		void DeleteLogFile();
		string LogFilename { get; }
	}
}

