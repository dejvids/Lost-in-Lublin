using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content.PM;
using Android.OS;
using LostInLublin.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;


namespace LostInLublin.Droid.Views
{
    [MvxActivityPresentation]
    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden, MainLauncher = true)]
    public class RootView : MvxAppCompatActivity<RootViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.mainLayout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        private void Initialize()
        {
        }
    }
}