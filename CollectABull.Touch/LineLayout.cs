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

	public class MyDataSource2<C, T> : UICollectionViewDataSource where C : MnpsCell // and T is a table name
	{
		static NSString cellId = new NSString(typeof(C).Name);
		List<TouchCellData> animals;
		public MyDataSource2()
		{
			animals = new List<TouchCellData>();
			var repo = Mvx.Resolve<ITableVSearchCell>();
			List<ITableVSearchCellData> _colors = repo.GetVTable<T>();
			foreach (ITableVSearchCellData c in _colors)
				animals.Add(new TouchCellData(c.key, c.descrip, c.graphic));
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

		public LineLayout()
		{	
			HeaderReferenceSize = new System.Drawing.SizeF(0, 0);
			ItemSize = new SizeF(ITEM_SIZE, ITEM_SIZE);
			MinimumLineSpacing = 0.0f; // this controls the horizontal distance between cells
			ScrollDirection = UICollectionViewScrollDirection.Horizontal;
			SectionInset = new UIEdgeInsets(0, 0, 0, 0);
//			FooterReferenceSize = new System.Drawing.SizeF (0, 0),
//			MinimumInteritemSpacing
		}
	}


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
	}

	public class MyDelegate<C, T> : UICollectionViewDelegate where C : MnpsCell
	{
		Action<int?> SetKey;
		public MyDelegate(Action<int?> sk)
		{
			SetKey = sk;
		}

		public void Clear()
		{
			SetKey(null);
		}

		public override bool ShouldSelectItem(UICollectionView collectionView, NSIndexPath indexPath)
		{
//			Console.WriteLine("ShouldSelectItem {0}", indexPath);
			var cell = (C)collectionView.CellForItem(indexPath);
			if (cell.Selected) // needed if collectionView.AllowsMultipleSelection = false
			{
//				Console.WriteLine("cell.Selected = false");
//				cell.Selected = false; seems to have no effect
				collectionView.DeselectItem(indexPath, true);
				SetKey(null);
				return false;
			}
			return true;
		}

		// only called when collectionView.AllowsMultipleSelection = true
		public override bool ShouldDeselectItem(UICollectionView collectionView, NSIndexPath indexPath)
		{
//			Console.WriteLine("ShouldDeselectItem  {0}", indexPath);
			return true; // true is the default if the function is not implemented
		}


		public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
		{
//			Console.WriteLine("ItemSelected  {0}", indexPath);
			var ds = collectionView.DataSource as MyDataSource2<C, T>;
			SetKey(ds.GetDbKey(indexPath.Row));

//			this is how to get from a cell to a db key
//			NSIndexPath[] ip = collectionView.GetIndexPathsForSelectedItems();
//			foreach (NSIndexPath path in ip)
//				Console.WriteLine("path  {0}", path);

		}
		public override void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath)
		{
//			Console.WriteLine("ItemDeselected  {0}", indexPath);
		}
	}


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

