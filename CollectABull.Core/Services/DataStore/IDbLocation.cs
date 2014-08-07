using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace CollectABull.Core.Services.DataStore
{
	public interface IDbLocation
	{
		bool SourceDbExists();
		void CopyDb();
		string DbName { get; }
		string SourceDbDirectory { get; }
		string TargetDbDirectory { get; }
	}
}
