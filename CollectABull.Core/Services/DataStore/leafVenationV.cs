using System;
using CollectABull.Core.Services.DataStore;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace CollectABull.Core.Services.DataStore
{
	public class leafVenationV : ITableVSearchCellData
	{
		[PrimaryKey, AutoIncrement]
		public int key { get; set; }
		public string descrip { get; set; }
		public string graphic { get; set; }
	}
}