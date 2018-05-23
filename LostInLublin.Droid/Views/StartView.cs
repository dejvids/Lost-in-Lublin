using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace LostInLublin.Droid.Views
{
    [Activity(MainLauncher = true)]

    public class StartView : MvxAppCompatActivity
    {
        public const string TAG = "MainActivity";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.startLayout);

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

    }

}