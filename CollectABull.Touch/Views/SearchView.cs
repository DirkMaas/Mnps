
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

namespace CollectABull.Touch
{
	public partial class SearchView : MvxViewController
	{
//		LineLayout lineLayout;
		UIScrollView _scrollView;

		public SearchView() : base("SearchView", null)
		{
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		void SetStuff<C, T>(UIViewController uiVc, Action<int?> SetKey, float y, float h=70) where C : MnpsCell // and T is a table class
		{
			var lineLayout = new LineLayout();
			var frame = new RectangleF(0f, y, 320f, h);
			var cccv = new MyCollectionView<C>(frame, lineLayout, false);
			cccv.Delegate = new MyDelegate<C,T>(this, SetKey);
			cccv.DataSource = new MyDataSource2<C,T>();
			cccv.RegisterClassForCell(typeof(C), new NSString(typeof(C).Name));
			uiVc.Add(cccv);
//			Add(cccv);
			_scrollView.AddSubview(cccv);
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
//			var uiVc = new MyViewController2(/*cccv, typeof(ColorCell)*/);
			_scrollView = new UIScrollView(new RectangleF(0, 0, View.Frame.Width, 
				View.Frame.Height - NavigationController.NavigationBar.Frame.Height));
			_scrollView.ContentSize = new SizeF(320f, 600f);
			var uiVc = new UIViewController();
//			float y = 0, h = 130;

			for (int i = 0; i < 8; ++i)
			{
// TODO Make some of these static, because they don't change???
				switch (i)
				{
				case 0:
					SetStuff<ColorCell, colorsV>(uiVc, SetColorKey, 0/*, 130f*/);
					break;
				case 1:
					SetStuff<FlowerShapeCell, flowerShapeV>(uiVc, SetFlowerShapeKey, 70);
					break;
				case 2:
					SetStuff<HabitatCell, habitatV>(uiVc, SetHabitatKey, 140);
					break;
				case 3:
					SetStuff<HeightCell, heightV>(uiVc, SetHeightKey, 210);
					break;
				case 4:
					SetStuff<LeafArrangementCell, leafArrangementV>(uiVc, SetLeafArrangementKey, 280);
					break;
				case 5:
					SetStuff<LeafMarginCell, leafMarginV>(uiVc, SetLeafMarginKey, 350);
					break;
				case 6:
					SetStuff<LeafShapeCell, leafShapeV>(uiVc, SetLeafShapeKey, 420);
					break;
				case 7:
					SetStuff<LeafVenationCell, leafVenationV>(uiVc, SetLeafVenationKey, 490);
					break;
//				case 8:
//					SetStuff<Leaf_stemTextureCell, leaf_stemTextureV>(uiVc, SetLeaf_stemTextureKey, 340);
//					break;
				default:
					throw new NotImplementedException();
				}
			}


#if false

			LineLayout lineLayout2 = new LineLayout()
			{
				ScrollDirection = UICollectionViewScrollDirection.Horizontal
			};
			var frame2 = new RectangleF(0f, 130, 320f, 70f);
			var fsccv = new MyCollectionView<FlowerShapeCell>(frame2, lineLayout2, false);
//			var uivc2 = new MyViewController<FlowerShapeCell>(uicv2);
			fsccv.Delegate = new MyDelegate<FlowerShapeCell, flowerShapeV>(this);
			fsccv.DataSource = new MyDataSource2<FlowerShapeCell, flowerShapeV>();
			fsccv.Tag = 2;
			fsccv.RegisterClassForCell(typeof(FlowerShapeCell), new NSString(typeof(FlowerShapeCell).Name));
			uiVc.Add(fsccv);
			Add(fsccv);
//			AddChildViewController(uivc2);
#endif


//			UIApplication app = UIApplication.SharedApplication;
//			var sel = new MonoTouch.ObjCRuntime.Selector("selector");
//			app.PerformSelector(sel, null, 0.0);
#if true
			AddChildViewController(uiVc);
			uiVc.DidMoveToParentViewController(this);
			Add(_scrollView);
#else
			// this crashes with the message: Object reference not set to an instance of an object
			uicv.Window.RootViewController=this;
			// This produces a black screen and the message:
			// Unbalanced calls to begin/end appearance transitions for <HomeView: 0x13461400>.
			//			UIApplication.SharedApplication.Windows[0].RootViewController = this;
			// - (void)performSelector:(SEL)aSelector withObject:(id)anArgument afterDelay:(NSTimeInterval)delay
#endif


//			var textField = new UITextField(new RectangleF(0, 300, 300, 40));
//			Add(textField);


			var set = this.CreateBindingSet<SearchView, CollectABull.Core.SearchViewModel>();
			// to remove the need for `For("N28")` see Setup.FillBindingNames
//			set.Bind(binaryEdit).For("N28").To(vm => vm.Counter);
//			set.Bind(textField).To(vm => vm.FlowerShape);
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
//			this.CreateBinding(uivc).For(cll => cll.DataContext).To((SearchViewModel vm) => vm).TwoWay().Apply();


//			window.RootViewController = uivc;
//			window.MakeKeyAndVisible();

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
