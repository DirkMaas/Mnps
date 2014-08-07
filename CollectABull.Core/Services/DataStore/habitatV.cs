using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace CollectABull.Core.Services.DataStore
{
	public class habitatV : ITableVSearchCellData
	{
		[PrimaryKey, AutoIncrement]
		public int key { get; set; }
		public string descrip { get; set; }
		public string graphic { get; set; }
	}
}
