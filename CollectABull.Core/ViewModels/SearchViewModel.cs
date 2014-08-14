using System;
using Cirrious.MvvmCross.ViewModels;
using CollectABull.Core.ViewModels;
using System.Collections.Generic;
using CollectABull.Core.Services.Collections;
using CollectABull.Core.Services.DataStore;

namespace CollectABull.Core
{
	public class SearchViewModel : MvxViewModel
	{
		INumberFound _nf;
/* Do I have a single variable for each search criteria (color, petals, etc), which would hold its current
 * value such as yellow, 5, etc.  This would not allow multiple selection.  Or do I have lists of booleans,
 * one list for the colors (1 boolean for each color), another for the petals.  Each boolean would be on if 
 * it's selected, off if not.
 * Advantages of A:
 * fewer variables to bind
 * Advantages of B:
 * more flexibility
 * Or do I mix the 2, using B on search criteria for which multiple selection might be an option, and A for
 * all the rest?
 */
		public SearchViewModel(INumberFound nf)
		{
			_nf = nf;
		}
		private int _numFound = 3; // hardcode this to flowersB.Count()?
		public int NumFound
		{
			get { return _numFound; }
			set { _numFound = value; RaisePropertyChanged(() => NumFound); }
		}
		private void SetNumFound()
		{
			NumFound = _nf.NumberFound(Color,
										FlowerShape,
										Habitat,
										Height,
										Leaf_StemTexture,
										LeafArrangement,
										LeafMargin,
										LeafShape,
										LeafVenation);
		}

		private int? _color = null;
		public int? Color
		{
			get { return _color; }
			set 
			{ 
				_color = value; 
				RaisePropertyChanged(() => Color);
				SetNumFound();
			}
		}

		private int? _flowerShape = null;
		public int? FlowerShape
		{
			get { return _flowerShape; }
			set { _flowerShape = value; RaisePropertyChanged(() => FlowerShape); SetNumFound();}
		}

		private int? _habitat = null;
		public int? Habitat
		{
			get { return _habitat; }
			set { _habitat = value; RaisePropertyChanged(() => Habitat); SetNumFound();}
		}

		private int? _height = null;
		public int? Height
		{
			get { return _height; }
			set { _height = value; RaisePropertyChanged(() => Height); SetNumFound();}
		}

		private int? _leaf_stemTexture = null;
		public int? Leaf_StemTexture
		{
			get { return _leaf_stemTexture; }
			set { _leaf_stemTexture = value; RaisePropertyChanged(() => Leaf_StemTexture); SetNumFound();}
		}

		private int? _leafArrangement = null;
		public int? LeafArrangement
		{
			get { return _leafArrangement; }
			set { _leafArrangement = value; RaisePropertyChanged(() => LeafArrangement); SetNumFound();}
		}

		private int? _leafShape = null;
		public int? LeafShape
		{
			get { return _leafShape; }
			set { _leafShape = value; RaisePropertyChanged(() => LeafShape); SetNumFound();}
		}

		private int? _leafMargin = null;
		public int? LeafMargin
		{
			get { return _leafMargin; }
			set { _leafMargin = value; RaisePropertyChanged(() => LeafMargin); SetNumFound();}
		}

		private int? _leafVenation = null;
		public int? LeafVenation
		{
			get { return _leafVenation; }
			set { _leafVenation = value; RaisePropertyChanged(() => LeafVenation); SetNumFound();}
		}

#if false
		private Cirrious.MvvmCross.ViewModels.MvxCommand _countCommand;
		public System.Windows.Input.ICommand CountCommand
		{
			get
			{
				_countCommand = _countCommand ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoCount);
				return _countCommand;
			}
		}

		private void DoCount()
		{
//			ShowViewModel<ListViewModel>();
		}                
#endif

		private Cirrious.MvvmCross.ViewModels.MvxCommand _searchResultsCmd;
		public System.Windows.Input.ICommand SearchResultsCmd
		{
			get
			{
				_searchResultsCmd = _searchResultsCmd ?? 
					new Cirrious.MvvmCross.ViewModels.MvxCommand(DoSearchResults);
				return _searchResultsCmd;
			}
		}
		private void DoSearchResults()
		{
			ShowViewModel<ListViewModel>();
		}                
	}
}