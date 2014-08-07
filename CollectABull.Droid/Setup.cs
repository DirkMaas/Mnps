using Android.Content.Res;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using CollectABull.Core.Services.DataStore;
using System;
using System.IO;
using Cirrious.CrossCore;
using Android.Content;


namespace CollectABull.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
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
				toReturn.Add(typeof(Cirrious.MvvmCross.Plugins.PictureChooser.Droid.MvxInMemoryImageValueConverter).Assembly);
				return toReturn;
			}
		}
		protected override void InitializeLastChance()
		{
			base.InitializeLastChance();
			Mvx.RegisterSingleton<IDbLocation>(new DroidDbLocation());
			Mvx.RegisterSingleton<IDebugLog>(new DroidDebugLog());
		}
	}
}