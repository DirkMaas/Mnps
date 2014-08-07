using System;
using Cirrious.MvvmCross.ViewModels;
using CollectABull.Core.ViewModels;
using System.Collections.Generic;
using CollectABull.Core.Services.Collections;
using CollectABull.Core.Services.DataStore;

namespace CollectABull.Core
{
//	public class CDbKey
//	{
//		private int? _color = null;
//		public int? Color
//		{
//			get { return _color; }
//			set { _color = value; RaisePropertyChanged(() => Color); }
//		}
//	}
	public class SearchViewModel : MvxViewModel
	{
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
		public SearchViewModel(/*ICollectionService cs*/)
		{
//			_items.Add(new Thing(null));
//			foreach (colorsV cv in cs.GetAllColorsV())
//				_items.Add(new Thing(cv.color));

		}
		private int? _color = null;
		public int? Color
		{
			get { return _color; }
			set { _color = value; RaisePropertyChanged(() => Color); }
		}

		private int? _flowerShape = null;
		public int? FlowerShape
		{
			get { return _flowerShape; }
			set { _flowerShape = value; RaisePropertyChanged(() => FlowerShape); }
		}

		private int? _habitat = null;
		public int? Habitat
		{
			get { return _habitat; }
			set { _habitat = value; RaisePropertyChanged(() => Habitat); }
		}

		private int? _height = null;
		public int? Height
		{
			get { return _height; }
			set { _height = value; RaisePropertyChanged(() => Height); }
		}

		private int? _leafArrangement = null;
		public int? LeafArrangement
		{
			get { return _leafArrangement; }
			set { _leafArrangement = value; RaisePropertyChanged(() => LeafArrangement); }
		}

		private int? _leafShape = null;
		public int? LeafShape
		{
			get { return _leafShape; }
			set { _leafShape = value; RaisePropertyChanged(() => LeafShape); }
		}

		private int? _leafMargin = null;
		public int? LeafMargin
		{
			get { return _leafMargin; }
			set { _leafMargin = value; RaisePropertyChanged(() => LeafMargin); }
		}

		private int? _leafVenation = null;
		public int? LeafVenation
		{
			get { return _leafVenation; }
			set { _leafVenation = value; RaisePropertyChanged(() => LeafVenation); }
		}

//		private List<bool> _colors = new List<bool>();
//		public List<bool> Colors
//		{
//			get { return _colors; }
//			set { _colors = value; RaisePropertyChanged(() => Colors); }
//		}
#if false

		private List<string> _items = new List<string>();

		public List<string> Items {
			get { return _items; }
			set {
				_items = value;
				RaisePropertyChanged(() => Items);
			}
		}

		private string _selectedItem = "";

		public string SelectedItem {
			get { return _selectedItem; }
			set {
				_selectedItem = value;
				RaisePropertyChanged(() => SelectedItem);
			}
		}
#else
		public class Thing
		{
			public Thing(string caption)
			{
				Caption = caption;
			}

			public string Caption { get; private set; }

			public override string ToString()
			{
				return Caption;
			}

			public override bool Equals(object obj)
			{
				var rhs = obj as Thing;
				if (rhs == null)
					return false;
				return rhs.Caption == Caption;
			}

			public override int GetHashCode()
			{
				if (Caption == null)
					return 0;
				return Caption.GetHashCode();
			}
		}
		private List<Thing> _items = new List<Thing>();
		public List<Thing> Items
		{
			get { return _items; }
			set { _items = value; RaisePropertyChanged(() => Items); }
		}

		private Thing _selectedItem = new Thing(null);
		public Thing SelectedItem
		{
			get { return _selectedItem; }
			set { _selectedItem = value; RaisePropertyChanged(() => SelectedItem); }
		}

#endif

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


		private Cirrious.MvvmCross.ViewModels.MvxCommand _searchResultsCmd;
		public System.Windows.Input.ICommand SearchResultsCmd
		{
			get
			{
				_searchResultsCmd = _searchResultsCmd ?? new Cirrious.MvvmCross.ViewModels.MvxCommand(DoSearchResults);
				return _searchResultsCmd;
			}
		}
		private void DoSearchResults()
		{
			ShowViewModel<ListViewModel>();
		}                
	}
}