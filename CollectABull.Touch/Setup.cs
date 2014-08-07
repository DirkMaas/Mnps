using MonoTouch.UIKit;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using CollectABull.Core.Services.DataStore;
using System.IO;
using MonoTouch.Foundation;
using System;
using Cirrious.CrossCore;
using CollectABull.Core;
using SimpleCollectionView;

namespace CollectABull.Touch
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		}

		protected override IMvxApplication CreateApp ()
		{
			return new Core.App();
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
		protected override System.Collections.Generic.List<System.Reflection.Assembly> ValueConverterAssemblies
		{
			get 
			{
				var toReturn = base.ValueConverterAssemblies;
				toReturn.Add(typeof(Cirrious.MvvmCross.Plugins.PictureChooser.Touch.MvxInMemoryImageValueConverter).Assembly);
				return toReturn;
			}
		}
		protected override void InitializeLastChance()
		{
			base.InitializeLastChance();
			Mvx.RegisterSingleton<IDbLocation>(new TouchDbLocation());
			Mvx.RegisterSingleton<IDebugLog>(new TouchDebugLog());
		}
//		protected override void FillBindingNames(Cirrious.MvvmCross.Binding.BindingContext.IMvxBindingNameRegistry registry)
//		{
//			// use these to register default binding names
//			registry.AddOrOverwrite<MyDelegate<AnimalCell>>(be => be.DbKey);
//			//registry.AddOrOverwrite(typeof(BinaryEdit),"N28Doofus");
//			base.FillBindingNames(registry);
//		}
	}
}