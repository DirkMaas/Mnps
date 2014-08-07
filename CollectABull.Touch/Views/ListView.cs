using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using CollectABull.Core.ViewModels;

namespace CollectABull.Touch
{
	public partial class ListView : MvxTableViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			// Perform any additional setup after loading the view, typically from a nib.
			var source = new MvxStandardTableViewSource(TableView, "TitleText Caption;ImageUrl ImagePath");
			TableView.Source = source;

			var set = this.CreateBindingSet<ListView, ListViewModel>();
			set.Bind(source).To(vm => vm.Items);
			set.Bind(source).For(s => s.SelectionChangedCommand).To(vmmm => vmmm.ShowDetailCommand);
			set.Apply();
			TableView.ReloadData();
		}
	}
}

