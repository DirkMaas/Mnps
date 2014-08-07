using System.Collections.Generic;
using CollectABull.Core.Services.DataStore;

namespace CollectABull.Core.Services.Collections
{
    public interface ICollectionService
    {
        List<SearchResultItem> All();
//      CollectedItem Latest { get; }
//		void Add(CollectedItem item);
//		void Delete(CollectedItem item);
        SearchResultItem Get(int id);
		List<colorsV> GetAllColorsV();
		List<flowerShapeV> GetAllFlowerShapeV();
		List<habitatV> GetAllHabitatV();
		List<heightV> GetAllHeightV();
		List<leaf_stemTextureV> GetAllLeaf_StemTextureV();
		List<leafArrangementV> GetAllLeafArrangementV();
		List<leafShapeV> GetAllLeafShapeV();
    }
}