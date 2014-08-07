using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.CrossCore.IoC;
using CollectABull.Core.Services.DataStore;
using CollectABull.Touch;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using CollectABull.Core;
using Cirrious.MvvmCross.ViewModels;

namespace SimpleCollectionView
{
#if false
	public class MyDataSource<T> : UICollectionViewDataSource where T : MnpsCell
	{
//		static NSString animalCellId = new NSString("AnimalCell");
		static NSString cellId = new NSString(typeof(T).Name);
//		List<IAnimal> animals;
		List<ITableVSearchCellData> _colors;
//		T _t;

		public MyDataSource()
		{
//			animals = new List<IAnimal>();
			var repo = Mvx.Resolve<Repository3>();
			_colors = repo.Colors;
//			foreach (colorsV c in colors)
//				animals.Add(new Color(c.key, c.color));

			// TODO add our data
//			for (int i = 0; i < 20; i++)
//				animals.Add(new Monkey());
		}
		public int? GetDbKey(int i)
		{
			return _colors[i].key;
		}

		public override int NumberOfSections(UICollectionView collectionView)
		{
			return 1;
		}
		public override int GetItemsCount(UICollectionView collectionView, int section)
		{
			return _colors.Count;
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = (T)collectionView.DequeueReusableCell(cellId, indexPath);

			var animal = animals[indexPath.Row];

			cell.Image = animal.Image;
//			animalCell.key = animal.key; here's how to save the db key

			return cell;
		}
	}
#endif

	public class MyDataSource2<C, T> : UICollectionViewDataSource where C : MnpsCell // and T is a table name
	{
//		static NSString animalCellId = new NSString("AnimalCell");
		static NSString cellId = new NSString(typeof(C).Name);
		List<TouchCellData> animals;
//		working here
		public MyDataSource2()
		{
			animals = new List<TouchCellData>();
			var repo = Mvx.Resolve<ITableVSearchCell>();
			List<ITableVSearchCellData> _colors = repo.GetVTable<T>();
			foreach (ITableVSearchCellData c in _colors)
				animals.Add(new TouchCellData(c.key, c.descrip, c.graphic));
//				animals.Add(new Monkey());

			// TODO add our data
			//			for (int i = 0; i < 20; i++)
			//				animals.Add(new Monkey());
		}
		public int? GetDbKey(int i)
		{
			return animals[i].key;
		}

		public override int NumberOfSections(UICollectionView collectionView)
		{
			return 1;
		}
		public override int GetItemsCount(UICollectionView collectionView, int section)
		{
			return animals.Count;
		}

		public override UICollectionViewCell GetCell(UICollectionView collectionView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			var cell = (C)collectionView.DequeueReusableCell(cellId, indexPath);

			cell.Image = UIImage.FromBundle(animals[indexPath.Row].graphic);

			return cell;
		}
	}


	public class LineLayout : UICollectionViewFlowLayout
	{
		public const float ITEM_SIZE = 50.0f;
//		public const int ACTIVE_DISTANCE = 200;
//		public const float ZOOM_FACTOR = 0.3f;
		
		public LineLayout()
		{	
			HeaderReferenceSize = new System.Drawing.SizeF(0, 0);
			ItemSize = new SizeF(ITEM_SIZE, ITEM_SIZE);
			MinimumLineSpacing = 0.0f; // this controls the horizontal distance between cells
			ScrollDirection = UICollectionViewScrollDirection.Horizontal;
			SectionInset = new UIEdgeInsets(0, 0, 0, 0);
//			FooterReferenceSize = new System.Drawing.SizeF (0, 0),
//			MinimumInteritemSpacing
//			this.supp
		}
// This is used in this case to ensure the code for transformation applied to the centermost cell will be 
// applied during scrolling
//		public override bool ShouldInvalidateLayoutForBoundsChange(RectangleF newBounds)
//		{
//			return true;
//		}


//		this is for zooming the middle item
//		public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(RectangleF rect)
//		{
//			var array = base.LayoutAttributesForElementsInRect(rect);
//			var visibleRect = new RectangleF(CollectionView.ContentOffset, CollectionView.Bounds.Size);
//						
//			foreach (var attributes in array)
//			{
//				if (attributes.Frame.IntersectsWith(rect))
//				{
//					float distance = visibleRect.GetMidX() - attributes.Center.X;
//					float normalizedDistance = distance / ACTIVE_DISTANCE;
//					if (Math.Abs(distance) < ACTIVE_DISTANCE)
//					{
//						float zoom = 1 + ZOOM_FACTOR * (1 - Math.Abs(normalizedDistance));
//						attributes.Transform3D = CATransform3D.MakeScale(zoom, zoom, 1.0f);
//						attributes.ZIndex = 1;											
//					}
//				}
//			}
//			return array;
//		}

//		makes the centermost cell snap to the center of the UICollectionView as scrolling stops
//		public override PointF TargetContentOffset (PointF proposedContentOffset, PointF scrollingVelocity)
//		{
//			float offSetAdjustment = float.MaxValue;
//			float horizontalCenter = (float)(proposedContentOffset.X + (this.CollectionView.Bounds.Size.Width / 2.0));
//			RectangleF targetRect = new RectangleF(proposedContentOffset.X, 0.0f, this.CollectionView.Bounds.Size.Width, this.CollectionView.Bounds.Size.Height);
//			var array = base.LayoutAttributesForElementsInRect(targetRect);
//			foreach (var layoutAttributes in array)
//			{
//				float itemHorizontalCenter = layoutAttributes.Center.X;
//				if (Math.Abs(itemHorizontalCenter - horizontalCenter) < Math.Abs(offSetAdjustment))
//					offSetAdjustment = itemHorizontalCenter - horizontalCenter;
//			}
//            return new PointF(proposedContentOffset.X + offSetAdjustment, proposedContentOffset.Y);
//		}
	}

#if false
//	[MvxViewFor(typeof(SearchViewModel))]
	public class MyViewController<T> : UIViewController //MvxViewController
	{
		static NSString cellId = new NSString(typeof(T).Name);
			//new NSString("AnimalCell");
		private UICollectionView _cv;
		public MyViewController(UICollectionView cv)
		{
			_cv = cv;
			// not sure these 2 lines do anything
			AutomaticallyAdjustsScrollViewInsets = true;
//			EdgesForExtendedLayout = UIRectEdge.None;
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_cv.RegisterClassForCell(typeof(T), cellId);
		}
	}

