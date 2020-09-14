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
        Button locationBtn;
        EditText locationTxt;
        EditText keyWordTxt;
        TextView startDate;
        TextView endDate;

        Geocoder geocoder;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
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
            SetContentView(Resource.Layout.startLayout);


            geocoder = new Geocoder(this.ApplicationContext);
            try
            {
            var addresses = geocoder.GetFromLocation(ViewModel.Lat, ViewModel.Long, 1);

            if (addresses.Count > 0)
            {
               ViewModel.Location= addresses[0].Locality;
            }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            locationTxt = FindViewById<EditText>(Resource.Id.locationBx);
            keyWordTxt = FindViewById<EditText>(Resource.Id.keywordBx);
            startDate = FindViewById<TextView>(Resource.Id.txt_from);
            endDate = FindViewById<TextView>(Resource.Id.txt_to);
            searchBtn = FindViewById<Button>(Resource.Id.searchBtn);
            locationBtn = FindViewById<Button>(Resource.Id.location_btn);

            startDate.Click += (s, e) =>
             {
                 DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                 {
                     startDate.Text = time.ToShortDateString();
                     
                 });
                 frag.Show(FragmentManager, DatePickerFragment.TAG);
             };

            endDate.Click += (s, e) =>
            {
                {
                    DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                    {
                        endDate.Text = time.ToShortDateString();
                    });
                    //if (!string.IsNullOrEmpty(endDate.Text))
                    //{
                    //    frag. = DateTime.Parse(endDate.Text);
                    //}
                    //else
                    //{
                    //    frag.minDate = DateTime.Now.Date;
                    //}
                    frag.Show(FragmentManager, DatePickerFragment.TAG);

                };
            };

            SetBindings();

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

            bindingSet.Bind(startDate)
                .For(v => v.Text)
                .To(vm => vm.StartDate)
                .TwoWay();

            bindingSet.Bind(endDate)
                .For(v => v.Text)
                .To(vm => vm.EndDate)
                .TwoWay();

            bindingSet.Bind(locationBtn)
                .For(nameof(View.Click))
                .To(vm => vm.SetLocationCmd);

            bindingSet.Bind(locationTxt)
                .For(v => v.Text)
                .To(vm => vm.Location)
                .Mode(MvvmCross.Binding.MvxBindingMode.TwoWay);

            bindingSet.Bind(keyWordTxt)
                .For(v => v.Text)
                .To(vm => vm.KeyWord)
                .Mode(MvvmCross.Binding.MvxBindingMode.TwoWay);

            bindingSet.Apply();
        }
    }

}