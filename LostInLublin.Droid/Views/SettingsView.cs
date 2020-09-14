using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LostInLublin.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Binding.BindingContext;
using Firebase.Iid;
using Android.Preferences;

namespace LostInLublin.Droid.Views
{
    [MvxActivityPresentation]
    [Activity]
    class SettingsView : MvxAppCompatActivity<SettingsViewModel>
    {
        CheckBox notification;
        private FirebaseInstanceIdService _notificationHub;
        public SettingsView()
        {

        }

        public SettingsView(FirebaseInstanceIdService notificationHub)
        {
            _notificationHub = notificationHub;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.settingsLayout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            notification = FindViewById<CheckBox>(Resource.Id.cbxNotification);



            SetBindings();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.MenuInflater.Inflate(Resource.Menu.add_meu, menu);
            var saveItem = menu.FindItem(Resource.Id.action_save);



            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_save:

                    // _notificationHub.UnregisterReceiver();
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                    string id  = prefs.GetString("reg_id",null);

    
                    this.ViewModel.SaveCmd.Execute(id);
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        private void SetBindings()
        {
            var bindingSet = this.CreateBindingSet<SettingsView, SettingsViewModel>();

            bindingSet.Bind(notification)
                .For(v => v.Checked)
                .To(vm => vm.NotificationOn);

            bindingSet.Apply();

        }
    }
}