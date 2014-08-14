using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
//using SQLiteNetExtensions.Attributes;
//using SQLiteNetExtensions.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace CollectABull.Core.Services.DataStore
{
	public interface ITableVSearchCellData
	{
		// database key
		int key { get; }
		// text description
		string descrip { get; }
		// location of the cell image in the filesystem
		string graphic { get; }
	}
	public interface ITableVSearchCell
	{
		List<ITableVSearchCellData> GetVTable<T>();
	}

	public interface INumberFound
	{
		int NumberFound(int? color, int? flowerShape, int? habitat, int? height, int? leaf_stem, int? leafArrangement,
			int? leafMargin, int? leafShape, int? leafVenation);
	}

	public class Repository3 : ITableVSearchCell, INumberFound
	{
		private readonly Dictionary<string, List<ITableVSearchCellData>> _cellInfo;
		private readonly ISQLiteConnection _dbConn;
//		private readonly List<colorsV> _colorsV;
		private readonly List<flowerColorR> _flowerColorR;
		private readonly List<flowerHabitatR> _flowerHabitatR;
		private readonly List<flowersB> _flowersB;
//		private readonly List<flowerShapeV> _flowerShapeV;
//		private readonly List<habitatV> _habitatV;
//		private readonly List<heightV> _heightV;
//		private readonly List<leaf_stemTextureV> _leaf_stemTextureV;
//		private readonly List<leafArrangementV> _leafArrangmentV;
//		private readonly List<leafMarginV> _leafMarginV;
//		private readonly List<leafShapeV> _leafShapeV;
//		private readonly List<leafVenationV> _leafVenationV;
//		IDebugLog log;
//		public Repository3()
//		{
//			string connStr = "Data Source=c:\\mydb.db;Version=3;FailIfMissing=True;Read Only=True;DateTimeFormat=Ticks";
//
//		}

		public Repository3(ISQLiteConnectionFactoryEx factory, IDbLocation dbLoc/*, IDebugLog l*/)
		{
//			log = l;
			var options = new SQLiteConnectionOptions 
			{
				Address = dbLoc.DbName,
				BasePath = dbLoc.TargetDbDirectory,
				StoreDateTimeAsTicks = true,
				Type = SQLiteConnectionOptions.DatabaseType.File 
			};
			_dbConn = factory.CreateEx(options);

#if false
			try
			{
//				List<ITableVSearchCellData> temp = GetAll<colorsV>();
//				foreach (ITableVSearchCellData t in temp)
				var temp = GetAll2<colorsV>();
				foreach (colorsV t in temp)
				{
					log.Log("key={0} descrip={1} graphic={2}", t.key, t.descrip, t.graphic);
				}
			}
			catch (Exception e)
			{
				log.LogException(e);
			}
#endif

			_cellInfo = new Dictionary<string, List<ITableVSearchCellData>>();
			_cellInfo.Add(typeof(colorsV).Name, GetList<colorsV>());
			_cellInfo.Add(typeof(flowerShapeV).Name, GetList<flowerShapeV>());
			_cellInfo.Add(typeof(habitatV).Name, GetList<habitatV>());
			_cellInfo.Add(typeof(heightV).Name, GetList<heightV>());
			_cellInfo.Add(typeof(leafArrangementV).Name, GetList<leafArrangementV>());
			_cellInfo.Add(typeof(leafMarginV).Name, GetList<leafMarginV>());
			_cellInfo.Add(typeof(leafShapeV).Name, GetList<leafShapeV>());
			_cellInfo.Add(typeof(leafVenationV).Name, GetList<leafVenationV>());
			_cellInfo.Add(typeof(leaf_stemTextureV).Name, GetList<leaf_stemTextureV>());

			_flowerColorR = GetAll<flowerColorR>();
			_flowerHabitatR = GetAll<flowerHabitatR>();
			_flowersB = GetAll<flowersB>();
		}

		private List<ITableVSearchCellData> GetList<T>() where T : ITableVSearchCellData, new()
		{
			var l1 = GetAll<T>();
			l1.OrderBy(x => x.descrip);
			var l2 = new List<ITableVSearchCellData>();
			foreach (T t in l1)
				l2.Add(t);
			return l2;
		}
		private List<T> GetAll<T>() where T : /*ITableVSearchCellData,*/ new()
		{
			return _dbConn
				.Table<T>()
//				.OrderBy(x => x.descrip)
				.ToList();
		}
//		private List<ITableVSearchCellData> GetAll2<T>() where T : ITableVSearchCellData, new()
//		{
//			return _dbConn
//				.Table<T>()
//				.OrderBy(x => x.descrip)
//				.ToList() as List<ITableVSearchCellData>; // compiles, but returns null
//		}

		public List<ITableVSearchCellData> GetVTable<T>()
		{ 
			return _cellInfo[typeof(T).Name]; 
		}

		void DoStuff(ref List<MyInt> tmp, ref bool all, ref bool prevResults, ref List<MyInt> results)
		{
			if (prevResults)
			{
				// do a join, and only keep ones that are duplicated
				results = 
					(from r in results
						join b in tmp on r.Key equals b.Key
						select new MyInt { Key = b.Key }).ToList();
			}
			else
				results = tmp;
			all = false;
			prevResults = true;
		}


		#region INumberFound implementation

		public int NumberFound(int? color, int? flowerShape, int? habitat, int? height, int? leaf_stem, 
			int? leafArrangement, int? leafMargin, int? leafShape, int? leafVenation)
		{
			bool all = true;
			bool prevResults = false;
			List<MyInt> results = new List<MyInt>();
			if (color != null)
			{
				results = 
					(from r in _flowerColorR
						where r.color.Equals((int)color)
						join b in _flowersB on r.flowerPk equals b.key
						select new MyInt {Key = b.key}).ToList();
				all = false;
				prevResults = true;
			}
			if (flowerShape != null)
			{
				List<MyInt> tmp = 
					(from b in _flowersB
					where b.flowerShape.Equals((int)flowerShape)
					select new MyInt {Key = b.key}).ToList();
				DoStuff(ref tmp, ref all, ref prevResults, ref results);
			}
			if (habitat != null)
			{
				List<MyInt> tmp = 
					(from r in _flowerHabitatR
						where r.habitat.Equals((int)habitat)
						join b in _flowersB on r.flowerPk equals b.key
						select new MyInt {Key = b.key}).ToList();
				DoStuff(ref tmp, ref all, ref prevResults, ref results);
			}
			if (height != null)
			{
				List<MyInt> tmp = 
					(from b in _flowersB
						where b.height.Equals((int)height)
						select new MyInt {Key = b.key}).ToList();
				DoStuff(ref tmp, ref all, ref prevResults, ref results);
			}
			if (leaf_stem != null)
			{
				List<MyInt> tmp = 
					(from b in _flowersB
						where b.leaf_stemTexture.Equals((int)leaf_stem)
						select new MyInt {Key = b.key}).ToList();
				DoStuff(ref tmp, ref all, ref prevResults, ref results);
			}
			if (leafArrangement != null)
			{
				List<MyInt> tmp = 
					(from b in _flowersB
						where b.leafArrangement.Equals((int)leafArrangement)
						select new MyInt {Key = b.key}).ToList();
				DoStuff(ref tmp, ref all, ref prevResults, ref results);
			}
			if (leafMargin != null)
			{
				List<MyInt> tmp = 
					(from b in _flowersB
						where b.leafMargin.Equals((int)leafMargin)
						select new MyInt {Key = b.key}).ToList();
				DoStuff(ref tmp, ref all, ref prevResults, ref results);
			}
			if (leafShape != null)
			{
				List<MyInt> tmp = 
					(from b in _flowersB
						where b.leafShape.Equals((int)leafShape)
						select new MyInt {Key = b.key}).ToList();
				DoStuff(ref tmp, ref all, ref prevResults, ref results);
			}
			if (leafVenation != null)
			{
				List<MyInt> tmp = 
					(from b in _flowersB
						where b.leafVenation.Equals((int)leafVenation)
						select new MyInt {Key = b.key}).ToList();
				DoStuff(ref tmp, ref all, ref prevResults, ref results);
			}

			if (all)
				return _flowersB.Count();
			return results./*Distinct().*/Count();
		}
		#endregion

		private class MyInt : IEquatable<MyInt>
		{
			public int Key { get; set; }

			#region IEquatable implementation

			public bool Equals(MyInt other)
			{
				if (ReferenceEquals(this, other))
					return true;
				if (ReferenceEquals(other, null) || ReferenceEquals(this, null))
					return false;

				return Key.Equals(other.Key);
			}
			public override int GetHashCode() 
			{
				return Key;
			}

			public override bool Equals(object obj) 
			{
				return Equals(obj as MyInt);
			}
			#endregion
		}
	}
}
