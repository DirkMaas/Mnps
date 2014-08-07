using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.MvvmCross.Plugins.Messenger;
using CollectABull.Core.Services.DataStore;

namespace CollectABull.Core.Services.Collections
{
	public class CollectionService : ICollectionService
    {
        private readonly IRepository _repository;
        private readonly IMvxMessenger _messenger;

        public CollectionService(IRepository repository, IMvxMessenger messenger)
        {
            _repository = repository;
            _messenger = messenger;
        }

        public List<SearchResultItem> All()
        {
            return _repository.All();
        }

        public SearchResultItem Get(int id)
        {
            return _repository.Get(id);
        }
		public List<colorsV> GetAllColorsV()
		{
			return _repository.GetAllColorsV();
		}

		public List<flowerShapeV> GetAllFlowerShapeV()
		{
			return _repository.GetAllFlowerShapeV();
		}
		public List<habitatV> GetAllHabitatV()
		{
			return _repository.GetAllHabitatV();
		}
		public List<heightV> GetAllHeightV()
		{
			return _repository.GetAllHeightV();
		}
		public List<leaf_stemTextureV> GetAllLeaf_StemTextureV()
		{
			return _repository.GetAllLeaf_StemTextureV();
		}
		public List<leafArrangementV> GetAllLeafArrangementV()
		{
			return _repository.GetAllLeafArrangementV();
		}
		public List<leafShapeV> GetAllLeafShapeV()
		{
			return _repository.GetAllLeafShapeV();
		}

#if false
		public void Add(SearchResultItem item)
		{
		_repository.Add(item);
		_messenger.Publish(new CollectionChangedMessage(this));
		}

		public void Delete(SearchResultItem item)
		{
		_repository.Delete(item);
		_messenger.Publish(new CollectionChangedMessage(this));
		}

		public SearchResultItem Latest {
		get {
		return _repository.Latest;
		}
		}
#endif
	}
}
