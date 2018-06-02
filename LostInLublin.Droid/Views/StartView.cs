using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using LostInLublin.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace LostInLublin.Droid.Views
{
    [Activity]

    public class StartView : MvxAppCompatActivity<StartViewModel>
    {
        public const string TAG = "MainActivity";
        Button searchBtn;
        EditText locationTxt;
        EditText keyWordTxt;
        EditText startDate;
        EditText endDate;

        Geocoder geocoder;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.startLayout);
            geocoder = new Geocoder(this.ApplicationContext);
            var addresses = geocoder.GetFromLocation(ViewModel.Lat, ViewModel.Long, 1);
            if(addresses.Count > 0)
            {
                locationTxt.Text = addresses[0].Locality;
            }

            locationTxt = FindViewById<EditText>(Resource.Id.locationBx);
            keyWordTxt = FindViewById<EditText>(Resource.Id.keywordBx);
            startDate = FindViewById<EditText>(Resource.Id.txt_from);
            endDate = FindViewById<EditText>(Resource.Id.txt_to);
            searchBtn = FindViewById<Button>(Resource.Id.searchBtn);

            startDate.Click += (s, e) =>
             {
                 DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                 {
                     startDate.Text = time.ToLongDateString();
                     this.ViewModel.StartDate = time;
                 });
                 frag.Show(FragmentManager, DatePickerFragment.TAG);
             };

            SetBindings();
            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    if (key != null)
                    {
                        var value = Intent.Extras.GetString(key);
                        Log.Debug(TAG, "Key: {0} Value: {1}", key, value);
                    }
                }
            }
            //if (!GetString(Resource.String.google_app_id).Equals("1:930876947890:android:d9a2b6e7a896707c"))
            //    throw new Exception("invalid json");
            //Task.Run(() =>
            //{
            //    var instanceId = FirebaseInstanceId.Instance;
            //    instanceId.DeleteInstanceId();
            //});
        }

        private void SetBindings()
        {
            var bindingSet = this.CreateBindingSet<StartView, StartViewModel>();

            bindingSet.Bind(searchBtn)
                .For(nameof(View.Click))
                .To(vm => vm.SearchCmd);

            bindingSet.Apply();
        }
    }

}