	// MyViewController2 may not be necessary anymore
	public class MyViewController2 : UIViewController //MvxViewController
	{
//		static NSString cellId = new NSString(typeof(T).Name);
		NSString cellId2;
		Type _type;
		//new NSString("AnimalCell");
//		private UICollectionView _cv;
		public MyViewController2(/*UICollectionView cv, Type t*/)
		{
//			_cv = cv;
//			_type = t;
//			cellId2 = new NSString(_type.Name);
			// not sure these 2 lines do anything
			AutomaticallyAdjustsScrollViewInsets = true;
			//			EdgesForExtendedLayout = UIRectEdge.None;
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
/*
			Type t;
			switch (_cv.Tag)
			{
			case 1:
				t = typeof(ColorCell);
//				id = new NSString(typeof(ColorCell).Name);
				break;
			case 2:
				t = typeof(FlowerShapeCell);
//				id = new NSString(typeof(FlowerShapeCell).Name);
				break;
			default:
				throw new NotImplementedException();
			}
//			NSString id = new NSString(t.Name);
*/

//			_cv.RegisterClassForCell(typeof(ColorCell), new NSString(typeof(ColorCell).Name));
//			_cv.RegisterClassForCell(typeof(FlowerShapeCell), new NSString(typeof(FlowerShapeCell).Name));
		}
	}
#endif

	public class MyCollectionView<T> : UICollectionView // MvxCollectionView does not exist
	{
		public MyCollectionView(RectangleF f, LineLayout ll, bool ms) : base(f, ll)
		{
			AllowsMultipleSelection = ms;
			ContentInset = new UIEdgeInsets(0f, 0f, 0f, 0f);
			ContentOffset = new PointF(0f, -30f); // changing this has no effect
			ContentSize = new SizeF(300f, 50f);
			BackgroundColor = UIColor.White;
		}
//		private MyViewController<T> _vc;
//		public MyViewController<T> vc
//		{
//			get { return _vc; }
//			set { _vc = value; }
//		}
	}

	public class MyDelegate<C, T> : UICollectionViewDelegate where C : MnpsCell
	{
		SearchView _vc;
//		delegate void SetKey(int? i);
//		int? _key;
		Action<int?> SetKey;
		public MyDelegate(SearchView sv, Action<int?> sk)
		{
			_vc = sv;
//			_key = k;
			SetKey = sk;
		}

//		int? _dbKey;
//		public int? DbKey
//		{
//			get { return _dbKey; }
//			set { _dbKey = value; }
//		}

		public override bool ShouldSelectItem(UICollectionView collectionView, NSIndexPath indexPath)
		{
			Console.WriteLine("ShouldSelectItem {0}", indexPath);
			var cell = (C)collectionView.CellForItem(indexPath);
			if (cell.Selected) // needed if collectionView.AllowsMultipleSelection = false
			{
				Console.WriteLine("cell.Selected = false");
//				cell.Selected = false; seems to have no effect
				collectionView.DeselectItem(indexPath, true);
//				_vc.ColorKey = null;
//				_key = null;
				SetKey(null);
				return false;
			}
			return true;
		}

