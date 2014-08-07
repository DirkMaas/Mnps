using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SQLiteNetExtensions.Attributes;

namespace CollectABull.Core.Services.DataStore
{
	public class flowerHabitatR
	{
		[PrimaryKey, AutoIncrement]
		public int key { get; set; }
		[ForeignKey(typeof(flowersB))]
		public int flowerPk { get; set; }
		[ForeignKey(typeof(habitatV))]
		public int habitat { get; set; }
	}
}
