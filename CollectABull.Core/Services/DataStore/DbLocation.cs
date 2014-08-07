using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace CollectABull.Core.Services.DataStore
{
	public class DbLocation
	{
		private string _dbName;
		public string DbName 
		{ 
			get { return _dbName; }
//			private set { _dbName = value; }
		}
		public DbLocation()
		{
			_dbName = "test.db";
		}
	}
}
