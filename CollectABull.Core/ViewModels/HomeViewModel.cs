using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Plugins.File;
using Cirrious.MvvmCross.ViewModels;
using CollectABull.Core.Services.Collections;
using CollectABull.Core.Services.DataStore;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace CollectABull.Core.ViewModels
{
    public class HomeViewModel 
		: MvxViewModel
    {
//        private readonly ICollectionService _collectionService;
        private MvxSubscriptionToken _collectionChangedSubscriptionToken;

		public HomeViewModel(
//			ICollectionService collectionService, 
			IDbLocation loc,
			IDebugLog log,
			IMvxFileStore fs,
			IMvxMessenger messenger
		)
        {

//            _collectionService = collectionService;
            _collectionChangedSubscriptionToken = messenger.Subscribe<CollectionChangedMessage>(OnCollectionChanged);

/* What do we want to happen?
 * - Print where we think db is, and where we want it to go
 * - Make sure db is where we think it is, if not, print error and throw exception
 * - make sure target directory exists
 * - do the copy or move
 * - check for existence of target, print error and throw exception if not there
*/

			log.Log("During development, database will always be copied");
			fs.DeleteFile(fs.PathCombine(loc.TargetDbDirectory, loc.DbName));

			log.Log("Source db should be at " + fs.PathCombine(loc.SourceDbDirectory, loc.DbName));
			log.Log("After copy/move, target db should be at " + fs.PathCombine(loc.TargetDbDirectory, loc.DbName));
			if (!loc.SourceDbExists()) 
			{
				log.Log("Source db not found at " + fs.PathCombine(loc.SourceDbDirectory, loc.DbName));
				var ex = new System.Exception("No source database found");
				log.LogException(ex);
				throw ex;
			}
			fs.EnsureFolderExists(loc.TargetDbDirectory);
			if (fs.Exists(fs.PathCombine(loc.TargetDbDirectory, loc.DbName)))
			{
				log.Log("No action taken; Target db found at " + fs.PathCombine(loc.TargetDbDirectory, loc.DbName));
			}
			else 
			{
				loc.CopyDb();
				if (!fs.Exists(fs.PathCombine(loc.TargetDbDirectory, loc.DbName)))
				{
					log.Log("Database copy or move failed");
					var ex =  new System.Exception("Database copy or move failed");
					log.LogException(ex);
					throw ex;
				}
			}
            UpdateLatest();

        }

        private void OnCollectionChanged(CollectionChangedMessage obj)
        {
            UpdateLatest();
        }

        private void UpdateLatest()
        {
//            Latest = _collectionService.Latest;
        }

        private SearchResultItem _latest;
        public SearchResultItem Latest
        {
            get { return _latest; }
            set { _latest = value; RaisePropertyChanged(() => Latest); }
        }

        private Cirrious.MvvmCross.ViewModels.MvxCommand _searchCmd;
        public System.Windows.Input.ICommand SearchCommand
        {
            get
            {
                _searchCmd = _searchCmd ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoSearch);
                return _searchCmd;
            }
        }
        private void DoSearch()
        {
			ShowViewModel<SearchViewModel>();
        }

        private Cirrious.MvvmCross.ViewModels.MvxCommand _listCommand;
        public System.Windows.Input.ICommand ListCommand
        {
            get
            {
                _listCommand = _listCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoList);
                return _listCommand;
            }
        }

        private void DoList()
        {
            ShowViewModel<ListViewModel>();
        }                
    }
}
