using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace CollectABull.Droid.Views
{
    [Activity(Label = "Home")]
    public class HomeView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
			if (System.IO.File.Exists("hello"))
				;
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeView);
        }
    }
}