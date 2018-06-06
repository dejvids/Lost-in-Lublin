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
using MvvmCross.Binding.BindingContext;
using MvvmCross.Plugin.PictureChooser.Platforms.Android;
using Android.Support.V4.App;
using Android;

namespace LostInLublin.Droid.Views
{
    [Activity]
    public class AddView: MvxAppCompatActivity<AddViewModel>
    {
        EditText messageText;

        LinearLayout takePicutre;
        ImageView picture;
        TextView deleteBtn;
        TextView daysSelector;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.addLayout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            messageText = FindViewById<EditText>(Resource.Id.messageEditText);
            takePicutre = FindViewById<LinearLayout>(Resource.Id.take_photo);
            //deleteBtn = FindViewById<TextView>(Resource.Id.deleteBtn);

            picture = FindViewById<ImageView>(Resource.Id.photo);
            
            ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage }, 0);

            SetBinding();
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
                    if(ViewModel.AddCmd.CanExecute(null))
                    this.ViewModel.AddCmd.Execute();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        private void SetBinding()
        {
            var bindingSet = this.CreateBindingSet<AddView,AddViewModel>();

            bindingSet.Bind(messageText)
                .For(v => v.Text)
                .Mode(MvvmCross.Binding.MvxBindingMode.TwoWay)
                .To(vm => vm.Message);

            bindingSet.Bind(takePicutre)
           .For(nameof(View.Click))
           .To(vm => vm.TakePhotoCommand);

            bindingSet.Bind(picture)
                      .To(x => x.Picture)
                      .For("Bitmap")
                      .WithConversion(new MvxInMemoryImageValueConverter());

            bindingSet.Bind(picture)
                      .To(x => x.Picture)
                      .For(v => v.Visibility)
                      .WithConversion(new InlineValueConverter<byte[], ViewStates>((byte[] arg) => arg == null ? ViewStates.Gone : ViewStates.Visible));

            bindingSet.Apply();
        }
    }
}