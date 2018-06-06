using Android.Content;
using Android.Views;
using Android.Widget;
using LostInLublin.Core.Models;
using LostInLublin.Core.ViewModels;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Binding.Views;

namespace LostInLublin.Droid.Views
{
    class PostsAdapter : MvxAdapter
    {
        private object applicationContex;
        private IMvxAndroidBindingContext bindingContext;
        #region constructors


        public PostsAdapter(Context applicationContex, IMvxAndroidBindingContext bindingContext) :base(applicationContex,bindingContext)
        {
            this.applicationContex = applicationContex;
            this.bindingContext = bindingContext;
        }
        #endregion

        protected override View GetBindableView(View convertView, object dataContext, ViewGroup parent, int templateId)
        {
            templateId = Resource.Layout.list_item;
            return base.GetBindableView(convertView, dataContext, parent, templateId);
        }
        protected override IMvxListItemView CreateBindableView(object dataContext, ViewGroup parent, int templateId)
        {

            var name = parent.FindViewById<TextView>(Resource.Id.messageTxt);

            var post = dataContext as Post;

            name.Text = post.Message;
            var view = base.CreateBindableView(dataContext, parent, templateId) as MvxListItemView;
            return view;
        }
    }
}