		// only called when collectionView.AllowsMultipleSelection = true
		public override bool ShouldDeselectItem(UICollectionView collectionView, NSIndexPath indexPath)
		{
			Console.WriteLine("ShouldDeselectItem  {0}", indexPath);
			return true;
		}


		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
			Console.WriteLine("ItemSelected  {0}", indexPath);
			var ds = collectionView.DataSource as MyDataSource2<C, T>;
//			DbKey = ds.GetDbKey(indexPath.Row);
//			var cv = collectionView as MyCollectionView<C>;
//			cv.vc.DbKey = ds.GetDbKey(indexPath.Row);
//			_vc.ColorKey = ds.GetDbKey(indexPath.Row);
//			_key = ds.GetDbKey(indexPath.Row);
			SetKey(ds.GetDbKey(indexPath.Row));

//			if (_vc.ColorKeyChanged != null) 
//				_vc.ColorKeyChanged(this, EventArgs.Empty);


//			this is how to get from a cell to a db key
			NSIndexPath[] ip = collectionView.GetIndexPathsForSelectedItems();
			foreach (NSIndexPath path in ip)
			{
				Console.WriteLine("path  {0}", path);
//				var dbkey = collectionView.DataSource.animals[indexPath.Row].key;
			}

		}
		public override void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath)
		{
//			DbKey = null;
			Console.WriteLine("ItemDeselected  {0}", indexPath);
		}
	}




//	public interface IAnimal
//	{
//		int DbKey { get; }
//
//		UIImage Image { get; }
//	}

	public class TouchCellData : ITableVSearchCellData
	{
		#region ITableVSearchCellData implementation

		public int key
		{ private set; get; }

		public string descrip
		{ private set; get; }

		public string graphic
		{ private set; get; }

		#endregion

		public TouchCellData(int k, string d, string g)
		{
			key = k;
			descrip = d;
			graphic = g;
		}

		public UIImage Image
		{get { return UIImage.FromBundle(graphic); } }
	}

	public class Monkey : TouchCellData
	{
		public Monkey() : base(1, "monkey", "monkey.png")
		{
		}
	}

#if false
	public class Color : IAnimal
	{
		string _color;
		public Color(int k, string c)
		{
			DbKey = k;
			_color = c;
		}

		int _dbKey;
		public int DbKey
		{
			private set { _dbKey = value; }
			get { return _dbKey; }
		}
		public UIImage Image
		{
			get
			{
				return UIImage.FromBundle(_color + ".png");
			}
		}
	}
	// bell, funnel, trumpet
	public class FlowerShape : IAnimal
	{
		string _shape;
		public FlowerShape(int k, string c)
		{
			DbKey = k;
			_shape = c;
		}

		int _dbKey;
		public int DbKey
		{
			private set { _dbKey = value; }
			get { return _dbKey; }
		}
		public UIImage Image
		{
			get
			{
				return UIImage.FromBundle(_shape + ".png");
			}
		}
	}
#endif
	public abstract class MnpsCell : /*MvxCollectionViewCell*/ UICollectionViewCell
	{
		protected UIImageView imageView;
		public MnpsCell(System.Drawing.RectangleF frame) : base(frame) 
		{
			// give the BackgroundView a black border, but for color items, make the BackgroundColor the same color; 
			// for other items make the BackgroundColor white.  Make the SelectedBackgroundView color black, so
			// when selected, the black border will get bigger.

			// equivalent to new UIView() {BackgroundColor = UIColor.White}; uses object initializer
			BackgroundView = new UIView{BackgroundColor = UIColor.White};
			BackgroundView.Layer.BorderWidth = 1.0f;
			BackgroundView.Layer.BorderColor = UIColor.Black.CGColor;

			SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Black};

//			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
//			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.White;
//			var c = new UIColor(red, green, blue, alpha);
			ContentView.Transform = CGAffineTransform.MakeScale(0.9f, 0.9f);

			imageView = new UIImageView(UIImage.FromBundle("placeholder.png"));
			imageView.Center = ContentView.Center;
			imageView.Transform = CGAffineTransform.MakeScale(1.2f, 1.0f);

			ContentView.AddSubview(imageView);

		}
		public UIImage Image
		{set{imageView.Image = value;}}
	}
	// TODO do we really need these classes that differ in name only?
	public class ColorCell : MnpsCell
	{
		[Export ("initWithFrame:")]
		public ColorCell(System.Drawing.RectangleF frame) : base(frame) {}
	}

	public class FlowerShapeCell : MnpsCell
	{
		[Export ("initWithFrame:")]
		public FlowerShapeCell(System.Drawing.RectangleF frame) : base(frame) {}
	}

	public class HabitatCell : MnpsCell
	{
		[Export ("initWithFrame:")]
		public HabitatCell(System.Drawing.RectangleF frame) : base(frame) {}
	}

	public class HeightCell : MnpsCell
	{
		[Export ("initWithFrame:")]
		public HeightCell(System.Drawing.RectangleF frame) : base(frame) {}
	}

	public class LeafShapeCell : MnpsCell
	{
		[Export ("initWithFrame:")]
		public LeafShapeCell(System.Drawing.RectangleF frame) : base(frame) {}
	}

	public class LeafArrangementCell : MnpsCell
	{
		[Export ("initWithFrame:")]
		public LeafArrangementCell(System.Drawing.RectangleF frame) : base(frame) {}
	}

	public class LeafMarginCell : MnpsCell
	{
		[Export ("initWithFrame:")]
		public LeafMarginCell(System.Drawing.RectangleF frame) : base(frame) {}
	}

	public class LeafVenationCell : MnpsCell
	{
		[Export ("initWithFrame:")]
		public LeafVenationCell(System.Drawing.RectangleF frame) : base(frame) {}
	}
}

