using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Toolbar=Android.Support.V7.Widget.Toolbar;
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

using MvvmCross.Views;
using Android.Content.PM;
using System.Collections.Generic;

namespace LostInLublin.Droid.Views
{
    //[MvxFragmentPresentation(typeof(RootViewModel), Resource.Id.content_frame, true)]
    //[Register(nameof(PostsView))]
    [MvxActivityPresentation]
    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden, MainLauncher = true)]

    public class PostsView : MvxAppCompatActivity<PostsViewModel>
    {
        MvxListView postsList;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.postsLayout);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            var view = this.BindingInflate(Resource.Layout.postsLayout, null);
            postsList = view.FindViewById<MvxListView>(Resource.Id.postsLstv);
            // postsList.ItemTemplateId = Resource.Layout.postItem;
            // postsList.Adapter = new PostsAdapter(this.ApplicationContext, (IMvxAndroidBindingContext)this.BindingContext);
            // postsList.ItemsSource = new List<Post> { new Post { Message = "Znaleziono" }, new Post { Message = "szukaj" } };
            // SetBindings();
            var p = this.ViewModel.GetPosts();
        }

        private void SetBindings()
        {
            var bindingSet = this.CreateBindingSet<PostsView, PostsViewModel>();

            bindingSet.Bind(postsList)
                .For(v => v.ItemsSource)
                .To(vm => vm.Posts);

            //bindingSet.Apply();
        }

    }
}