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

namespace LostInLublin.Droid.Views
{
    [MvxActivityPresentation]
    [Activity]
    class DetailView:MvxAppCompatActivity<DetailViewModel>
    {

        Button btnLink;
        TextView txtCreated;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.detailLayout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);
            btnLink = FindViewById<Button>(Resource.Id.btnLink);
            txtCreated = FindViewById<TextView>(Resource.Id.txt_created);

            SetBinging();
           // SupportActionBar.SetDisplayHomeAsUpEnabled(true);
           // SupportActionBar.SetDisplayShowHomeEnabled(true);
        }

        private void SetBinging()
        {
            var bindingSet = this.CreateBindingSet<DetailView, DetailViewModel>();

            bindingSet.Bind(btnLink)
                .For(nameof(View.Click))
                .To(vm => vm.OpenLinkCmd);

            bindingSet.Bind(txtCreated)
                .For(v => v.Text)
                .To(vm => vm.Created);

            bindingSet.Apply();
        }
    }
}