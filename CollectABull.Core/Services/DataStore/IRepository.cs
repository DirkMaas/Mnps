using System.Collections.Generic;

namespace CollectABull.Core.Services.DataStore
{
	// interface to the database
	// for MNPS: 
	// All -- get all rows
	// Count -- get count of rows meeting search criteria
	// Select -- get rows meeting search criteria
    public interface IRepository
    {
        List<SearchResultItem> All();
//		CollectedItem Latest { get; }
//		void Add(CollectedItem collectedItem);
//		void Delete(CollectedItem collectedItem);
//		void Update(CollectedItem collectedItem);
		int Count(/* search criteria */);
		List<SearchResultItem> Select(/* search criteria */);
        SearchResultItem Get(int id);
		List<colorsV> GetAllColorsV();
		List<flowerShapeV> GetAllFlowerShapeV();
		List<habitatV> GetAllHabitatV();
		List<heightV> GetAllHeightV();
		List<leaf_stemTextureV> GetAllLeaf_StemTextureV();
		List<leafArrangementV> GetAllLeafArrangementV();
		List<leafShapeV> GetAllLeafShapeV();
    }
	public interface IRepository2<T>
	{
		List<SearchResultItem> All();
		//		CollectedItem Latest { get; }
		//		void Add(CollectedItem collectedItem);
		//		void Delete(CollectedItem collectedItem);
		//		void Update(CollectedItem collectedItem);
		int Count(/* search criteria */);
		List<SearchResultItem> Select(/* search criteria */);
		SearchResultItem Get(int id);
		List<T> GetAll();
	}
}