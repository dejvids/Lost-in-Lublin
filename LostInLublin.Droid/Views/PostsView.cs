using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Views;
using LostInLublin.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.Views;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using Android.Util;


using MvvmCross.Views;
using Android.Content.PM;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Widget;
using Android.Opengl;

namespace LostInLublin.Droid.Views
{
    //[MvxFragmentPresentation(typeof(RootViewModel), Resource.Id.content_frame, true)]
    //[Register(nameof(PostsView))]
    [MvxActivityPresentation]
    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden, MainLauncher = false)]

    public class PostsView : MvxAppCompatActivity<PostsViewModel>
    {
        public const string TAG = "MainActivity";
        MvxListView postsList;
        ProgressBar loader;
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
            SetContentView(Resource.Layout.postsLayout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitleTextColor(Resource.Color.md_white_1000);
            SetSupportActionBar(toolbar);
            var view = this.BindingInflate(Resource.Layout.postsLayout, null);

            loader = FindViewById<ProgressBar>(Resource.Id.pr_bar);
            // postsList = view.FindViewById<MvxListView>(Resource.Id.postsLstv);
            // postsList.ItemTemplateId = Resource.Layout.postItem;
            // postsList.Adapter = new PostsAdapter(this.ApplicationContext, (IMvxAndroidBindingContext)this.BindingContext);
            // postsList.ItemsSource = new List<Post> { new Post { Message = "Znaleziono" }, new Post { Message = "szukaj" } };
            SetBindings();
            LoadData();
            
        }

        private async void LoadData()
        {

            this.ViewModel.GetPosts();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater inflater = this.MenuInflater;
            inflater.Inflate(Resource.Menu.my_menu, menu);
            return true;

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_refresh:
                    {
                        Task.Run(() =>
                          this.ViewModel.GetPosts()
                        );
                        Task.WaitAll();
                        break;
                    }
                case Resource.Id.action_add:
                    ViewModel.AddItemCmd.Execute();
                    break;
                case Resource.Id.action_settings:
                    ViewModel.SettingsCmd.Execute();
                    break;


            }
            return base.OnOptionsItemSelected(item);
        }


        private void SetBindings()
        {
            var bindingSet = this.CreateBindingSet<PostsView, PostsViewModel>();

            bindingSet.Bind(postsList)
                .For(v => v.ItemsSource)
                .To(vm => vm.Posts);


            bindingSet.Bind(loader)
                .For(v => v.Visibility)
                .To(vm => vm.ProgressBarVisibile)
                .WithConversion(new InlineValueConverter<bool, ViewStates>(x => { return x ? ViewStates.Visible : ViewStates.Gone; }));

            bindingSet.Apply();
        }

    }
}