using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;

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

	public class Repository3 : ITableVSearchCell
	{
		private readonly Dictionary<string, List<ITableVSearchCellData>> _cellInfo;
		private readonly ISQLiteConnection _dbConn;
//		IDebugLog log;

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
		}

		private List<ITableVSearchCellData> GetList<T>() where T : ITableVSearchCellData, new()
		{
			var l1 = GetAll2<T>();
			var l2 = new List<ITableVSearchCellData>();
			foreach (T t in l1)
			{
				l2.Add(t);
			}
			return l2;
		}
		private List<T> GetAll2<T>() where T : ITableVSearchCellData, new()
		{
			return _dbConn
				.Table<T>()
				.OrderBy(x => x.descrip)
				.ToList();
		}
//		private List<ITableVSearchCellData> GetAll<T>() where T : ITableVSearchCellData, new()
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
	}
}





#if false
	public class Repository : IRepository
    {
		private readonly ISQLiteConnection _dbConn;

		public Repository(ISQLiteConnectionFactoryEx factory, IDbLocation dbLoc)
        {
			var options = new SQLiteConnectionOptions 
			{
				Address = dbLoc.DbName,
				BasePath = dbLoc.TargetDbDirectory,
				StoreDateTimeAsTicks = true,
				Type = SQLiteConnectionOptions.DatabaseType.File 
			};
			_dbConn = factory.CreateEx(options);
        }

		public int Count()
		{
//			don't compile
			throw new NotImplementedException();
		}
		public List<SearchResultItem> Select()
		{
//			don't compile
			throw new NotImplementedException();
		}

        public List<SearchResultItem> All()
        {
            return _dbConn
                .Table<SearchResultItem>()
                .OrderByDescending(x => x.WhenUtc)
                .ToList();
        }

		// TODO possible speed improvement -- select only what's needed
		public List<colorsV> GetAllColorsV()
		{
			return _dbConn
				.Table<colorsV>()
//				.OrderBy(x => x.color)
				.ToList();
		}

		public List<flowerShapeV> GetAllFlowerShapeV()
		{
			return _dbConn
				.Table<flowerShapeV>()
//				.OrderBy(x => x.type)
				.ToList();
		}

		public List<habitatV> GetAllHabitatV()
		{
			return _dbConn
				.Table<habitatV>()
//				.OrderBy(x => x.type)
				.ToList();
		}

		public List<heightV> GetAllHeightV()
		{
			return _dbConn
				.Table<heightV>()
//				.OrderBy(x => x.type)
				.ToList();
		}

		public List<leaf_stemTextureV> GetAllLeaf_StemTextureV()
		{
			return _dbConn
				.Table<leaf_stemTextureV>()
//				.OrderBy(x => x.type)
				.ToList();
		}

		public List<leafArrangementV> GetAllLeafArrangementV()
		{
			return _dbConn
				.Table<leafArrangementV>()
//				.OrderBy(x => x.type)
				.ToList();
		}

		public List<leafShapeV> GetAllLeafShapeV()
		{
			return _dbConn
				.Table<leafShapeV>()
//				.OrderBy(x => x.type)
				.ToList();
		}

#if NOT_NEEDED
		public CollectedItem Latest {
			get {
				return _connection
                    .Table<CollectedItem>()
                    .OrderByDescending(x => x.WhenUtc)
                    .FirstOrDefault();
			}
		}

		public void Add(CollectedItem collectedItem)
		{
			_connection.Insert(collectedItem);
		}

		public void Delete(CollectedItem collectedItem)
		{
			_connection.Delete(collectedItem);
		}

		public void Update(CollectedItem collectedItem)
		{
			_connection.Update(collectedItem);
		}
#endif

        public SearchResultItem Get(int id)
        {
            return _dbConn.Get<SearchResultItem>(id);
        }
    }


	public class Repository2<T> : IRepository2<T> where T: new()
	{
		private readonly ISQLiteConnection _dbConn;
//		private readonly 

		public Repository2(ISQLiteConnectionFactoryEx factory, IDbLocation dbLoc)
		{
			var options = new SQLiteConnectionOptions 
			{
				Address = dbLoc.DbName,
				BasePath = dbLoc.TargetDbDirectory,
				StoreDateTimeAsTicks = true,
				Type = SQLiteConnectionOptions.DatabaseType.File 
			};
			_dbConn = factory.CreateEx(options);
		}

		public int Count()
		{
			//			don't compile
			throw new NotImplementedException();
//			IEnumerable<int> flowers _connection.
		}
		public List<SearchResultItem> Select()
		{
			//			don't compile
			throw new NotImplementedException();
		}

		public List<SearchResultItem> All()
		{
			return _dbConn
				.Table<SearchResultItem>()
				.OrderByDescending(x => x.WhenUtc)
				.ToList();
		}

		// TODO possible speed improvement -- select only what's needed
		public List<T> GetAll()
		{
			return _dbConn
				.Table<T>()
//				.OrderBy(x => x.type)
				.ToList();
		}

		public SearchResultItem Get(int id)
		{
			return _dbConn.Get<SearchResultItem>(id);
		}
	}
#endif
