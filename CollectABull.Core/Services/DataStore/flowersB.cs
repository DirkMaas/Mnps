using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SQLiteNetExtensions.Attributes;

namespace CollectABull.Core.Services.DataStore
{
	public class flowersB
	{
		[PrimaryKey, AutoIncrement]
		public int key { get; set; }
		public string commonName { get; set; }
		public string genus { get; set; }
		public string species { get; set; }
		public string subspecies { get; set; }
		public int numPetals { get; set; }
		[ForeignKey(typeof(heightV))]
		public int height { get; set; }
		[ForeignKey(typeof(leafArrangementV))]
		public int leafArrangement { get; set; }
		[ForeignKey(typeof(flowerShapeV))]
		public int flowerShape { get; set; }
		[ForeignKey(typeof(leafShapeV))]
		public int leafShape { get; set; }
		[ForeignKey(typeof(leaf_stemTextureV))]
		public int leaf_stemTexture { get; set; }
		[ForeignKey(typeof(leafMarginV))]
		public int leafMargin { get; set; }
		[ForeignKey(typeof(leafVenationV))]
		public int leafVenation { get; set; }
	}	
}
