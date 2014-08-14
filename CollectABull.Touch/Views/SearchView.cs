
using System;
using System.Drawing;

using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using CollectABull.Core;
using MonoTouch.CoreAnimation;
using MonoTouch.UIKit;
using SimpleCollectionView;
using CollectABull.Core.Services.DataStore;
using MonoTouch.Foundation;
using System.Collections.Generic;
using System.Diagnostics;

namespace CollectABull.Touch
{
	public partial class SearchView : MvxViewController
	{
		UIScrollView _scrollView;
		UIViewController _uiVc;
		List<MyCollectionView<MnpsCell>> _cvList;
		enum WhichCollectionView { ColorCv = 1, FlowerShapeCv, HabitatCv, HeightCv, LeafArrangementCv, LeafMarginCv,
			LeafShapeCv, LeafVenationCv };

		public SearchView() : base("SearchView", null)
		{
			_cvList = new List<MyCollectionView<MnpsCell>>();
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		void SetStuff<C, T>(Action<int?> SetKey, float y, WhichCollectionView which, float h=70) where C : MnpsCell // and T is a table class
		{
			var lineLayout = new LineLayout();
			var frame = new RectangleF(0f, y, 320f, h);
			var mcv = new MyCollectionView<C>(frame, lineLayout, false);
			mcv.Delegate = new MyDelegate<C, T>(SetKey);
			mcv.DataSource = new MyDataSource2<C, T>();
			mcv.RegisterClassForCell(typeof(C), new NSString(typeof(C).Name));
			_uiVc.Add(mcv);
			_scrollView.AddSubview(mcv);
			mcv.Tag = (int)which;
//			_cvList.Add(mcv);
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
//			var vw = new UIView(new RectangleF(0f, 65f, View.Frame.Width, 50));
//			Add(vw);
			_scrollView = new UIScrollView(new RectangleF(0, 105, View.Frame.Width, 
				View.Frame.Height - NavigationController.NavigationBar.Frame.Height - 105));
			_scrollView.ContentSize = new SizeF(320f, 600f);
			_uiVc = new UIViewController();

			for (int i = 0; i < 8; ++i)
			{
// TODO Make some of these static, because they don't change???
				switch (i)
				{
				case 0:
					SetStuff<ColorCell, colorsV>(SetColorKey, 0, WhichCollectionView.ColorCv);
					break;
				case 1:
					SetStuff<FlowerShapeCell, flowerShapeV>(SetFlowerShapeKey, 70, WhichCollectionView.FlowerShapeCv);
					break;
				case 2:
					SetStuff<HabitatCell, habitatV>(SetHabitatKey, 140, WhichCollectionView.HabitatCv);
					break;
				case 3:
					SetStuff<HeightCell, heightV>(SetHeightKey, 210, WhichCollectionView.HeightCv);
					break;
				case 4:
					SetStuff<LeafArrangementCell, leafArrangementV>(SetLeafArrangementKey, 280, WhichCollectionView.LeafArrangementCv);
					break;
				case 5:
					SetStuff<LeafMarginCell, leafMarginV>(SetLeafMarginKey, 350, WhichCollectionView.LeafMarginCv);
					break;
				case 6:
					SetStuff<LeafShapeCell, leafShapeV>(SetLeafShapeKey, 420, WhichCollectionView.LeafShapeCv);
					break;
				case 7:
					SetStuff<LeafVenationCell, leafVenationV>(SetLeafVenationKey, 490, WhichCollectionView.LeafVenationCv);
					break;
//				case 8:
//					SetStuff<Leaf_stemTextureCell, leaf_stemTextureV>(SetLeaf_stemTextureKey, 340);
//					break;
				default:
					throw new NotImplementedException();
				}
			}


			AddChildViewController(_uiVc);
			_uiVc.DidMoveToParentViewController(this);
			Add(_scrollView);

//			var textField = new UITextField(new RectangleF(0, 50, 300, 40));
//			Add(textField);


			var set = this.CreateBindingSet<SearchView, CollectABull.Core.SearchViewModel>();
			// to remove the need for `For("N28")` see Setup.FillBindingNames
//			set.Bind(binaryEdit).For("N28").To(vm => vm.Counter);
			set.Bind(lblNumFound).To(vm => vm.NumFound);
			// to remove the need for `For(be => be.MyCount)` see Setup.FillBindingNames
//			set.Bind(nicerBinaryEdit).For(be => be.MyCount).To(vm => vm.Counter);
			set.Bind(this).For(cv => cv.ColorKey).To(vm => vm.Color);
			set.Bind(this).For(cv => cv.FlowerShapeKey).To(vm => vm.FlowerShape);
			set.Bind(this).For(cv => cv.HabitatKey).To(vm => vm.Habitat);
			set.Bind(this).For(cv => cv.HeightKey).To(vm => vm.Height);
			set.Bind(this).For(cv => cv.LeafArrangementKey).To(vm => vm.LeafArrangement);
			set.Bind(this).For(cv => cv.LeafMarginKey).To(vm => vm.LeafMargin);
			set.Bind(this).For(cv => cv.LeafShapeKey).To(vm => vm.LeafShape);
			set.Bind(this).For(cv => cv.LeafVenationKey).To(vm => vm.LeafVenation);
			set.Apply();

			btnClear.TouchUpInside += (o, s) =>
			{
				ClearSetting<ColorCell, colorsV>(WhichCollectionView.ColorCv);
				ClearSetting<FlowerShapeCell, flowerShapeV>(WhichCollectionView.FlowerShapeCv);
				ClearSetting<HabitatCell, habitatV>(WhichCollectionView.HabitatCv);
				ClearSetting<HeightCell, heightV>(WhichCollectionView.HeightCv);
				ClearSetting<LeafArrangementCell, leafArrangementV>(WhichCollectionView.LeafArrangementCv);
				ClearSetting<LeafMarginCell, leafMarginV>(WhichCollectionView.LeafMarginCv);
				ClearSetting<LeafShapeCell, leafShapeV>(WhichCollectionView.LeafShapeCv);
				ClearSetting<LeafVenationCell, leafVenationV>(WhichCollectionView.LeafVenationCv);
			};
		}

		void ClearSetting<C, T>(WhichCollectionView which) where C : MnpsCell
		{
			var cv1 = 
				(MyCollectionView<C>)_scrollView.ViewWithTag((int)which);
			Debug.Assert(cv1 != null);
			NSIndexPath[] p = cv1.GetIndexPathsForSelectedItems();
			foreach (NSIndexPath ip in p)
			{
				cv1.DeselectItem(ip, false);
				var del = (MyDelegate<C, T>)cv1.Delegate;
				Debug.Assert(del != null);
				del.Clear();
			}
		}

		public void SetColorKey(int? i) {ColorKey = i;}
		public event EventHandler ColorKeyChanged;
		int? _colorKey;
		public int? ColorKey
		{
			get { return _colorKey; }
			set { _colorKey = value; UpdateColorKey();}
		}
		private void UpdateColorKey()
		{
			var handler = ColorKeyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public void SetHabitatKey(int? i) {HabitatKey = i;}
		public event EventHandler HabitatKeyChanged;
		int? _habitatKey;
		public int? HabitatKey
		{
			get { return _habitatKey; }
			set { _habitatKey = value; UpdateHabitatKey();}
		}
		private void UpdateHabitatKey()
		{
			var handler = HabitatKeyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public void SetHeightKey(int? i) {HeightKey = i;}
		public event EventHandler HeightKeyChanged;
		int? _heightKey;
		public int? HeightKey
		{
			get { return _heightKey; }
			set { _heightKey = value; UpdateHeightKey();}
		}
		private void UpdateHeightKey()
		{
			var handler = HeightKeyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public void SetFlowerShapeKey(int? i) {FlowerShapeKey = i;}
		public event EventHandler FlowerShapeKeyChanged;
		int? _flowerShapeKey;
		public int? FlowerShapeKey
		{
			get { return _flowerShapeKey; }
			set { _flowerShapeKey = value; UpdateFlowerShapeKey();}
		}
		private void UpdateFlowerShapeKey()
		{
			var handler = FlowerShapeKeyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public void SetLeafArrangementKey(int? i) {LeafArrangementKey = i;}
		public event EventHandler LeafArrangementKeyChanged;
		int? _leafArrangementKey;
		public int? LeafArrangementKey
		{
			get { return _leafArrangementKey; }
			set { _leafArrangementKey = value; UpdateLeafArrangementKey();}
		}
		private void UpdateLeafArrangementKey()
		{
			var handler = LeafArrangementKeyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public void SetLeafMarginKey(int? i) {LeafMarginKey = i;}
		public event EventHandler LeafMarginKeyChanged;
		int? _leafMarginKey;
		public int? LeafMarginKey
		{
			get { return _leafMarginKey; }
			set { _leafMarginKey = value; UpdateLeafMarginKey();}
		}
		private void UpdateLeafMarginKey()
		{
			var handler = LeafMarginKeyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public void SetLeafShapeKey(int? i) {LeafShapeKey = i;}
		public event EventHandler LeafShapeKeyChanged;
		int? _leafShapeKey;
		public int? LeafShapeKey
		{
			get { return _leafShapeKey; }
			set { _leafShapeKey = value; UpdateLeafShapeKey();}
		}
		private void UpdateLeafShapeKey()
		{
			var handler = LeafShapeKeyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public void SetLeafVenationKey(int? i) {LeafVenationKey = i;}
		public event EventHandler LeafVenationKeyChanged;
		int? _leafVenationKey;
		public int? LeafVenationKey
		{
			get { return _leafVenationKey; }
			set { _leafVenationKey = value; UpdateLeafVenationKey();}
		}
		private void UpdateLeafVenationKey()
		{
			var handler = LeafVenationKeyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}
	}
}
